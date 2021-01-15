using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	
	private bool going;
	private static int NUMBER_OF_OPTIONS = 4;
	private Dictionary<string, string> allPreferences;
	public Text displayText;

	[HideInInspector] public List<ExitChoice> exitChoices = new List<ExitChoice>();
	[HideInInspector] public List<string> exitNames = new List<string>();
	[HideInInspector] public RoomNavigation roomNavigation;
	[HideInInspector] public List<string> interactionDescriptionsInRoom = new List<string> ();
	
	// todo - make this final, or find a better way to lock it in as unchanging
	[HideInInspector] public Choice[] startingActions;
	
	
	[HideInInspector] public Choice[] actions;

	List<string> actionLog = new List<string>(); 

	void Awake () {
		roomNavigation = GetComponent<RoomNavigation> ();
	}

	void Start ()
	{
		InitializeChoices();
		allPreferences = LoadPreferredButtonsForOptions();
		DisplayRoomText ();
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
			Text textObject = button.GetComponentInChildren<Text>();

			if (i < choices.Length || preferencesForCurrentChoices.ContainsValue(i.ToString()))
			{
				KeyValuePair<string, string> preferredChoiceKvp = 
					preferencesForCurrentChoices.FirstOrDefault(kvp => kvp.Value.Equals(i.ToString()));

				// Set button text to preferred choice, otherwise just set it to whatever choice is up in the index of Choices
				if (!preferredChoiceKvp.Equals(null))
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
			}

		}

		// We want the game controller to be the source of truth on what the player's options are
		exitChoices = roomNavigation.currentRoom.exitChoices();
		exitNames = roomNavigation.currentRoom.exitNames();
	}

	public void DisplayLoggedText () {
		string logAsText = string.Join ("\n", actionLog.ToArray ());
		displayText.text = logAsText;
	}
	
	public void DisplayRoomText () {
		ClearCollectionsForNewRoom ();
		UnpackRoom ();

		string joinedInteractionDescriptions = string.Join ("\n", interactionDescriptionsInRoom.ToArray ());
		string combinedText = roomNavigation.currentRoom.description + "\n" + joinedInteractionDescriptions;

		LogStringWithReturn (combinedText);
	}

	void UnpackRoom() {
		roomNavigation.UnpackExitsInRoom ();
	}

	void ClearCollectionsForNewRoom() {
		interactionDescriptionsInRoom.Clear ();
		roomNavigation.ClearExits ();
	}

	Dictionary<string, string> LoadPreferredButtonsForOptions()
	{
		TextAsset data = (TextAsset) Resources.Load("commandPreferredButtons");
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

	public void InitializeChoices()
	{
		Go go = ScriptableObject.CreateInstance<Go>();
		go.keyword = "go";

		Look look = ScriptableObject.CreateInstance<Look>();
		look.keyword = "look";

		startingActions = new InputAction[] {
			go, look
		};
		
		actions = new InputAction[] {
			go, look
		};
	}
}
