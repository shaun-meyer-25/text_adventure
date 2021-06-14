using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = System.Object;

public class RoomNavigation : MonoBehaviour {
	
	// RoomNavigation keeps track of the current room and its properties, like its exits
	
	public Room currentRoom;

	private IController controller;

	Dictionary<string, Room> exitDictionary = new Dictionary<string, Room>();
	void Awake() {
		controller = GetComponent<IController> ();
	}

	public void UnpackExitsInRoom() { 
		Debug.Log(currentRoom.roomName);
		Debug.Log(controller);
		for (int i = 0; i < currentRoom.GetExits(controller.checkpointManager.checkpoint).Length; i++)
		{
			string roomName = currentRoom.GetExits(controller.checkpointManager.checkpoint)[i].roomName;
			if (roomName.Contains("<color"))
			{
				roomName = roomName.Substring(TextProcessing.LookAheadForChar(0, roomName, '>') + 1)
					.Replace("</color>", "");
			}
			exitDictionary.Add (currentRoom.GetExits(controller.checkpointManager.checkpoint)[i].keyString, 
				controller.allRoomsInGame.Find(o => o.roomName == 
				                                   roomName));
		}
	}

	public bool AttemptToChangeRooms(string directionNoun) {
		if (exitDictionary.ContainsKey(directionNoun)) {
			for (int i = 0; i < controller.travelingCompanions.Count; i++)
			{
				currentRoom.RemovePersonFromRoom(controller.travelingCompanions[i].name);
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

			if (directionNoun == "sleep")
			{
				controller.LogStringWithReturn("you go to sleep");
			}
			else
			{
				controller.LogStringWithReturn("you go " + directionNoun);
			}

			for (int i = 0; i < controller.travelingCompanions.Count; i++)
			{
				if (!currentRoom.PeopleInRoom.Contains(controller.travelingCompanions[i]))
				{
					currentRoom.AddPersonToRoom(controller.travelingCompanions[i]);
				}
			}
			
			CheckIfCheckpointNeedsSetting();

			if (currentRoom.GetEffectTriggerName(controller.checkpointManager.checkpoint) != null &&
				currentRoom.GetEffectTriggerName(controller.checkpointManager.checkpoint) != "")
			{
				controller.volumeManipulation.EffectStart(controller, currentRoom.GetEffectTriggerName(controller.checkpointManager.checkpoint));
			}
			
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

		if (currentRoom.roomName == "north foothills" && controller.checkpointManager.checkpoint == 2 &&
		    !controller.checkpointManager.ohmInPosition)
		{
			controller.checkpointManager.SetCheckpoint(3);
		}

		if (currentRoom.roomName == "watering hole" && controller.checkpointManager.checkpoint == 4)
		{
			List<Interaction> interactions =
				new List<Interaction>(controller.characters.First(o => o.noun.Equals("Ohm")).interactions);
			Interaction interaction = interactions.Find(o => o.action.keyword.Equals("interact"));
			interaction.textResponse =
				"they have a dark look on their face. 'berries, from woods'. a simple command that describes your failures today.";
		}

		if (currentRoom.roomName == "sleep" && controller.checkpointManager.checkpoint == 5)
		{
			controller.checkpointManager.SetCheckpoint(6);
			controller.levelLoader.LoadScene("First Dream");
		}

		if (currentRoom.roomName == "sleep" && controller.checkpointManager.checkpoint == 7)
		{
			// todo - when we call this method this insures that SetCheckpoint is called twice. we need it only called once
			// it's called the second time in the Start function in GameController
			controller.checkpointManager.SetCheckpoint(8);
			controller.levelLoader.LoadScene("Second Day");
		}

		if (currentRoom.roomName == "watering hole" && controller.checkpointManager.checkpoint == 9)
		{
			controller.checkpointManager.SetCheckpoint(10);
		}

		if (currentRoom.roomName == "mountains" && controller.checkpointManager.checkpoint == 15)
		{
			controller.checkpointManager.SetCheckpoint(16);
			controller.levelLoader.LoadScene("In Mountains");
		}
	}

	public void SetExitLabels(Exit[] choices)
	{
		List<Exit> listOfExits = new List<Exit>(choices);
		if (SceneManager.GetActiveScene().name == "In Mountains") return;
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
