using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	/*
	 * Things to do: make a 'exiting' flag to keep track of what the button option types are
	 * - maybe make an exit Choice type, that can be used alongside the current Exit class?
	 * 
	 */

	private bool going;
	private static int NUMBER_OF_OPTIONS = 4;
	public Text displayText;
	public Choice[] roomActions;

	[HideInInspector] public List<ExitChoice> exitChoices = new List<ExitChoice>();
	[HideInInspector] public List<string> exitNames = new List<string>();
	[HideInInspector] public List<string> roomActionNames = new List<string>();
	[HideInInspector] public RoomNavigation roomNavigation;
	[HideInInspector] public List<string> interactionDescriptionsInRoom = new List<string> ();

	List<string> actionLog = new List<string>(); 

	void Awake () {
		roomNavigation = GetComponent<RoomNavigation> ();
	}

	void Start () {
		DisplayRoomText ();
		DisplayLoggedText (); 
		UpdateRoomChoices (roomNavigation.currentRoom.roomActions);
	}

	public void UpdateRoomChoices(Choice[] choices)
	{
		
		// todo - if there was an existing CHOICE but there isn't now, remove that choice
        roomActions = choices;
		for (int i = 0; i < NUMBER_OF_OPTIONS; i++) {
			GameObject button = GameObject.Find("Option" + (i + 1));
			Text textObject = button.GetComponentInChildren<Text>();
			
			if (i < choices.Length)
			{
				Choice choice = choices [i];

				if (choice != null) {
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
		roomActionNames = roomNavigation.currentRoom.roomActionNames();
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

	public void LogStringWithReturn(string stringToAdd) {
		actionLog.Add (stringToAdd + "\n");
	}
}
