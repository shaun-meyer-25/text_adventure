using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName= "TextAdventure/InputActions/Go")]
public class Go : ActionChoice {
	
	public override void RespondToAction (GameController controller, string[] separatedInputWords)
	{
		int checkpoint = controller.checkpointManager.checkpoint;
		if (separatedInputWords.Length == 1) {
			List<ExitChoice> exits = new List<ExitChoice>();

			for (int i = 0; i < controller.roomNavigation.currentRoom.GetExits(checkpoint).Length; i++)
			{
				ExitChoice choice = CreateInstance<ExitChoice>();
				choice.keyword = controller.roomNavigation.currentRoom.GetExits(checkpoint)[i].keyString;
				exits.Add(choice);
			}
		
			if (exits.Count > 0) {
				controller.LogStringWithReturn("Go where?");
				controller.UpdateRoomChoices(exits.ToArray());
			}
		} else {
			controller.roomNavigation.AttemptToChangeRooms(separatedInputWords[1]);
		}
	}
}
