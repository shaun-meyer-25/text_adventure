using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/TakeBear")]
public class TakeBear : ActionResponse
{
    
    // todo - REMOVE THIS, ITS NOT USED
    public override bool DoActionResponse(IController controller)
    {
        if (controller.roomNavigation.currentRoom.roomName == requiredString)
        {
            controller.checkpointManager.SetCheckpoint(12);
            return true;
        }

        return false;
    }
}