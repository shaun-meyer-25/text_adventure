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
                return true;
            }
            else
            {
                controller.LogStringWithReturn("the bear stares you down as you attempt to stab it with your spear. it rears up on its haunches. " +
                                               "ohm cries out. as the bear lunges at you, your spear buries into its mass, but you are pinned to the ground.");
                controller.LogStringWithReturn("\nthe bear roars in pain and anger, then buries its teeth into your neck. red pain clouds your vision," +
                                               "you can't breathe as you feel it tearing out your throat.");
                controller.YouAreDead();
            }
        }

        return false;
    }
}
