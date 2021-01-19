using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour {
	
	// RoomNavigation keeps track of the current room and its properties, like its exits
	
	public Room currentRoom;

	private GameController controller;

	Dictionary<string, Room> exitDictionary = new Dictionary<string, Room>();
	void Awake() {
		controller = GetComponent<GameController> ();
	}

	public void UnpackExitsInRoom() { 
		for (int i = 0; i < currentRoom.exits.Length; i++) {
			exitDictionary.Add (currentRoom.exits [i].keyString, currentRoom.exits [i].valueRoom);
			controller.interactionDescriptionsInRoom.Add (currentRoom.exits [i].exitDescription);
		}
	}

	// todo - before we were using the next room's options to update our choices. we will now have to figure out a way to do that with global actions
	public bool AttemptToChangeRooms(string directionNoun) {
		if (exitDictionary.ContainsKey(directionNoun)) {
			currentRoom = exitDictionary[directionNoun];
			controller.LogStringWithReturn("You head off to the " + directionNoun);
			controller.DisplayRoomText();
			
			controller.UpdateRoomChoices(controller.startingActions);

			if (controller.debuggingMode)
			{
				controller.currentRoom = currentRoom.roomName;
			}

			return true;
		} else {
			controller.LogStringWithReturn("There is no path to the " + directionNoun);
			return false;
		}
	}

	public void ClearExits() {
		exitDictionary.Clear();
	}
}
