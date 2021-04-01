using System.Collections;
using System.Collections.Generic;
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
                controller.LogStringWithReturn("ohm is distracting the beast. you plunge forward with your spear. at the last minute, " +
                                               "the bear whirls, snapping the spear. it was too fast, too strong for you. you are defenseless, and must run.");
            }
            else
            {
                controller.BearKillsYou();
            }
        }
        return true;
    }
}
