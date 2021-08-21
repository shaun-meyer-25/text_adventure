using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/TakeTorch")]
public class TakeTorch : ActionResponse
{
    public override bool DoActionResponse(IController controller)
    {
        controller.checkpointManager.SetCheckpoint(18);
        return true;
    }
}
