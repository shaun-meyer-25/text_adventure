using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/UseBatDroppings")]
public class UseBatDroppings : ActionResponse
{
    public override bool DoActionResponse(IController controller)
    {
        
        SteamAchivements sa = FindObjectOfType<SteamAchivements>();
        if (sa != null)
        {
            sa.SetAchievement("EXPLORER");
        }
        controller.LogStringWithReturn("you apply the droppings to your torch.");
        FinalCaveController cont = (FinalCaveController) controller;
        cont.Torch.pointLightOuterRadius = 6.5f;
        return true;
    }
}
