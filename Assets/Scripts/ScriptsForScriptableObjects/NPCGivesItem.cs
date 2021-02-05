using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/NPCGivesItem")]
public class NPCGivesItem : ActionResponse
{
    public override bool DoActionResponse(GameController controller)
    { 
        InteractableObject item =
               InteractableObject.Instantiate(
                   controller.interactableItems.usableItemList.Find(o => o.noun == requiredString));
           
        controller.interactableItems.nounsInInventory.Add(requiredString);
        controller.interactableItems.AddActionResponsesToUseDictionary();
        controller.LogStringWithReturn("you are given a " + requiredString);
        return true;
    }

    public ScriptableObject SetRequiredString(string requiredString)
    {
        this.requiredString = requiredString;
        return this;
    }
}
