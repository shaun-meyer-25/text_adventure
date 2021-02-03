using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponse/MakeConversation")]
public class MakeConversation : ActionResponse
{
    public override bool DoActionResponse(GameController controller)
    {
        return true;
    }
}

/*
[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/FeedFire")]
public class FeedFire : ActionResponse
{
    public override bool DoActionResponse(GameController controller)
    {
        if (controller.roomNavigation.currentRoom.roomName == requiredString)
        {
            bool fireWasFed = controller.fire.FeedFire();
            if (fireWasFed)
            {
                controller.checkpointManager.SetCheckpoint(1);
                //controller.roomNavigation.currentRoom.InteractableObjectsInRoom
            }
            return true;
        }

        return false;
    }
}
*/