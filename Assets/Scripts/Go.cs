using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName= "TextAdventure/InputActions/Go")]
public class Go : InputAction {

	// todo - this is the class i need to workshop. need to figure out how to transition to an "exiting" state after "go"
	
	public override void RespondToInput (GameController controller, string[] separatedInputWords) {
		Exit[] exits = controller.roomNavigation.currentRoom.exits;
		
		if (exits != null && exits.Length > 0) {
			controller.LogStringWithReturn("Go where?");
			controller.UpdateRoomChoices(exits);
		}
		
		// the second word is keyword for the room (grab Skull) (go North)
		bool changed = controller.roomNavigation.AttemptToChangeRooms(separatedInputWords[1]);

		if (changed) {
			
		}
	}
}
