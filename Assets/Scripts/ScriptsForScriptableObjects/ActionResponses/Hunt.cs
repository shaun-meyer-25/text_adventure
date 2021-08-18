using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/Hunt")]
public class Hunt : ActionResponse
{
    public override bool DoActionResponse(IController controller)
    {
        if (controller.roomNavigation.currentRoom.roomName == requiredString && requiredString == "upper foothills")
        {
            if (controller.checkpointManager.ohmInPosition)
            {
                controller.roomNavigation.currentRoom.description = "unwise to stay here, the bear could be around, and you do not have a weapon.";

                controller.levelLoader.LoadScene("FirstBearEncounter");
                
//                List<Interaction> interactions =
//                    new List<Interaction>(controller.characters.First(o => o.noun.Equals("Ohm")).interactions);
//                Interaction interaction = interactions.Find(o => o.action.keyword.Equals("interact"));
//                interaction.textResponse = "Ohm has fled. you should follow.";
            }
            else
            {
                controller.LogStringWithReturn("Ohm signals at you to wait.");
                return false;
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
