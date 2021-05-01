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
            controller.checkpointManager.checkpoint == 9)
        {
            controller.fifthButton.gameObject.SetActive(true);
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
