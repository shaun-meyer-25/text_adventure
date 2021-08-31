using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Interact")]
public class Interact : ActionChoice
{
    public override void RespondToAction(IController controller, string[] separatedInputWords) {

        if (separatedInputWords.Length == 1) {
            controller.LogStringWithReturn("interact with what?");
            controller.UpdateRoomChoices(controller.interactableChoices);
            controller.isInteracting = true;

        } else if (separatedInputWords.Length == 2) {
            if (separatedInputWords[1].Equals("object"))
            {
                if (controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Length > 0)
                {
                    controller.LogStringWithReturn("which object?");
                    controller.UpdateRoomChoices(controller.roomNavigation.currentRoom.InteractableObjectsInRoom);
                }
                else
                {
                    controller.LogStringWithReturn("you see no objects nearby.");
                    controller.UpdateRoomChoices(controller.startingActions);
                    controller.isInteracting = false;
                }
            }
            else if (separatedInputWords[1].Equals("person"))
            {
                if (controller.roomNavigation.currentRoom.PeopleInRoom.Length > 0)
                {
                    
                    controller.LogStringWithReturn("which person?");
                    controller.UpdateRoomChoices(controller.roomNavigation.currentRoom.PeopleInRoom);
                }
                else
                {
                    if ((controller.checkpointManager.checkpoint == 0 || controller.checkpointManager.checkpoint == 6 || controller.checkpointManager.checkpoint == 7) 
                        && controller.roomNavigation.currentRoom.roomName == "home cave")
                    {
                        controller.LogStringWithReturn("best not to wake anyone right now.");
                        controller.UpdateRoomChoices(controller.startingActions);
                        controller.isInteracting = false;
                    }
                    else
                    {
                        controller.LogStringWithReturn("there is no one around.");
                        controller.UpdateRoomChoices(controller.startingActions);
                        controller.isInteracting = false;
                    }
                }
            }
            else if (controller.roomNavigation.currentRoom.CharacterNamesInRoom().Contains(separatedInputWords[1]))
            {
                for (int i = 0; i < controller.roomNavigation.currentRoom.PeopleInRoom.Length; i++)
                {
                    InteractableObject character = controller.roomNavigation.currentRoom.PeopleInRoom[i];
                    if (character.keyword.Equals(separatedInputWords[1]))
                    {
                        List<Interaction> interactions = new List<Interaction>(controller.roomNavigation.currentRoom.PeopleInRoom [i].interactions);
                        Interaction interaction = interactions.Find(o => o.action.keyword.Equals("interact"));
                        controller.LogStringWithReturn(interaction.textResponse);
                        if (!(interaction.ActionResponse == null))
                        {
                            interaction.ActionResponse.DoActionResponse(controller);
                        }
                        else
                        {
                            controller.UpdateRoomChoices(controller.startingActions);
                            controller.isInteracting = false;   
                        }
                    }
                }
            }
            else
            {
                Dictionary<string, string> takeDictionary = controller.interactableItems.Take(separatedInputWords);

                if (takeDictionary != null)
                {
                    controller.LogStringWithReturn(controller.TestVerbDictionaryWithNoun(takeDictionary, separatedInputWords[0], separatedInputWords[1]));
                    InteractableObject obj = controller.interactableItems.usableItemList
                        .Find(o => o.noun == separatedInputWords[1]);
                    Interaction interaction =
                        new List<Interaction>(obj.interactions).Find(o => o.action.keyword.Equals("interact"));
                    if (!(interaction.ActionResponse == null))
                    {
                        interaction.ActionResponse.DoActionResponse(controller);
                    }
                }
                else
                {
                    InteractableObject obj = controller.interactableItems.interactableOnly
                        .Find(o => o.noun == separatedInputWords[1]);
                    Interaction interaction =
                        new List<Interaction>(obj.interactions).Find(o => o.action.keyword.Equals("interact"));
                    if (!(interaction.ActionResponse == null))
                    {
                        interaction.ActionResponse.DoActionResponse(controller);
                    }
                }

                controller.UpdateRoomChoices(controller.startingActions);
                controller.isInteracting = false;
            }
        }
    }
}
