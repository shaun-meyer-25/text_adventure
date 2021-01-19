using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName= "TextAdventure/InputActions/Go")]
public class Go : ActionChoice {
	
	public override void RespondToAction (GameController controller, string[] separatedInputWords) {

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
