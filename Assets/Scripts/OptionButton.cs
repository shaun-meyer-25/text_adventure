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
		
		for (int i = 0; i < controller.roomActions.Length; i++) {
			Choice choice = controller.roomActions [i];
			// If the button's text matches a choice's keyword, DO that Choice
			if (choice.keyword == text) {
				if (choice.GetType().IsSubclassOf(typeof(InputAction))) {
					InputAction inputAction = (InputAction) choice; 
					inputAction.RespondToInput (controller, new string[] { "go", "outside" });
				} else if (choice.GetType().IsSubclassOf(typeof(Exit))) {
					ScriptableObject.CreateInstance<Go>().RespondToInput(controller, new string[] { "go", choice.keyword });
				}
			}
		}
		
		controller.DisplayLoggedText ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
