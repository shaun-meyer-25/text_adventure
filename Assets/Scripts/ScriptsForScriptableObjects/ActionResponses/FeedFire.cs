using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/FeedFire")]
public class FeedFire : ActionResponse
{
    public override bool DoActionResponse(IController controller)
    {
        if (controller.roomNavigation.currentRoom.roomName == requiredString)
        {
            if (controller.checkpointManager.checkpoint == 0)
            {
                controller.checkpointManager.SetCheckpoint(1);
                controller.LogStringWithReturn("you break the tree branch into smaller pieces, tossing them on the fire one at a time. " +
                                               "the noise from this begins to wake the others, who stir and rise to start their days.");
            }
            bool fireWasFed = controller.fire.FeedFire();
            
            return fireWasFed;
        }

        return false;
    }
}
