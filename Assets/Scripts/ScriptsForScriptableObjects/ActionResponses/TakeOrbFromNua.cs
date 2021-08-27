using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeOrbFromNua : ActionResponse
{
    public override bool DoActionResponse(IController controller)
    {
        controller.checkpointManager.SetCheckpoint(15);

        return true;
    }
}
