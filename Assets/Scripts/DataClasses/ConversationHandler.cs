using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public static class ConversationHandler 
{
    public static void HandleConversation(IController controller, string response)
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

        if (response == "yes" && controller.checkpointManager.checkpoint == 16)
        {
            controller.isSecondButtonDisabled = true;
            GameObject button = GameObject.Find("Option2");
            button.GetComponent<Button>().interactable = false;
            controller.buttonWrapAnimator.SetTrigger("WrapButton");
            controller.LogStringWithReturn("<color=purple>ohm reaches for the orb. push his hand away.</color>");
            return;
        }

        if (response == "no" && controller.checkpointManager.checkpoint == 16)
        {
            if (!controller.isSecondButtonDisabled)
            {
                controller.isSecondButtonDisabled = true;
                GameObject button = GameObject.Find("Option2");
                button.GetComponent<Button>().interactable = false;
                controller.buttonWrapAnimator.SetTrigger("WrapButton");
            }

            controller.LogStringWithReturn("you shove their hand away. they fall backwards. a shout, a look of shock, disbelief in their eyes. then a loud crack, an explosion of red on the rocks.");
            controller.LogStringWithReturn(
                "Onah screams. it cuts through the sound of the storm. they flee further into the mountains with Nua at their side.");
            controller.LogStringWithReturn(
                "<color=purple>you can not return without the others. the one you care for will think you a monster. the one that fell is fine. go after the others.</color>");
            controller.checkpointManager.SetCheckpoint(17);

        }
        
        if (response == "no" && controller.checkpointManager.checkpoint == 14)
        {
            controller.LogStringWithReturn("Ohm looks at you strangely, but does not protest.");
            SetOhmInteraction(controller);
            
            controller.checkpointManager.SetCheckpoint(9);
        }

        if (response == "yes" && controller.checkpointManager.checkpoint == 14)
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

    private static void SetOhmInteraction(IController controller)
    {
        List<Interaction> interactions =
            new List<Interaction>(controller.characters.First(o => o.noun.Equals("Ohm")).interactions);
        Interaction interaction = interactions.Find(o => o.action.keyword.Equals("interact"));
        interaction.textResponse = "they look at you strangely.";
        interaction.actionResponse = null;
    }
}
