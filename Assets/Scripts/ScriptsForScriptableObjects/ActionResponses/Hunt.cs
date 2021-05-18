using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/Hunt")]
public class Hunt : ActionResponse
{
    public override bool DoActionResponse(GameController controller)
    {
        if (controller.roomNavigation.currentRoom.roomName == requiredString && requiredString == "north foothills")
        {
            if (controller.checkpointManager.ohmInPosition)
            {
                controller.roomNavigation.currentRoom.description = "unwise to stay here, the bear could be around, and you do not have a weapon.";
  //              controller.roomNavigation.currentRoom.roomInvestigationDescription = "unwise to stay here, the bear could be around, and you do not have a weapon.";
//
                controller.LogStringWithReturn("Ohm is distracting the beast. you plunge forward with your spear. at the last moment, your footfalls alert the bear. " +
                                               "it whirls on you and your spear misses its mark, burying itself in its leg. the wound does not seem to lessen the creature's bloodlust.");
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
        
        if (controller.roomNavigation.currentRoom.roomName == "watering hole" && controller.checkpointManager.checkpoint == 9)
        {
            controller.LogStringWithReturn("you plunge forward with all your might. you drive your spear into the beast's side as it lunges at Ohm.");
            controller.LogStringWithReturn("your piercing blow struck true...");
            controller.LogStringWithReturn("...but still the bear does not fall. the blow may be mortal given time, but ravenous hunger and battle rage drives it just as yours drives you. " +
                                           "it swipes at you and you must recoil. Ohm cries out in pain beneath the beast's large paw. desperation clouds your mind.");

            controller.checkpointManager.SetCheckpoint(11);
            return true;

        }
        
        return false;
    }
}
