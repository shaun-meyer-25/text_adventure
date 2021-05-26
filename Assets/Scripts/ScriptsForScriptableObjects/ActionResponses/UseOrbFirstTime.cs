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
            controller.LogStringWithReturn("you remove the orb from your pouch. time seems to slow around you. <color=purple>you should use it, it will give you the strength to do what needs to be done.</color>.");
            // todo - pulse purple single time
            Text textObject = controller.fifthButton.GetComponentInChildren<Text>();
            textObject.text = "ORB";
            return true;

        }
        else
        {
            controller.LogStringWithReturn("it does not seem active.");
            return false;
        }
    }
}
