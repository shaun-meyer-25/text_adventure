using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOrbFromNua : ActionResponse
{
    public override bool DoActionResponse(IController controller)
    {
        // todo - screen flash
        controller.LogStringWithReturn("you wrench the orb out of the young one's hands. they fall hard on the ground as their grasp on it slips.");
        controller.LogStringWithReturn("Onah shrieks. they grab Nua by the hand and run out of the cave. you don't know why you reacted so aggressively.");
        controller.checkpointManager.SetCheckpoint(15);
        controller.UpdateRoomChoices(controller.startingActions);
        return true;
    }
}
