using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ConversationHandler 
{
    public static void HandleConversation(GameController controller, string response)
    {
        if (response == "no" && controller.checkpointManager.checkpoint == 8)
        {
            controller.LogStringWithReturn("Ohm looks at you strangely, but does not protest.");
            SetOhmInteraction(controller);
            
            controller.checkpointManager.SetCheckpoint(9);
        }

        if (response == "yes" && controller.checkpointManager.checkpoint == 8)
        {
            controller.volumeManipulation.EffectStart(controller, "firstOrbEncounter");
            controller.LogStringWithReturn("<color=purple>you refuse to hand it to them</color>");
            controller.LogStringWithReturn("Ohm looks at you strangely, but does not protest.");
            SetOhmInteraction(controller);
            
            controller.checkpointManager.SetCheckpoint(9);
        }
        
        controller.isConversing = false;
        controller.UpdateRoomChoices(controller.startingActions);
    }

    private static void SetOhmInteraction(GameController controller)
    {
        List<Interaction> interactions =
            new List<Interaction>(controller.characters.First(o => o.noun.Equals("Ohm")).interactions);
        Interaction interaction = interactions.Find(o => o.action.keyword.Equals("interact"));
        interaction.textResponse = "they look at you strangely.";
        interaction.actionResponse = null;
    }
}
