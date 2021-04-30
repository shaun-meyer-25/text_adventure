using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/NPCGivesItem")]
public class NPCGivesItem : ActionResponse
{
    public override bool DoActionResponse(GameController controller)
    { 
        if (!controller.interactableItems.nounsInInventory.Contains(requiredString)) {
            InteractableObject item =
                Instantiate(
                    controller.interactableItems.usableItemList.Find(o => o.noun == requiredString));

            controller.interactableItems.nounsInInventory.Add(requiredString);
            controller.interactableItems.AddActionResponsesToUseDictionary();
            controller.LogStringWithReturn("you are given a " + requiredString);

            return true;
        }

        return false;
    }

    public NPCGivesItem SetRequiredString(string requiredString)
    {
        this.requiredString = requiredString;
        return this;
    }
}
