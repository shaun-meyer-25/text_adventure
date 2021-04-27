using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/TakeOrb")]
public class TakeOrb : ActionResponse
{
    public override bool DoActionResponse(GameController controller)
    {
        
        controller.volumeManipulation.EffectEnd(controller, "firstOrbEncounter");
        controller.checkpointManager.SetCheckpoint(6);
        return true;
    }
}
