﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class RoomNavigation : MonoBehaviour {
	
	// RoomNavigation keeps track of the current room and its properties, like its exits
	
	public Room currentRoom;

	private GameController controller;

	Dictionary<string, Room> exitDictionary = new Dictionary<string, Room>();
	void Awake() {
		controller = GetComponent<GameController> ();
	}

	public void UnpackExitsInRoom() { 
		for (int i = 0; i < currentRoom.Exits.Length; i++) {
			exitDictionary.Add (currentRoom.Exits [i].keyString, currentRoom.Exits [i].valueRoom);
			controller.interactionDescriptionsInRoom.Add (currentRoom.Exits [i].exitDescription);
		}
	}

	public bool AttemptToChangeRooms(string directionNoun) {
		if (exitDictionary.ContainsKey(directionNoun)) {
			if (currentRoom.roomName != "home cave")
			{
				currentRoom.SetBasePeopleInRoom();
			}
			currentRoom = exitDictionary[directionNoun];
			controller.LogStringWithReturn("you go " + directionNoun);

			CheckIfCheckpointNeedsSetting();
			
			controller.LoadRoomDataAndDisplayRoomText();
			controller.UpdateRoomChoices(controller.startingActions);
			
			return true;
		} else {
			controller.LogStringWithReturn("There is no path to the " + directionNoun);
			return false;
		}
	}

	public void ClearExits() {
		exitDictionary.Clear();
	}

	private void CheckIfCheckpointNeedsSetting()
	{
		if (currentRoom.roomName == "outside home" &&
		    controller.interactableItems.nounsInInventory.Contains("spear") &&
		    controller.checkpointManager.checkpoint == 1)
		{
			controller.checkpointManager.SetCheckpoint(2);
		}

		if (currentRoom.roomName == "north foothills" && controller.checkpointManager.checkpoint == 2)
		{
			controller.checkpointManager.SetCheckpoint(3);
		}

		if (currentRoom.roomName == "watering hole" && controller.checkpointManager.checkpoint == 3 &&
		    !controller.interactableItems.nounsInInventory.Contains("spear"))
		{
			controller.checkpointManager.SetCheckpoint(4);
		}
	}
}
