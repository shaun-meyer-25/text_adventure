using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName= "TextAdventure/ButtonHandlers/StartingScene")]
public class StartingSceneHandler : IOptionButtonHandler
{
    override public void Handle(IController controller, Text textObject)
    {
	    string text = textObject.text;

	    // todo - we could make exitnames part of an isExiting flag like for interacting or observing, and move this block in with "go"
	    if (controller.exitNames.Contains(text))
	    {
		    int numOfExits = controller.exitNames.Count;
		    for (int i = 0; i < numOfExits; i++)
		    {
			    Choice choice = controller.exitChoices[i];

			    if (choice.keyword == text)
			    {
				    ScriptableObject.CreateInstance<Go>()
					    .RespondToAction(controller, new string[] {"go", choice.keyword});
				    break;
			    }
		    }
	    }
	    else if ((controller.ObservableChoiceNames().Contains(text) ||
	              controller.roomNavigation.currentRoom.ObjectNames().Contains(text)) && controller.isObserving)
	    {
		    for (int i = 0; i < controller.actions.Length; i++)
		    {
			    Choice choice = controller.actions[i];

			    if (choice.keyword == text)
			    {
				    ScriptableObject.CreateInstance<Observe>()
					    .RespondToAction(controller, new string[] {"observe", choice.keyword});
			    }
		    }
	    }
	    else if (controller.interactableItems.nounsInInventory.Contains(text) && controller.isUsing)
	    {
		    ScriptableObject.CreateInstance<Use>().RespondToAction(controller, new string[] {"use", text});
	    }
	    else if (controller.isInteracting)
	    {
		    for (int i = 0; i < controller.actions.Length; i++)
		    {
			    Choice choice = controller.actions[i];

			    if (choice.keyword == text)
			    {
				    ScriptableObject.CreateInstance<Interact>()
					    .RespondToAction(controller, new string[] {"interact", choice.keyword});
			    }
		    }
	    }
	    else if (controller.isConversing)
	    {
		    ConversationHandler.HandleConversation(controller, text);
	    }
	    else
	    {
		    for (int i = 0; i < controller.actions.Length; i++)
		    {
			    var choice = controller.actions[i];

			    if (choice.keyword == text)
			    {
				    ActionChoice actionChoice = (ActionChoice) choice;
				    actionChoice.RespondToAction(controller, new string[] {choice.keyword});
			    }
		    }
	    }

	    controller.DisplayLoggedText();
    }
}
