using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/ForagingBerries")]
public class ForagingBerries : ActionResponse
{
    public override bool DoActionResponse(GameController controller)
    {
        List<Interaction> interactions =
            new List<Interaction>(controller.characters.First(o => o.noun.Equals("ohm")).interactions);
        Interaction interaction = interactions.Find(o => o.action.keyword.Equals("interact"));
        interaction.textResponse = "they are silent, staring at the ground.";
        
        controller.SetNighttime();
        controller.isDaytime = false;
        return true;
    }
}