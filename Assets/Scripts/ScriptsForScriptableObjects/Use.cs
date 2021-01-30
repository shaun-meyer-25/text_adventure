using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName= "TextAdventure/InputActions/Use")]
public class Use : ActionChoice
{
    public override void RespondToAction(GameController controller, string[] separatedInputWords)
    {
        if (separatedInputWords.Length == 1)
        {
            if (controller.interactableItems.InteractableObjectsInInventory().Count > 0)
            {
                controller.LogStringWithReturn("Use what?");
                controller.UpdateRoomChoices(controller.interactableItems.InteractableObjectsInInventory().ToArray());
                controller.isUsing = true;
            }
            else
            {
                controller.LogStringWithReturn("There is nothing in your inventory");
            }
        }
        else if (separatedInputWords.Length == 2)
        {
            controller.interactableItems.UseItem(separatedInputWords);
            controller.UpdateRoomChoices(controller.startingActions);
            controller.isUsing = false;
        }
    }
}
