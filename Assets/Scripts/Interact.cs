using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Interact")]
public class Interact : ActionChoice
{
    public override void RespondToAction (GameController controller, string[] separatedInputWords) {

        if (separatedInputWords.Length == 1) {
            controller.LogStringWithReturn("Interact with what?");
            for (int i = 0; i < controller.observableChoices.Length; i++)
            {
                controller.UpdateRoomChoices(controller.interactableChoices);
                controller.isInteracting = true;
            }
            
        } else if (separatedInputWords.Length == 2) {
            if (separatedInputWords[1].Equals("object"))
            {
                if (controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Length > 0)
                {
                    controller.LogStringWithReturn("Which object?");
                    controller.UpdateRoomChoices(controller.roomNavigation.currentRoom.InteractableObjectsInRoom);
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
                //             ((List<InteractableObject>) controller.roomNavigation.currentRoom.interactableObjectsInRoom).RemoveAll(o => o.noun == noun)


                if (takeDictionary != null)
                {
                    controller.LogStringWithReturn(controller.TestVerbDictionaryWithNoun(takeDictionary, separatedInputWords[0], separatedInputWords[1]));
                }

                controller.UpdateRoomChoices(controller.startingActions);
                controller.isInteracting = false;
            }
        }
    }
}
