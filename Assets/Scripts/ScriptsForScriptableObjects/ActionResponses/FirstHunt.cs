using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/FirstHunt")]
public class FirstHunt : ActionResponse
{
    public override bool DoActionResponse(GameController controller)
    {
        if (controller.roomNavigation.currentRoom.roomName == requiredString)
        {
            if (controller.checkpointManager.ohmInPosition)
            {
                controller.roomNavigation.currentRoom.description = "unwise to stay here, the bear could be around, and you do not have a weapon.";
                controller.roomNavigation.currentRoom.roomInvestigationDescription = "unwise to stay here, the bear could be around, and you do not have a weapon.";

                controller.LogStringWithReturn("ohm is distracting the beast. you plunge forward with your spear. at the last minute, " +
                                               "the bear whirls, snapping the spear. it was too fast, too strong for you. you are defenseless, and must run.");
                List<Interaction> interactions =
                    new List<Interaction>(controller.characters.First(o => o.noun.Equals("ohm")).interactions);
                Interaction interaction = interactions.Find(o => o.action.keyword.Equals("interact"));
                interaction.textResponse = "'RUN'";
            }
            else
            {
                controller.BearKillsYou();
            }
        }
        return true;
    }
}
