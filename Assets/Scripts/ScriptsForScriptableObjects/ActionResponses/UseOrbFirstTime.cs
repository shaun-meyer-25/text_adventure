using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/UseOrbFirstTime")]
public class UseOrbFirstTime : ActionResponse
{
    public override bool DoActionResponse(GameController controller)
    {
        if (requiredString == controller.roomNavigation.currentRoom.roomName && 
            !controller.interactableItems.nounsInInventory.Contains("spear") && 
            controller.checkpointManager.checkpoint == 11)
        {
            controller.fifthButton.gameObject.SetActive(true);
            controller.LogStringWithReturn("your awareness of the situation enhances, time seems to slow down. new possibilities of how to act arise from every detail of the landscape.");
            // todo - pulse purple single time
            Text textObject = controller.fifthButton.GetComponentInChildren<Text>();
            textObject.text = "throw";
            return true;

        }
        else
        {
            controller.LogStringWithReturn("it does not seem active.");
            return false;
        }
    }
}
