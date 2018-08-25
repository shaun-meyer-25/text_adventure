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
	public Text displayText;
	public Choice[] roomActions;

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
        roomActions = roomNavigation.currentRoom.roomActions;
		for (int i = 0; i < choices.Length; i++) {
            Choice choice = choices [i];
            GameObject button = GameObject.Find("Option" + (i + 1));
            Text textObject = button.GetComponentInChildren<Text>();
			if (choice != null) {
				textObject.text = choice.keyword;				
			}
		}
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
