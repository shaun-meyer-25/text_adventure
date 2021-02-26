using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/OhmGetInPosition")]
public class OhmGetInPosition : ActionResponse
{
    public override bool DoActionResponse(GameController controller)
    {
        if (controller.roomNavigation.currentRoom.roomName == requiredString)
        {
            controller.checkpointManager.ohmInPosition = true;
            controller.LogStringWithReturn("ohm circles around to distract the bear. its gaze follows him.");
            
            // todo - fix
            controller.roomNavigation.currentRoom.description = "something else";
            controller.roomNavigation.currentRoom.roomInvestigationDescription = "some other thing";
            
            //List<Interaction> interactions =
                //new List<Interaction>(controller.roomNavigation.currentRoom.PeopleInRoom[0].interactions);
            //Interaction interaction = interactions.Find(o => o.action.keyword.Equals("interact"));
            //interaction.textResponse = ""
            return true;
        }

        return false;
    }
}
