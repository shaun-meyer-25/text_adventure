using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName= "TextAdventure/InputActions/Use")]
public class Use : ActionChoice
{
    public override void RespondToAction (GameController controller, string[] separatedInputWords) {
        controller.interactableItems.UseItem(separatedInputWords);
    }
}
