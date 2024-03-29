using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/TakeOrb")]
public class TakeOrb : ActionResponse
{
    public override bool DoActionResponse(IController controller)
    {
        
        controller.volumeManipulation.EffectEnd(controller, "firstOrbEncounter");
        controller.checkpointManager.SetCheckpoint(7);
        SteamAchivements sa = FindObjectOfType<SteamAchivements>();
        if (sa != null)
        {
            sa.SetAchievement("GOT_ORB");
        }
        return true;
    }
}
