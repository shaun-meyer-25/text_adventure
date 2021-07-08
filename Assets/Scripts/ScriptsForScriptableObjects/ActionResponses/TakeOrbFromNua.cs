using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOrbFromNua : ActionResponse
{
    public override bool DoActionResponse(IController controller)
    {
        
        controller.volumeManipulation.EffectStart(controller, "respondToNua");
        controller.checkpointManager.SetCheckpoint(15);
        controller.UpdateRoomChoices(controller.startingActions);
        controller.isInteracting = false;
        return true;
    }
}
