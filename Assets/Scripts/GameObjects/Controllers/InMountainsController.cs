using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InMountainsController : IController
{
	private static int NUMBER_OF_OPTIONS = 4;
	private Dictionary<string, string> allPreferences;
	private List<string> undisplayedSentences = new List<string>();
	private TextProcessing _textProcessing;
	
	void Awake ()
	{
		_textProcessing = new TextProcessing(this, processingDelay);
		volumeManipulation = gameObject.AddComponent<VolumeManipulation>();
		levelLoader = FindObjectOfType<LevelLoader>();
		checkpointManager = GetComponent<CheckpointManager>();
		interactableItems = GetComponent<InteractableItems> ();
		roomNavigation = GetComponent<RoomNavigation> ();
		isDaytime = true;
	}

	void Start ()
	{
		audio = FindObjectOfType<AudioSource>();
		checkpointManager.SetCheckpoint(StaticDataHolder.instance.Checkpoint);
		displayText.text = "";
		allPreferences = LoadDictionaryFromFile("commandPreferredButtons");
		LoadRoomDataAndDisplayRoomText ();
		DisplayLoggedText (); 
		UpdateRoomChoices (actions);
		roomNavigation.SetExitLabels(roomNavigation.currentRoom.GetExits(checkpointManager.checkpoint));
		interactableItems.AddActionResponsesToUseDictionary();
	}

	public override void UpdateRoomChoices(Choice[] choices)
	{
		actions = choices;

		Dictionary<string, string> preferencesForCurrentChoices =
			allPreferences.Where(kvp => choices.Select(c => c.keyword).Contains(kvp.Key))
				.ToDictionary(pair => pair.Key, pair => pair.Value);
		
		for (int i = 0; i < NUMBER_OF_OPTIONS; i++) {
			GameObject button = GameObject.Find("Option" + (i + 1));
			button.GetComponent<Button>().interactable = true;
			
			Text textObject = button.GetComponentInChildren<Text>();

			if (preferencesForCurrentChoices.ContainsValue(i.ToString()) || 
			    (i < choices.Length && !preferencesForCurrentChoices.ContainsKey(choices[i].keyword)))
			{
				KeyValuePair<string, string> preferredChoiceKvp = 
					preferencesForCurrentChoices.FirstOrDefault(kvp => kvp.Value.Equals(i.ToString()));

				// Set button text to preferred choice, otherwise just set it to whatever choice is up in the index of Choices
				if (!preferredChoiceKvp.Equals(default(KeyValuePair<string, string>)))
				{
					textObject.text = preferredChoiceKvp.Key;
				}
				else
				{
					Choice choice = choices [i];
					
					textObject.text = choice.keyword;				
	
				}
			}
			else
			{
				textObject.text = null;
				button.GetComponent<Button>().interactable = false;
			}

		}

		// We want the game controller to be the source of truth on what the player's options are
		exitChoices = roomNavigation.currentRoom.exitChoices(checkpointManager.checkpoint);
		exitNames = roomNavigation.currentRoom.exitNames(checkpointManager.checkpoint);
	}

	public override void DisplayLoggedText ()
	{
		displayText.text = "";

		string logAsText = string.Join ("\n", actionLog.ToArray ());
		List<string> pastLog = new List<string>(logAsText.Split('\n'));
		while (logAsText.Length > 10000)
		{
			pastLog.RemoveRange(0, pastLog.Count / 2);
			logAsText = string.Join("\n", pastLog.ToArray());
		}
		foreach (var line in pastLog)
		{
			displayText.text += "\n<color=" + currentColor + ">" + line + "</color>";
		}

		string undisplayedLogAsText = string.Join ("\n\n", undisplayedSentences.ToArray ());
		while (undisplayedLogAsText.Length > 10000)
		{
			List<string> log = new List<string>(undisplayedLogAsText.Split('\n'));
			log.RemoveRange(0, log.Count / 2);
			undisplayedLogAsText = string.Join("\n", log.ToArray());
		}
		
		_textProcessing.StopTypingCoroutine();
		
		_textProcessing.DisplayText("\n" + undisplayedLogAsText);
		foreach (var sentence in undisplayedSentences)
		{
			actionLog.Add(sentence + "\n");
		}
		undisplayedSentences.Clear();
	}
	
	public override void LogStringWithReturn(string stringToAdd) {
		if (stringToAdd != "")
		{
			undisplayedSentences.Add(stringToAdd);
		}
	}

	public override void LoadRoomData()
	{
		ClearCollectionsForNewRoom ();
		UnpackRoom ();
		
	}
	
	public override void LoadRoomDataAndDisplayRoomText () {
		LoadRoomData();

		string combinedText = "\n";
		
		if (!roomNavigation.currentRoom.roomName.Equals("home cave"))
		{
			combinedText = DescribeTravelingCompanions(combinedText);
		}

		string joinedInteractionDescriptions = string.Join ("\n", interactionDescriptionsInRoom.ToArray ());
		combinedText = roomNavigation.currentRoom.GetDescription(checkpointManager.checkpoint) + "\n" + joinedInteractionDescriptions + "\n" + combinedText;
		
		LogStringWithReturn (combinedText);
	}

	void UnpackRoom()
	{
		roomNavigation.UnpackExitsInRoom();
		PrepareObjectsToTakeOrExamine(roomNavigation.currentRoom);
	}

	public override void PrepareObjectsToTakeOrExamine(Room currentRoom)
	{
		for (int i = 0; i < currentRoom.InteractableObjectsInRoom.Length; i++)
		{
			string descriptionNotInInventory = interactableItems.GetObjectsNotInInventory(currentRoom, i);
			if (descriptionNotInInventory != null)
			{
				// if we end up making objects something you have to look for, we will change this.
				interactionDescriptionsInRoom.Add(descriptionNotInInventory);
			}

			InteractableObject interactableInRoom = currentRoom.InteractableObjectsInRoom[i];
			for (int j = 0; j < interactableInRoom.interactions.Length; j++)
			{
				Interaction interaction = interactableInRoom.interactions[j];
				if (interaction.action.keyword == "observe")
				{
					interactableItems.examineDictionary.Add(interactableInRoom.noun, interaction.textResponse);
				}
				if (interaction.action.keyword == "interact")
				{
					interactableItems.takeDictionary.Add(interactableInRoom.noun, interaction.textResponse);
				}
			}
		}
	}

	public override string TestVerbDictionaryWithNoun(Dictionary<string, string> verbDictionary, string verb, string noun)
	{
		if (verbDictionary.ContainsKey(noun))
		{
			return verbDictionary[noun];
		}
		return "you can't " + verb + " " + noun;
	}

	void ClearCollectionsForNewRoom() {
		interactableItems.ClearCollections();
		interactionDescriptionsInRoom.Clear ();
		roomNavigation.ClearExits ();
	}

	public List<string> ActionNames()
	{
		List<string> actionNames = new List<string>();
		for (int i = 0; i < actions.Length; i++) {
			actionNames.Add(actions[i].keyword);
		}

		return actionNames;
	}

	public override List<string> ObservableChoiceNames()
	{
		List<string> observableChoiceNames = new List<string>();
		for (int i = 0; i < observableChoices.Length; i++)
		{
			observableChoiceNames.Add(observableChoices[i].keyword);
		}

		return observableChoiceNames;
	}
	
	public List<string> InteractChoiceNames()
	{
		List<string> interactChoiceNames = new List<string>();
		for (int i = 0; i < interactableChoices.Length; i++)
		{
			interactChoiceNames.Add(interactableChoices[i].keyword);
		}

		return interactChoiceNames;
	}

	private string DescribeTravelingCompanions(string combinedText)
	{
		InteractableObject[] peopleInRoom = roomNavigation.currentRoom.PeopleInRoom;
		if (peopleInRoom.Length == 1)
		{
			combinedText = peopleInRoom[0].keyword + " is traveling with you";
		} else if (peopleInRoom.Length == 2)
		{
			combinedText = peopleInRoom[0].keyword + " and " + peopleInRoom[1].keyword + " are traveling with you";
		} else if (peopleInRoom.Length > 2)
		{
			for (int i = 0; i < peopleInRoom.Length; i++)
			{
				if (i == peopleInRoom.Length - 1)
				{
					combinedText += " and " + peopleInRoom[i].keyword + " are traveling with you";
				}
				else
				{
					combinedText += peopleInRoom[i].keyword + ", ";
				}
			}
		}

		return combinedText;
	}

	public IEnumerator TypeSentence(Dictionary<int, Tuple<string, char>> charactersAndTheirColors )
	{
		for (int i = 0; i < charactersAndTheirColors.Count; i++)
		{
			Tuple<string, char> value = charactersAndTheirColors[i];
			displayText.text += "<color=" + value.Item1 + ">" + value.Item2 + "</color>";

			yield return new WaitForSeconds(processingDelay);
		}

	}
}
