using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName= "TextAdventure/InputActions/Go")]
public class Go : InputAction {

	// todo - this is the class i need to workshop. need to figure out how to transition to an "exiting" state after "go"
	
	public override void RespondToInput (GameController controller, string[] separatedInputWords) {

		if (separatedInputWords.Length == 1) {
			List<ExitChoice> exits = new List<ExitChoice>();

			for (int i = 0; i < controller.roomNavigation.currentRoom.exits.Length; i++)
			{
				ExitChoice choice = CreateInstance<ExitChoice>();
				choice.keyword = controller.roomNavigation.currentRoom.exits[i].keyString;
				exits.Add(choice);
			}
		
			if (exits != null && exits.Count > 0) {
				controller.LogStringWithReturn("Go where?");
				controller.UpdateRoomChoices(exits.ToArray());
			}
		} else {
			controller.roomNavigation.AttemptToChangeRooms(separatedInputWords[1]);
		}
	}
}
