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
			for (int i = 0; i < controller.exitNames.Count; i++) {
				Choice choice = controller.exitChoices [i];
			
				if (choice.keyword == text) {
					ScriptableObject.CreateInstance<Go>().RespondToInput(controller, new string[] { "go", choice.keyword });
				}
			}
		}
		else
		{
			// todo - only doing GO right now. we're iterating over current available actions and probably do not need to do that. could simply do lookup by name maybe?
			
			for (int i = 0; i < controller.actions.Length; i++) {
				var choice = controller.actions [i];
				
				if (choice.keyword == text) {
					InputAction inputAction = (InputAction) choice; 
					inputAction.RespondToInput (controller, new string[] { "go" });
				}
			}	
		}
	
		
		controller.DisplayLoggedText ();
	}
}
