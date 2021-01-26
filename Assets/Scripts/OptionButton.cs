using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour {

	public GameController controller;
	private Button button;
	
	void Awake () {
		button = gameObject.GetComponent<Button>();
		button.onClick.AddListener(TaskOnClick);
	}
	void TaskOnClick() {		
		Text textObject = button.GetComponentInChildren<Text>();
		string text = textObject.text;
		
		if (controller.exitNames.Contains(text)) {
			int numOfExits = controller.exitNames.Count;
			for (int i = 0; i < numOfExits; i++) {
				Choice choice = controller.exitChoices [i];
			
				if (choice.keyword == text) 
				{
					ScriptableObject.CreateInstance<Go>().RespondToAction(controller, new string[] { "go", choice.keyword });
					break;
				}
			}
		}
		// this could be a problem, as the objects can either be Observed or Taken 
		else if ((controller.ObservableChoiceNames().Contains(text) || controller.roomNavigation.currentRoom.ObjectNames().Contains(text)) && controller.isObserving) 
		{
			for (int i = 0; i < controller.ObservableChoiceNames().Count; i++)
			{
				Choice choice = controller.actions[i];

				if (choice.keyword == text)
				{
					ScriptableObject.CreateInstance<Observe>().RespondToAction(controller, new string[] {"observe", choice.keyword });
				}
			}
		}
		else if ((controller.InteractChoiceNames().Contains(text) || controller.roomNavigation.currentRoom.ObjectNames().Contains(text)) && controller.isInteracting) 
		{
			for (int i = 0; i < controller.InteractChoiceNames().Count; i++)
			{
				Choice choice = controller.actions[i];

				if (choice.keyword == text)
				{
					ScriptableObject.CreateInstance<Interact>().RespondToAction(controller, new string[] {"interact", choice.keyword });
				}
			}
		}
		else 
		{
			// todo - why does "go" have a special case? define that in a comment or refactor it
			
			for (int i = 0; i < controller.actions.Length; i++) {
				var choice = controller.actions [i];
				
				if (choice.keyword == text && text == "go") {
					ActionChoice actionChoice = (ActionChoice) choice; 
					actionChoice.RespondToAction (controller, new string[] { "go" });
				} else if (choice.keyword == text)
				{
					ActionChoice actionChoice = (ActionChoice) choice; 
					actionChoice.RespondToAction (controller, new string[] { choice.keyword });
				} 
			}	
		}
	
		
		controller.DisplayLoggedText ();
	}
}
