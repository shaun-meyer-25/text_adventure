using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/TakeSpearSecondTime")]
public class TakeSpearSecondTime : ActionResponse
{
    public override bool DoActionResponse(IController controller)
    {
        controller.checkpointManager.SetCheckpoint(9);
        return true;
    }
}
