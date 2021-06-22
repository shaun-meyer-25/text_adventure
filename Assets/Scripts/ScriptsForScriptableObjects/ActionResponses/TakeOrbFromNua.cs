using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOrbFromNua : ActionResponse
{
    public override bool DoActionResponse(IController controller)
    {
        // todo - screen flash
        controller.LogStringWithReturn("you wrench the orb out of the young one's hands. they fall hard on the ground as their grasp on it slips.");
        controller.LogStringWithReturn("Onah shrieks. they grab Nua by the hand and run out of the cave.");
        controller.LogStringWithReturn(
            "Ohm rises to their feet. they give you a harsh glance, then motion for you to try to follow after. you need to make this right.");
        controller.checkpointManager.SetCheckpoint(15);
        controller.UpdateRoomChoices(controller.startingActions);
        controller.isInteracting = false;
        return true;
    }
}
