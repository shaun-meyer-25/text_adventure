using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Room")]
public class Room : ScriptableObject {
	[TextArea]
	public string description;
	[TextArea]
	public string roomInvestigationDescription;
	public string roomName;
	public Exit[] exits;
	public InteractableObject[] interactableObjectsInRoom;
	
	// The use case of the following methods depends on the name lists to be in the same order as the object lists

	public List<string> exitNames()
	{
		List<string> exitNames = new List<string>();
		for (int i = 0; i < exits.Length; i++)
		{
			exitNames.Add(exits[i].keyString);
		}

		return exitNames;
	}
	
	public List<ExitChoice> exitChoices()
	{
		List<ExitChoice> exitChoices = new List<ExitChoice>();

		for (int i = 0; i < exits.Length; i++)
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
}