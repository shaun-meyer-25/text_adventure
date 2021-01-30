using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/FeedFire")]
public class FeedFire : ActionResponse
{
    public override bool DoActionResponse(GameController controller)
    {
        if (controller.roomNavigation.currentRoom.roomName == requiredString)
        {
            bool fireWasFed = controller.fire.FeedFire();
            if (fireWasFed)
            {
                //controller.interactableItems
                InteractableObject o = controller.interactableItems.usableItemList.Find(o => o.noun.Equals("tree branch"));
                // destroying the item doesn't remove it from the inventory 
                // DestroyImmediate(o, true);
            }
            return true;
        }

        return false;
    }

/*
 *     public Room roomToChangeTo;

    public override bool DoActionResponse(GameController controller)
    {
        if (controller.roomNavigation.currentRoom.roomName == requiredString)
        {
            controller.roomNavigation.currentRoom = roomToChangeTo;
            controller.LoadRoomDataAndDisplayRoomText();
            return true;
        }

        return false;
    }
 */
}
