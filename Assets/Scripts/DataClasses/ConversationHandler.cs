using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConversationHandler 
{
    public static void HandleConversation(GameController controller, string response)
    {
        if (response == "no" && controller.checkpointManager.checkpoint == 8)
        {
            controller.LogStringWithReturn("Ohm looks at you strangely, but does not protest.");
        }

        if (response == "yes" && controller.checkpointManager.checkpoint == 8)
        {
            controller.volumeManipulation.EffectStart(controller, "firstOrbEncounter");
            controller.LogStringWithReturn("<color=purple>you refuse to hand it to them</color>");
            controller.LogStringWithReturn("Ohm looks at you strangely, but does not protest.");
        }

        controller.isConversing = false;
        controller.UpdateRoomChoices(controller.startingActions);
    }
}
