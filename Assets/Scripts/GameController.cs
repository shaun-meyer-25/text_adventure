using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class GameController : MonoBehaviour {
	
	private static int NUMBER_OF_OPTIONS = 4;
	private Dictionary<string, string> allPreferences;
	private Dictionary<string, string> caveDescription;
	private Dictionary<string, string> caveInvestigationDescriptions;
	public InteractableObject[] characters;
	public Text displayText;
	public Image background;
	public Choice[] actions;
	public ObserveChoice[] observableChoices;
	public AudioSource tunnelSceneBackground;
	public InteractChoice[] interactableChoices;
	public List<InteractableObject> travelingCompanions;
	List<string> actionLog = new List<string>(); 

	[HideInInspector] public List<ExitChoice> exitChoices = new List<ExitChoice>();
	[HideInInspector] public List<string> exitNames = new List<string>();
	[HideInInspector] public RoomNavigation roomNavigation;
	[HideInInspector] public List<string> interactionDescriptionsInRoom = new List<string> ();
	[HideInInspector] public InteractableItems interactableItems;
	[HideInInspector] public bool isInteracting = false;
	[HideInInspector] public bool isObserving = false;
	[HideInInspector] public bool isUsing = false;
	[HideInInspector] public Fire fire;
	[HideInInspector] public CheckpointManager checkpointManager;

	// todo - instead of using this workaround, we should make a "CalculateDNextChoices" or something based on game state and room
	// this section mostly for debugging
	public Choice[] startingActions;
	
	void Awake ()
	{
		interactableItems = GetComponent<InteractableItems> ();
		roomNavigation = GetComponent<RoomNavigation> ();
		fire = GetComponent<Fire>();
		checkpointManager = GetComponent<CheckpointManager>();
		
		// todo - probably want an audio loading class or method
		
		if (SceneManager.GetActiveScene().name != "Experimental") return;
		Component[] aSources = GetComponents(typeof(AudioSource));
		tunnelSceneBackground = (AudioSource) aSources[0];
	}

	void Start ()
	{
		allPreferences = LoadDictionaryFromFile("commandPreferredButtons");
		caveDescription = LoadDictionaryFromFile("homeCaveDescriptions");
		caveInvestigationDescriptions = LoadDictionaryFromFile("homeCaveInvestigationDescriptions");
		if (SceneManager.GetActiveScene().name == "Main")
		{
			// todo - let's get this in a text file or something, it sucks to hardcode it in like this
			LogStringWithReturn(
				"eyes open. you look around the cave. this is your home. there are many figures laying nearby. the familiar shape next to you makes you feel safe and warm. you reach out and grab their hand. they are still asleep.");
		}

		if (SceneManager.GetActiveScene().name == "Experimental")
		{
			tunnelSceneBackground.Play();
		}
		
		LoadRoomDataAndDisplayRoomText ();
		DisplayLoggedText (); 
		UpdateRoomChoices (actions);
		
	}

	public void UpdateRoomChoices(Choice[] choices)
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
		exitChoices = roomNavigation.currentRoom.exitChoices();
		exitNames = roomNavigation.currentRoom.exitNames();
	}

	public void DisplayLoggedText () {
		for (int i = 0; i < actionLog.Count; i++)
		{
			// todo - get this into a data file
			actionLog[i] = actionLog[i].Replace("you don't need to do this", "<color=red>" + "you don't need to do this" + "</color>");
			actionLog[i] = actionLog[i].Replace("you can't go back. only forward.",
				"<color=purple>" + "you can't go back. only forward." + "</color>");
		}
		string logAsText = string.Join ("\n", actionLog.ToArray ());
		while (logAsText.Length > 10000)
		{
			List<string> log = new List<string>(logAsText.Split('\n'));
			log.RemoveRange(0, log.Count / 2);
			logAsText = string.Join("\n", log.ToArray());
		}
		displayText.text = logAsText;
	}
	
	public void LoadRoomDataAndDisplayRoomText () {
		ClearCollectionsForNewRoom ();
		UnpackRoom ();

		for (int i = 0; i < travelingCompanions.Count; i++)
		{
			roomNavigation.currentRoom.AddPersonToRoom(travelingCompanions[i]);
		}

		string combinedText = "\n";

		if (roomNavigation.currentRoom.roomName.Equals("home cave"))
		{
			roomNavigation.currentRoom.description = caveDescription[checkpointManager.checkpoint.ToString()];
			roomNavigation.currentRoom.roomInvestigationDescription = caveInvestigationDescriptions[checkpointManager.checkpoint.ToString()];
		}
		else
		{
			combinedText = DescribeTravelingCompanions(combinedText);
		}

		string joinedInteractionDescriptions = string.Join ("\n", interactionDescriptionsInRoom.ToArray ());
		combinedText = roomNavigation.currentRoom.description + "\n" + joinedInteractionDescriptions + "\n" + combinedText;
		
		LogStringWithReturn (combinedText);
	}

	void UnpackRoom()
	{
		roomNavigation.UnpackExitsInRoom();
		PrepareObjectsToTakeOrExamine(roomNavigation.currentRoom);
	}

	void PrepareObjectsToTakeOrExamine(Room currentRoom)
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

	public string TestVerbDictionaryWithNoun(Dictionary<string, string> verbDictionary, string verb, string noun)
	{
		if (verbDictionary.ContainsKey(noun))
		{
			return verbDictionary[noun];
		}
		return "You can't " + verb + " " + noun;
	}

	void ClearCollectionsForNewRoom() {
		interactableItems.ClearCollections();
		interactionDescriptionsInRoom.Clear ();
		roomNavigation.ClearExits ();
	}

	public Dictionary<string, string> LoadDictionaryFromFile(string fileName)
	{
		TextAsset data = (TextAsset) Resources.Load(fileName);
		string[] lines = data.text.Split('\n');
		
		Dictionary<String, String> dict = new Dictionary<string, string>();

		foreach (string line in lines)
		{
			string[] split = line.Split('=');
			string key = split[0].Trim();
			string value = split[1].Trim();
			dict.Add(key, value);
		}
		
		return dict;
	}
	
	public Dictionary<string, List<string>> LoadDictionaryFromCsvFile(string fileName)
	{
		TextAsset data = (TextAsset) Resources.Load(fileName);
		string[] lines = data.text.Split('\n');
		
		Dictionary<String, List<String>> dict = new Dictionary<string, List<string>>();

		foreach (string line in lines)
		{
			string[] split = line.Split(',');
			string key = split[0].Trim();
			List<string> value = split.Skip(1).ToList();
			dict.Add(key, value);
		}
		
		return dict;
	}

	public void LogStringWithReturn(string stringToAdd) {
		actionLog.Add (stringToAdd + "\n");
	}
	
	public List<string> ActionNames()
	{
		List<string> actionNames = new List<string>();
		for (int i = 0; i < actions.Length; i++) {
			actionNames.Add(actions[i].keyword);
		}

		return actionNames;
	}

	public List<string> ObservableChoiceNames()
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
			combinedText = peopleInRoom[0] + " and " + peopleInRoom[1] + " are traveling with you";
		} else if (peopleInRoom.Length > 2)
		{
			for (int i = 0; i < peopleInRoom.Length; i++)
			{
				if (i == peopleInRoom.Length - 1)
				{
					combinedText += " and " + peopleInRoom[i] + " are traveling with you";
				}
				else
				{
					combinedText += peopleInRoom[i] + ", ";
				}
			}
		}

		return combinedText;
	}

	public void YouAreDead()
	{
		LogStringWithReturn("the unforgiving world has claimed your life.");
		for (int i = 0; i < NUMBER_OF_OPTIONS; i++)
		{
			GameObject button = GameObject.Find("Option" + (i + 1));
			button.GetComponent<Button>().interactable = false;
		}
	}
}
