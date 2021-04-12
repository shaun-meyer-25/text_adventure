using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
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
		for (int i = 0; i < currentRoom.GetExits(controller.checkpointManager.checkpoint).Length; i++) {
			exitDictionary.Add (currentRoom.GetExits(controller.checkpointManager.checkpoint)[i].keyString, 
				controller.allRoomsInGame.Find(o => o.roomName == 
				                                    currentRoom.GetExits(controller.checkpointManager.checkpoint) [i].roomName));
			controller.interactionDescriptionsInRoom.Add (currentRoom.GetExits(controller.checkpointManager.checkpoint) [i].description);
		}
	}

	public bool AttemptToChangeRooms(string directionNoun) {
		if (exitDictionary.ContainsKey(directionNoun)) {
			if (currentRoom.roomName != "home cave")
			{
				currentRoom.SetBasePeopleInRoom();
			}
			
			if (currentRoom.roomName == "home cave")
			{
				if (controller.isDaytime && controller.currentColor == "white")
				{
					controller.SetDaylight();
				} 
			}
			else
			{
				if (controller.allRoomsInGame.Find(o => o.roomName == exitDictionary[directionNoun].roomName).roomName ==
				    "home cave" && controller.currentColor == "black")
				{
					controller.SetNighttime();
				}
			}
			currentRoom = controller.allRoomsInGame.Find(o => o.roomName == exitDictionary[directionNoun].roomName);
			controller.LogStringWithReturn("you go " + directionNoun);

			CheckIfCheckpointNeedsSetting();
			
			controller.LoadRoomDataAndDisplayRoomText();
			SetExitLabels(currentRoom.GetExits(controller.checkpointManager.checkpoint));

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

		if (currentRoom.roomName == "north foothills" && controller.checkpointManager.checkpoint == 2 && !controller.checkpointManager.ohmInPosition)
		{
			controller.checkpointManager.SetCheckpoint(3);
		}

		if (currentRoom.roomName == "watering hole" && controller.checkpointManager.checkpoint == 3 &&
		    !controller.interactableItems.nounsInInventory.Contains("spear"))
		{
			controller.checkpointManager.SetCheckpoint(4);
		}

		if (currentRoom.roomName == "sleep" && controller.checkpointManager.checkpoint == 4)
		{
			controller.checkpointManager.SetCheckpoint(5);
			controller.levelLoader.LoadScene("First Dream");
		}
	}

	public void SetExitLabels(Exit[] choices)
	{
		List<Exit> listOfExits = new List<Exit>(choices);
		for (int i = 0; i < 4; i++) {
			if (i == 0)
			{
				Exit e = listOfExits.Find(o => (o.keyString == "north" || o.keyString == "outside"));
				if (e != null) controller.northLabel.text = e.roomName;
				else controller.northLabel.text = null;
			}
			if (i == 1)
			{
				Exit e = listOfExits.Find(o => o.keyString == "east");
				if (e != null) controller.eastLabel.text = e.roomName;
				else controller.eastLabel.text = null;
			}
			if (i == 2)
			{
				Exit e = listOfExits.Find(o => o.keyString == "south" || o.keyString == "home");
				if (e != null) controller.southLabel.text = e.roomName;
				else controller.southLabel.text = null;
			}
			if (i == 3)
			{
				Exit e = listOfExits.Find(o => o.keyString == "west");
				if (e != null) controller.westLabel.text = e.roomName;
				else controller.westLabel.text = null;
			}
		}
	}
}
