﻿using System;
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

	public bool AttemptToChangeRooms(string directionNoun)
	{
		string previousRoom = controller.roomNavigation.currentRoom.roomName;
		
		if (exitDictionary.ContainsKey(directionNoun)) {
			for (int i = 0; i < controller.travelingCompanions.Count; i++)
			{
				currentRoom.RemovePersonFromRoom(controller.travelingCompanions[i].name);
			}
			
			if (currentRoom.roomName == "home cave")
			{
				if (controller.isDaytime && controller.currentColor == "white" && SceneManager.GetActiveScene().name != "Third Day")
				{
					controller.SetDaylight();
				} else if (SceneManager.GetActiveScene().name == "Third Day")
				{
					controller.SetStorm();
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
				controller.LogStringWithReturn("you go to sleep.");
			}
			else
			{
				controller.LogStringWithReturn("you go " + directionNoun + ".");
			}

			for (int i = 0; i < controller.travelingCompanions.Count; i++)
			{
				if (!currentRoom.PeopleInRoom.Contains(controller.travelingCompanions[i]))
				{
					currentRoom.AddPersonToRoom(controller.travelingCompanions[i]);
				}
			}
			
			CheckIfCheckpointNeedsSetting();

			CheckIfAudioNeedsAdjusting(previousRoom);
			
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

	private void CheckIfAudioNeedsAdjusting(string previousRoom)
	{
		///// FIRST DAY
		///
		/// 
		if (controller.isDaytime && (previousRoom == "home cave" && currentRoom.roomName == "outside home") && controller.checkpointManager.checkpoint < 5)
		{
			if (controller.audio != null)
			{
				controller.audio.clip = StaticDataHolder.instance.FirstDaySong;
				controller.audio.volume = 0.7f;
				controller.audio.Play();
			}
		}

		if (controller.audio != null && controller.audio.isPlaying && currentRoom.roomName == "home cave"  && controller.checkpointManager.checkpoint < 5)
		{
			StartCoroutine(FadeAudioSource.StartFade(controller.audio, 1f, 0f));
		}
		
		if (controller.audio != null && controller.audio.isPlaying && currentRoom.roomName == "home cave" && controller.checkpointManager.checkpoint < 2)
		{
			StartCoroutine(FadeAudioSource.StartFade(controller.audio, .5f, 0f));
		}

		if (controller.audio != null && controller.audio.isPlaying && currentRoom.roomName == "home cave" &&
		    controller.checkpointManager.checkpoint > 0 && controller.checkpointManager.checkpoint < 6)
		{
			StartCoroutine(FadeAudioSource.StartFade(controller.audio, .5f, 0f));
			StartCoroutine(FadeAudioSource.StartNew(controller.audio, 1f, StaticDataHolder.instance.Fire, 0.3f));
		}
		
		
		////// SECOND DAY
		///
		///
		if (controller.isDaytime && (previousRoom == "home cave" && currentRoom.roomName == "outside home") && controller.checkpointManager.checkpoint >= 8 && controller.checkpointManager.checkpoint < 12)
		{
			if (controller.audio != null)
			{
				controller.audio.clip = StaticDataHolder.instance.SecondDaySong;
				controller.audio.volume = 0.3f;
				controller.audio.Play();
			}
		}
		
		if (controller.audio != null && controller.audio.isPlaying && currentRoom.roomName == "home cave" && controller.checkpointManager.checkpoint == 12)
		{
			StartCoroutine(FadeAudioSource.StartFade(controller.audio, .5f, 0f));
		}

		
		if (controller.audio != null && controller.audio.isPlaying && currentRoom.roomName == "home cave" &&
		    controller.checkpointManager.checkpoint >= 8 && controller.checkpointManager.checkpoint < 12)
		{
			StartCoroutine(FadeAudioSource.StartFade(controller.audio, .5f, 0f));
			StartCoroutine(FadeAudioSource.StartNew(controller.audio, 1f, StaticDataHolder.instance.Fire, 0.3f));
		}

		////// THIRD DAY
		///
		///
		if (previousRoom == "home cave" && currentRoom.roomName == "outside home" &&
		    controller.checkpointManager.checkpoint >= 14)
		{
			controller.audio.clip = StaticDataHolder.instance.Wind;
			controller.audio.volume = .1f;
			controller.audio.Play();
		}

		if (controller.audio != null && currentRoom.roomName == "home cave" &&
		    controller.checkpointManager.checkpoint >= 14)
		{
			controller.audio.Stop();
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
			controller.travelingCompanions.Add(controller.characters.First(o => o.noun.Equals("Ohm")));
			controller.checkpointManager.SetCheckpoint(2);
		}

		if (currentRoom.roomName == "upper foothills" && controller.checkpointManager.checkpoint == 2 &&
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
				"they have a dark look on their face. they forcefully point to the west, to the forest to forage. a simple command that describes your failures today.";
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

		if (currentRoom.roomName == "sleep" && controller.checkpointManager.checkpoint == 13)
		{
			controller.checkpointManager.SetCheckpoint(14);
			controller.levelLoader.LoadScene("Nightmare");
		}
		
		if (currentRoom.roomName == "mountains" && controller.checkpointManager.checkpoint == 15)
		{
			controller.checkpointManager.SetCheckpoint(16);
			controller.levelLoader.LoadScene("In Mountains");
			throw new Exception("stop scene");
		}

		if (currentRoom.roomName == "outside home" && controller.checkpointManager.checkpoint == 20 && !HasShell())
		{
			InteractableObject person = new List<InteractableObject>(controller.characters)
				.Find(o => o.name == "Tei");
			Interaction interaction =
				new List<Interaction>(person.interactions).Find(o => o.action.keyword.Equals("interact"));
			interaction.textResponse = "Tei has a brightness in their eyes as they go with you towards your usual place. the storm has stopped for now and they seem happy to be out of the cave.";
		}
		
		if (currentRoom.roomName == "peninsula" && controller.checkpointManager.checkpoint == 20 && !HasShell())
		{
			InteractableObject person = new List<InteractableObject>(controller.characters)
				.Find(o => o.name == "Tei");
			Interaction interaction =
				new List<Interaction>(person.interactions).Find(o => o.action.keyword.Equals("interact"));
			interaction.textResponse = "it is good to be here with them, once again.";
		}
		
		if (currentRoom.roomName == "home cave" && controller.checkpointManager.checkpoint == 20 && HasShell())
		{
			controller.checkpointManager.SetCheckpoint(19);
			currentRoom.AddObjectToRoom(controller.checkpointManager.torch);
		}

		if (currentRoom.roomName == "mountains4" && controller.checkpointManager.checkpoint == 17)
		{
			//controller.checkpointManager.SetCheckpoint(21);
			StaticDataHolder.instance.Checkpoint = 24;
			controller.levelLoader.LoadScene("Final Cave Bad");
			throw new Exception("stop scene");
		}
		
		if (currentRoom.roomName == "mountains4" && controller.checkpointManager.checkpoint == 18)
		{
			//controller.checkpointManager.SetCheckpoint(21);
			StaticDataHolder.instance.Checkpoint = 21;
			controller.levelLoader.LoadScene("Final Cave");
			throw new Exception("stop scene");
		}
	}

	private bool HasShell()
	{
		return controller.interactableItems.nounsInInventory.Contains("shell") ||
		        controller.interactableItems.nounsInInventory.Contains("tei's shell");
	}
	
	public void SetExitLabels(Exit[] choices)
	{
		List<Exit> listOfExits = new List<Exit>(choices);
		if (SceneManager.GetActiveScene().name == "In Mountains" || SceneManager.GetActiveScene().name == "TetrisGame"
		|| SceneManager.GetActiveScene().name == "Final Cave" || SceneManager.GetActiveScene().name == "Final Cave Bad") return;
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
