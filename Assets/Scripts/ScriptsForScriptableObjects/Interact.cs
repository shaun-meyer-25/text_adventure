using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Interact")]
public class Interact : ActionChoice
{
    public override void RespondToAction (GameController controller, string[] separatedInputWords) {

        if (separatedInputWords.Length == 1) {
            controller.LogStringWithReturn("Interact with what?");
            controller.UpdateRoomChoices(controller.interactableChoices);
            controller.isInteracting = true;

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
                if (controller.roomNavigation.currentRoom.PeopleInRoom.Length > 0)
                {
                    controller.LogStringWithReturn("Which person?");
                    controller.UpdateRoomChoices(controller.roomNavigation.currentRoom.PeopleInRoom);
                }
                else
                {
                    controller.LogStringWithReturn("Nobody around yet");
                    controller.UpdateRoomChoices(controller.startingActions);
                    controller.isInteracting = false;
                }
            }
            else if (controller.roomNavigation.currentRoom.CharacterNamesInRoom().Contains(separatedInputWords[1]))
            {
                for (int i = 0; i < controller.roomNavigation.currentRoom.PeopleInRoom.Length; i++)
                {
                    InteractableObject character = controller.roomNavigation.currentRoom.PeopleInRoom[i];
                    if (character.keyword.Equals(separatedInputWords[1]))
                    {
                        controller.LogStringWithReturn(character.description);
                        List<Interaction> interactions = new List<Interaction>(controller.characters[i].interactions);
                        Interaction interaction = interactions.Find(o => o.action.keyword.Equals("interact"));
                        if (!(interaction.ActionResponse == null))
                        {
                            interaction.ActionResponse.DoActionResponse(controller);
                        }
                        controller.UpdateRoomChoices(controller.startingActions);
                        controller.isInteracting = false;
                    }
                }
            }
            else
            {
                Dictionary<string, string> takeDictionary = controller.interactableItems.Take(separatedInputWords);

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
