using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/GiveTeiShell")]
public class GiveTeiShell : ActionResponse
{
    public override bool DoActionResponse(IController controller)
    {
        if (controller.checkpointManager.checkpoint == 20)
        {
            // animator.SetTrigger("RescindTendrils");
            controller.LogStringWithReturn("you give the shell you found to Tei. they give you one they found as well. they take you in an embrace.");
            controller.LogStringWithReturn(
                "you feel whole, calm, in a way you have not felt in a long time. maybe you will survive. maybe you will all survive.");
            controller.LogStringWithReturn("you obtain Tei's shell");
            controller.interactableItems.nounsInInventory.Add("tei's shell");
            return true;
        }
        
        return false;
    }
}
