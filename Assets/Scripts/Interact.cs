using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Interact")]
public class Interact : ActionChoice
{
    public override void RespondToAction (GameController controller, string[] separatedInputWords) {

        if (separatedInputWords.Length == 1) {
            
            for (int i = 0; i < controller.observableChoices.Length; i++)
            {
                controller.LogStringWithReturn("Interact with what?");
                controller.UpdateRoomChoices(controller.interactableChoices);
                controller.isInteracting = true;
            }
            
        } else if (separatedInputWords.Length == 2) {
            if (separatedInputWords[1].Equals("object"))
            {
                if (controller.roomNavigation.currentRoom.interactableObjectsInRoom.Length > 0)
                {
                    controller.LogStringWithReturn("Which object?");
                    controller.UpdateRoomChoices(controller.roomNavigation.currentRoom.interactableObjectsInRoom);
                }
                else
                {
                    controller.LogStringWithReturn("You see no objects nearby");
                    controller.UpdateRoomChoices(controller.startingActions);
                    controller.isInteracting = false;
                }
            }
            else if (separatedInputWords[1].Equals("person"))
            {
                // todo
                controller.LogStringWithReturn("Nobody around yet");
                controller.UpdateRoomChoices(controller.startingActions);
                controller.isInteracting = false;
            }
            else
            {
                Dictionary<string, string> takeDictionary = controller.interactableItems.Take(separatedInputWords);

                controller.LogStringWithReturn("You take the " + separatedInputWords[1]);
                controller.UpdateRoomChoices(controller.startingActions);
                controller.isInteracting = false;

                // todo - do we need any of this? 
                /*
                InteractableObject[] interactableObjects = controller.roomNavigation.currentRoom.interactableObjectsInRoom;
                for (int i = 0; i < interactableObjects.Length; i++)
                {
                    if (interactableObjects[i].keyword == separatedInputWords[1])
                    {
                        controller.LogStringWithReturn(interactableObjects[i].description);
                        controller.UpdateRoomChoices(controller.startingActions);
                    }
                }     
                controller.UpdateRoomChoices(controller.startingActions); */
            }
        }
    }
}
