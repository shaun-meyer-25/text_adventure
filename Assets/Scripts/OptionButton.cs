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

		if (controller.roomActionNames.Contains(text)) {
			for (int i = 0; i < controller.roomActions.Length; i++) {
				Choice choice = controller.roomActions [i];
				Debug.Log("the choice - " + choice.keyword);
				// Right now we're only matching on room actions - exits not included. need to include them. 
			
				// If the button's text matches a choice's keyword, DO that Choice
				if (choice.keyword == text) {
					InputAction inputAction = (InputAction) choice; 
					inputAction.RespondToInput (controller, new string[] { "go" });
				}
			}			
		} else if (controller.exitNames.Contains(text)) {
			for (int i = 0; i < controller.exitNames.Count; i++) {
				Choice choice = controller.exitChoices [i];
			
				if (choice.keyword == text) {
					ScriptableObject.CreateInstance<Go>().RespondToInput(controller, new string[] { "go", choice.keyword });
				}
			}
		}
	
		
		controller.DisplayLoggedText ();
	}
}
