using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class GameController : IController {
	
	private static int NUMBER_OF_OPTIONS = 4;
	private Dictionary<string, string> allPreferences;
	private Dictionary<string, string> caveDescription;
	private Dictionary<string, string> caveInvestigationDescriptions;
	private List<string> undisplayedSentences = new List<string>();
	private TextProcessing _textProcessing;
	
	public LevelLoader levelLoader;
	public InteractableObject[] characters;
	public Image background;
	public Choice[] actions;
	public ObserveChoice[] observableChoices;
	public AudioSource audio;
	public InteractChoice[] interactableChoices;
	public List<InteractableObject> travelingCompanions;
	public List<Room> allRoomsInGame;
	public Animator backgroundColor;
	public bool isDaytime;
	public float processingDelay;
	public Text northLabel;
	public Text eastLabel;
	public Text westLabel;
	public Text southLabel;
	public VolumeManipulation volumeManipulation;
	public OptionButton fifthButton;

	[HideInInspector] public List<string> actionLog = new List<string>();
	[HideInInspector] public List<ExitChoice> exitChoices = new List<ExitChoice>();
	[HideInInspector] public List<string> exitNames = new List<string>();
	[HideInInspector] public RoomNavigation roomNavigation;
	[HideInInspector] public List<string> interactionDescriptionsInRoom = new List<string> ();
	[HideInInspector] public InteractableItems interactableItems;
	[HideInInspector] public bool isInteracting = false;
	[HideInInspector] public bool isObserving = false;
	[HideInInspector] public bool isUsing = false;
	[HideInInspector] public bool isConversing = false;
	[HideInInspector] public Fire fire;
	[HideInInspector] public CheckpointManager checkpointManager;
	
	public Choice[] startingActions;
	
	void Awake ()
	{
		_textProcessing = new TextProcessing(this, processingDelay);
		volumeManipulation = gameObject.AddComponent<VolumeManipulation>();
		foreach (var text in FindObjectsOfType<Text>())
		{
			if (text.name == "NorthExitLabel")
			{
				northLabel = text;
			}
			if (text.name == "EastExitLabel")
			{
				eastLabel = text;
			}
			if (text.name == "SouthExitLabel")
			{
				southLabel = text;
			}
			if (text.name == "WestExitLabel")
			{
				westLabel = text;
			}
		}
		levelLoader = FindObjectOfType<LevelLoader>();
		checkpointManager = GetComponent<CheckpointManager>();
		interactableItems = GetComponent<InteractableItems> ();
		roomNavigation = GetComponent<RoomNavigation> ();
		fire = GetComponent<Fire>();
		isDaytime = true;
	}

	void Start ()
	{
		audio = FindObjectOfType<AudioSource>();
		checkpointManager.SetCheckpoint(StaticDataHolder.instance.Checkpoint);
		displayText.text = "";
		allPreferences = LoadDictionaryFromFile("commandPreferredButtons");
		caveDescription = LoadDictionaryFromFile("homeCaveDescriptions");
		caveInvestigationDescriptions = LoadDictionaryFromFile("homeCaveInvestigationDescriptions");
		if (SceneManager.GetActiveScene().name == "Main" && checkpointManager.checkpoint == 0)
		{
			// todo - let's get this in a text file or something, it sucks to hardcode it in like this
			LogStringWithReturn(
				"eyes open. you look around the cave. this is your home. there are many figures laying nearby. the familiar shape next to you makes you feel safe and warm. you reach out and grab their hand. they are still asleep.");
		}
		if (SceneManager.GetActiveScene().name == "Second Day" && checkpointManager.checkpoint == 8)
		{
			// todo - let's get this in a text file or something, it sucks to hardcode it in like this
			LogStringWithReturn(
				"you awake slowly. you sit up, and see that the others have woken up before you. they seem to have noticed the orb that you kept in your hands.");
		}

		LoadRoomDataAndDisplayRoomText ();
		DisplayLoggedText (); 
		UpdateRoomChoices (actions);
		roomNavigation.SetExitLabels(roomNavigation.currentRoom.GetExits(checkpointManager.checkpoint));
		interactableItems.AddActionResponsesToUseDictionary();
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
		exitChoices = roomNavigation.currentRoom.exitChoices(checkpointManager.checkpoint);
		exitNames = roomNavigation.currentRoom.exitNames(checkpointManager.checkpoint);
	}

	public void DisplayLoggedText ()
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
	
	public void LogStringWithReturn(string stringToAdd) {
		undisplayedSentences.Add(stringToAdd);
	}

	public void LoadRoomData()
	{
		ClearCollectionsForNewRoom ();
		UnpackRoom ();
		
	}
	
	public void LoadRoomDataAndDisplayRoomText () {
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

	public void PrepareObjectsToTakeOrExamine(Room currentRoom)
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
		return "you can't " + verb + " " + noun;
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

	public void SetDaylight()
	{
		backgroundColor.SetTrigger("SetDaytime");
		currentColor = "black";
	}

	public void SetNighttime()
	{
		backgroundColor.SetTrigger("SetNighttime");
		currentColor = "white";
	}
	
	public void BearKillsYou()
	{
		SceneManager.LoadScene("BearKillsYou");
	}
	
	void DelayedSceneLoad()
	{
		levelLoader.LoadScene("Thank You");
	}

	public Text GetTextForButton(string key)
	{
		if (key == "0")
		{
			return northLabel;
		}
		else if (key == "1")
		{
			return eastLabel;
		}
		else if (key == "2")
		{
			return southLabel;
		}
		else if (key == "3")
		{
			return westLabel;
		}

		throw new Exception("we don't have a preferred key for this");
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
