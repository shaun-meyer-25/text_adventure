using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName= "TextAdventure/InputActions/Go")]
public class Go : ActionChoice {
	
	public override void RespondToAction (GameController controller, string[] separatedInputWords) {

		if (separatedInputWords.Length == 1) {
			List<ExitChoice> exits = new List<ExitChoice>();

			for (int i = 0; i < controller.roomNavigation.currentRoom.Exits.Length; i++)
			{
				ExitChoice choice = CreateInstance<ExitChoice>();
				choice.keyword = controller.roomNavigation.currentRoom.Exits[i].keyString;
				exits.Add(choice);
			}
		
			if (exits != null && exits.Count > 0) {
				controller.LogStringWithReturn("Go where?");
				controller.UpdateRoomChoices(exits.ToArray());
			}
		} else {
			if (separatedInputWords[1] == "sleep" && controller.checkpointManager.checkpoint == 5)
			{
				controller.levelLoader.LoadScene("First Dream");
				Room homeCave = controller.allRoomsInGame.Find(o => o.roomName == "home cave");
				Room outside = controller.allRoomsInGame.Find(o => o.roomName == "outside home");
				Exit e = new Exit();
				e.exitDescription = "it is night out. leaving the cave is dangerous, but it must be done";
				e.keyString = "outside";
				e.valueRoom = outside;
				controller.roomNavigation.currentRoom.SetExitsInRoom(new Exit[] {e});
			}
			else
			{
				controller.roomNavigation.AttemptToChangeRooms(separatedInputWords[1]);
			}
		}
	}
}
