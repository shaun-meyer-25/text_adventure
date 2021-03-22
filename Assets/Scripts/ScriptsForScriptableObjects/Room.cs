using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Room")]
public class Room : ScriptableObject
{
	[SerializeField] [TextArea] private string baseDescription;
	[SerializeField] [TextArea] private string baseInvestigationDescription;
	
	[TextArea]
	public string description;
	[TextArea]
	public string roomInvestigationDescription;
	public string roomName;
	
	[SerializeField] private List<InteractableObject> baseInteractableObjectsInRoom;
	
	private InteractableObject[] interactableObjectsInRoom;
	public InteractableObject[] InteractableObjectsInRoom
	{
		get { return interactableObjectsInRoom; }
	}

	[SerializeField] [ItemCanBeNull] private List<InteractableObject> basePeopleInRoom;
	
	private InteractableObject[] peopleInRoom;

	public InteractableObject[] PeopleInRoom
	{
		get { return peopleInRoom;  }
	}

	[SerializeField] private List<Exit> baseExits;

	private Exit[] exits;

	public Exit[] Exits
	{
		get { return exits.ToArray(); }
	}

	public void SetExitsInRoom(Exit[] e)
	{
		exits = e;
	}
	
	public void SetInteractableObjectsInRoom(InteractableObject[] objects)
	{
		interactableObjectsInRoom = objects;
	}

	public void AddObjectToRoom(InteractableObject obj)
	{
		List<InteractableObject> updatedObjectsInRoom = new List<InteractableObject>(InteractableObjectsInRoom);
		updatedObjectsInRoom.Add(obj);
		interactableObjectsInRoom = updatedObjectsInRoom.ToArray();
	}

	public void SetPeopleInRoom(InteractableObject[] objects)
	{
		peopleInRoom = objects;
	}
	
	public void AddPersonToRoom(InteractableObject obj)
	{
		List<InteractableObject> updatedPeopleInRoom;
		if (PeopleInRoom == null)
		{
			updatedPeopleInRoom = new List<InteractableObject>();
		}
		else
		{
			updatedPeopleInRoom = new List<InteractableObject>(PeopleInRoom);
		}
		
		updatedPeopleInRoom.Add(obj);
		peopleInRoom = updatedPeopleInRoom.ToArray();
	}
	
	// using https://answers.unity.com/questions/1664323/how-are-you-resetting-your-scriptable-objects-betw.html
	
	// The use case of the following methods depends on the name lists to be in the same order as the object lists

	private void OnEnable()
	{
		exits = baseExits.ToArray();
		interactableObjectsInRoom = baseInteractableObjectsInRoom.ToArray();
		peopleInRoom = basePeopleInRoom.ToArray();
		description = baseDescription;
		roomInvestigationDescription = baseInvestigationDescription;
	}
	
	public void OnAfterDeserialize() 
	{
	}

	public List<string> exitNames()
	{
		List<string> exitNames = new List<string>();
		for (int i = 0; i < Exits.Length; i++)
		{
			exitNames.Add(exits[i].keyString);
		}

		return exitNames;
	}
	
	public List<ExitChoice> exitChoices()
	{
		List<ExitChoice> exitChoices = new List<ExitChoice>();

		for (int i = 0; i < Exits.Length; i++)
		{
			ExitChoice choice = CreateInstance<ExitChoice>();
			choice.keyword = exits[i].keyString;
			exitChoices.Add(choice);
		}

		return exitChoices;
	}	
	
	public List<string> ObjectNames()
	{
		List<string> objectNames = new List<string>();
		for (int i = 0; i < interactableObjectsInRoom.Length; i++) {
			objectNames.Add(interactableObjectsInRoom[i].keyword);
		}

		return objectNames;
	}

	public List<string> CharacterNamesInRoom()
	{
		List<string> characterNames = new List<string>();
		for (int i = 0; i < peopleInRoom.Length; i++)
		{
			characterNames.Add(peopleInRoom[i].keyword);
		}

		return characterNames;
	}

	public void SetBasePeopleInRoom()
	{
		peopleInRoom = basePeopleInRoom.ToArray();
	}
}