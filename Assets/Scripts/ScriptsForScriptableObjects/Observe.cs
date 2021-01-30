using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Observe")]
public class Observe : ActionChoice
{
    public override void RespondToAction (GameController controller, string[] separatedInputWords) {

        if (separatedInputWords.Length == 1) {
            controller.LogStringWithReturn("Observe what?");
            controller.UpdateRoomChoices(controller.observableChoices);
            controller.isObserving = true;
        } else if (separatedInputWords.Length == 2) {
            if (separatedInputWords[1].Equals("object"))
            {
                if (controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Length > 0)
                {
                    controller.LogStringWithReturn("Observe which object?");
                    controller.UpdateRoomChoices(controller.roomNavigation.currentRoom.InteractableObjectsInRoom);
                }
                else
                {
                    controller.LogStringWithReturn("You see no objects nearby");
                    controller.UpdateRoomChoices(controller.startingActions);
                    controller.isObserving = false;
                }
            }
            else if (separatedInputWords[1].Equals("inventory"))
            {
                controller.interactableItems.DisplayInventory();
                controller.isObserving = false;
                controller.UpdateRoomChoices(controller.startingActions);
            }
            else if (separatedInputWords[1].Equals("surroundings"))
            {
                Room room = controller.roomNavigation.currentRoom;
                controller.LogStringWithReturn("You see " + room.roomInvestigationDescription);
                controller.isObserving = false;
                controller.UpdateRoomChoices(controller.startingActions);
            }
            else // i.e.  "observe branch"
            {
                InteractableObject[] interactableObjects = controller.roomNavigation.currentRoom.InteractableObjectsInRoom;
                for (int i = 0; i < interactableObjects.Length; i++)
                {
                    if (interactableObjects[i].keyword == separatedInputWords[1])
                    {
                        controller.LogStringWithReturn (controller.TestVerbDictionaryWithNoun (controller.interactableItems.examineDictionary, separatedInputWords [0], separatedInputWords [1]));
                        controller.UpdateRoomChoices(controller.startingActions);
                    }
                }
                controller.isObserving = false;
            }
        }
    }
}

