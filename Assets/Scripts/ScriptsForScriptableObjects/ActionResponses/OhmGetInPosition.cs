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
            //controller.roomNavigation.currentRoom.description = "something else";
            controller.roomNavigation.currentRoom.roomInvestigationDescription = "the bear is focused on ohm. now is your chance to strike.";

            return true;
        }

        return false;
    }
    
    public ScriptableObject SetRequiredString(string requiredString)
    {
        this.requiredString = requiredString;
        return this;
    }
}
