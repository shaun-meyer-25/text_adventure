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
	public InputAction[] roomActions;

	public List<string> roomActionNames()
	{
		List<string> roomActionNames = new List<string>();
		for (int i = 0; i < roomActions.Length; i++) {
			roomActionNames.Add(roomActions[i].keyword);
		}

		return roomActionNames;
	}
}