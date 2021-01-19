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
		int numOfExits = controller.exitNames.Count;
		
		if (controller.exitNames.Contains(text)) {
			for (int i = 0; i < numOfExits; i++) {
				Choice choice = controller.exitChoices [i];
			
				if (choice.keyword == text) {
					ScriptableObject.CreateInstance<Go>().RespondToAction(controller, new string[] { "go", choice.keyword });
					break;
				}
			}
		}
		else
		{
			// todo - only doing GO right now. we're iterating over current available actions and probably do not need to do that. could simply do lookup by name maybe?
			// i think we're calling the "GO" method twice and that's causing the far navigation 
			
			for (int i = 0; i < controller.actions.Length; i++) {
				var choice = controller.actions [i];
				
				if (choice.keyword == text) {
					ActionChoice actionChoice = (ActionChoice) choice; 
					actionChoice.RespondToAction (controller, new string[] { "go" });
				}
			}	
		}
	
		
		controller.DisplayLoggedText ();
	}
}
