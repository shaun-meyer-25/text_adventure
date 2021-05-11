using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Room")]
public class Room : ScriptableObject
{
	[SerializeField] [TextArea] private string baseDescription;
	[SerializeField] [TextArea] private string baseInvestigationDescription;

	private List<RoomData> _roomData;
	
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

	public Exit[] GetExits(int checkpoint)
	{
		RoomData roomData = _roomData.Find(o => o.chapter == checkpoint);
		if (roomData != null && roomData.exits != null)
		{
			return roomData.exits.ToArray();
		}

		Debug.Log("no exits found for checkpoint " + checkpoint);
		return new Exit[] { };
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

	public void RemovePersonFromRoom(string personName)
	{
		List<InteractableObject> people = new List<InteractableObject>(PeopleInRoom);
		people.Remove(people.Find(o => o.name == personName));
		peopleInRoom = people.ToArray();
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
		string filePath = "RoomData/" + roomName;
		TextAsset file = Resources.Load<TextAsset>(filePath);
		if (file != null)
		{
			_roomData = new List<RoomData>(JsonUtility.FromJson<Wrapper<RoomData>>("{\"array\":" + file.text + "}").array);
		}
		
		
		interactableObjectsInRoom = baseInteractableObjectsInRoom.ToArray();
		peopleInRoom = basePeopleInRoom.ToArray();
		description = baseDescription;
		roomInvestigationDescription = baseInvestigationDescription;
	}
	
	public string GetDescription(int checkpoint)
	{
		return _roomData.Find(o => o.chapter == checkpoint).description;
	}

	public string GetInvestigationDescription(int checkpoint)
	{
		RoomData data = _roomData.Find(o => o.chapter == checkpoint);
		if (data != null)
		{
			return _roomData.Find(o => o.chapter == checkpoint).investigationDescription;
		}
		
		return null;
	}

	public string GetEffectTriggerName(int checkpoint)
	{
		RoomData data = _roomData.Find(o => o.chapter == checkpoint);
		if (data != null)
		{
			return data.effectTriggerName;
		}

		return null;
	}
	
	public void OnAfterDeserialize() 
	{
	}

	public List<string> exitNames(int checkpoint)
	{
		List<string> exitNames = new List<string>();
		RoomData roomData = _roomData.Find(o => o.chapter == checkpoint);

		for (int i = 0; i < GetExits(checkpoint).Length; i++)
		{
			exitNames.Add(roomData.exits[i].keyString);
		}

		return exitNames;
	}
	
	public List<ExitChoice> exitChoices(int checkpoint)
	{
		List<ExitChoice> exitChoices = new List<ExitChoice>();
		RoomData roomData = _roomData.Find(o => o.chapter == checkpoint);

		for (int i = 0; i <  GetExits(checkpoint).Length; i++)
		{
			ExitChoice choice = CreateInstance<ExitChoice>();
			choice.keyword = roomData.exits[i].keyString;
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