using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Look")]
public class Look : InputAction
{

    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        // the second word is keyword for the room (grab Skull) (go North)
        //controller.roomNavigation.AttemptToChangeRooms(separatedInputWords[1]);
        Room room = controller.roomNavigation.currentRoom;
        controller.LogStringWithReturn("You see " + room.roomInvestigationDescription);

    }
}
