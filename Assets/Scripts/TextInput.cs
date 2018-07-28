 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour {
/*
	public InputField inputField;

	GameController controller;

	void Awake() {
		controller = GetComponent<GameController> ();
		inputField.onEndEdit.AddListener (AcceptStringInput);
	}

	void AcceptStringInput(string userInput) {
		userInput = userInput.ToLower ();
		controller.LogStringWithReturn (userInput);

		char[] delimiterCharacters = { ' ' };
		string[] separatedInputWords = userInput.Split (delimiterCharacters);

		for (int i = 0; i < controller.roomActions.Length; i++) {
			Choice inputAction = controller.roomActions [i];
			if (inputAction.keyword == separatedInputWords [0]) {
				inputAction.RespondToInput (controller, separatedInputWords);
			}
		}

		InputComplete ();
	}

	void InputComplete() {
		controller.DisplayLoggedText ();

		// Activate Input Field, because when you hit return on an input field it takes the focus away from 
		//    the input field (no longer active)
		//
		// we don't want to do that, this is a text based game, reactivate it automatically
		inputField.ActivateInputField ();
		inputField.text = null;
	}
	*/
}
