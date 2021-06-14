using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/OhmFalls")]
public class OhmFalls : ActionResponse
{
    public override bool DoActionResponse(IController controller)
    {
        if (controller.roomNavigation.currentRoom.roomName == requiredString)
        {
            controller.processingDelay = 0.09f;
            controller.LogStringWithReturn(
                "<color=purple>THEY ARE REACHING FOR THE ORB</color> you shove their hand away. they fall backwards. a shout, a look of shock, disbelief in their eyes. then a loud crack, an explosion of red on the rocks.");
            controller.LogStringWithReturn(
                "Onah screams. it cuts through the sound of the storm. they flee further into the mountains with Nua at their side.");
            controller.LogStringWithReturn(
                "<color=purple>you can not return without the others. the one you care for will think you a monster. the one that fell is fine. go after the others.</color>");
            controller.UpdateRoomChoices(controller.startingActions);
            controller.isInteracting = false;
            controller.checkpointManager.SetCheckpoint(17);
            return true;
        }
        else
        {
            controller.isInteracting = false;
            controller.LogStringWithReturn("they call out to Onah and Nua. they move slowly up the wet, jagged rocks.");
            controller.UpdateRoomChoices(controller.startingActions);
            return true;
        }
    }
}
