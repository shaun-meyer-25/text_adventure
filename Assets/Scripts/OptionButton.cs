using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour {

	public GameController controller;
	public AudioSource audioSource;
	private Button button;
	
	void Awake () {
		button = gameObject.GetComponent<Button>();
		button.onClick.AddListener(TaskOnClick);
		audioSource = gameObject.GetComponent<AudioSource>();
	}

	void TaskOnClick()
	{
		if (SceneManager.GetActiveScene().name == "Main")
		{
			Text textObject = button.GetComponentInChildren<Text>();
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
		} else if (SceneManager.GetActiveScene().name == "Experimental") {
			Text textObject = button.GetComponentInChildren<Text>();
			string text = textObject.text;

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
			else if (text == "observe" || text == "use" || text == "interact")
			{
				audioSource.Play();
				controller.LogStringWithReturn("<color=purple>" + ManipulationEffects.RandomDistortedString(controller) + "</color>");
				IEnumerator coroutine = ManipulationEffects.MessWithBackground(controller);
				StartCoroutine(coroutine);
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
}
