using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Observe")]
public class Observe : ActionChoice
{
    public override void RespondToAction (GameController controller, string[] separatedInputWords) {

        if (separatedInputWords.Length == 1) {
            
            for (int i = 0; i < controller.observableChoices.Length; i++)
            {
                controller.LogStringWithReturn("Observe what?");
                controller.UpdateRoomChoices(controller.observableChoices);
            }
            
        } else if (separatedInputWords.Length == 2) {
            if (separatedInputWords[1].Equals("object"))
            {
                controller.LogStringWithReturn("Observe which object?");
                controller.UpdateRoomChoices(controller.roomNavigation.currentRoom.interactableObjectsInRoom);
            }
            else
            {
                InteractableObject[] interactableObjects = controller.roomNavigation.currentRoom.interactableObjectsInRoom;
                for (int i = 0; i < interactableObjects.Length; i++)
                {
                    if (interactableObjects[i].keyword == separatedInputWords[1])
                    {
                        controller.LogStringWithReturn(interactableObjects[i].description);
                        controller.UpdateRoomChoices(controller.startingActions);
                    }
                }                
            }
        }
    }
}

