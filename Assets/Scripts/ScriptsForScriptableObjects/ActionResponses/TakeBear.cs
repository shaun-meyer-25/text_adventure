using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/TakeBear")]
public class TakeBear : ActionResponse
{
    public override bool DoActionResponse(GameController controller)
    {
        if (controller.roomNavigation.currentRoom.roomName == requiredString)
        {
            controller.checkpointManager.SetCheckpoint(10);
            return true;
        }

        return false;
    }
}