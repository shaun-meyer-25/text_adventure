using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/UseOrbFirstTime")]
public class UseOrbFirstTime : ActionResponse
{
    public override bool DoActionResponse(IController controller)
    {
        if (requiredString == controller.roomNavigation.currentRoom.roomName &&
            controller.interactableItems.nounsInInventory.Contains("spear") &&
            controller.checkpointManager.checkpoint == 10)
        {
            controller.fifthButton.gameObject.SetActive(true);
            controller.LogStringWithReturn("<color=purple>it senses your need. it ought to be used.</color>");
            // todo - pulse purple single time
            return true;
        }
        else
        {
            controller.LogStringWithReturn("it does not seem active.");
            return false;
        }
    }
}
