using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/Hunt")]
public class Hunt : ActionResponse
{
    public override bool DoActionResponse(IController controller)
    {
        if (controller.roomNavigation.currentRoom.roomName == requiredString && requiredString == "north foothills")
        {
            if (controller.checkpointManager.ohmInPosition)
            {
                controller.roomNavigation.currentRoom.description = "unwise to stay here, the bear could be around, and you do not have a weapon.";

                controller.LogStringWithReturn("Ohm is distracting the beast. you plunge forward with your spear, but your footfalls alert the bear. " +
                                               "it whirls on you and your spear misses its mark, burying itself in its leg. the wound does not seem to lessen the creature's bloodlust.");

                // todo - could be more interesting trying to do a thing where your spear gets stuck in the ground, if you try to retrieve it the bear kills you
                
                controller.checkpointManager.SetCheckpoint(4);
                
                List<Interaction> interactions =
                    new List<Interaction>(controller.characters.First(o => o.noun.Equals("Ohm")).interactions);
                Interaction interaction = interactions.Find(o => o.action.keyword.Equals("interact"));
                interaction.textResponse = "'RUN'";
            }
            else
            {
                controller.BearKillsYou();
            }
            return true;

        }
        
        if (controller.roomNavigation.currentRoom.roomName == "watering hole" && controller.checkpointManager.checkpoint == 10)
        {
            controller.LogStringWithReturn("<color=purple>you cannot do this alone.</color>");
            controller.LogStringWithReturn("your confidence falters. you cannot try the same thing you did yesterday.");
            return false;
        }
        
        return false;
    }
}
