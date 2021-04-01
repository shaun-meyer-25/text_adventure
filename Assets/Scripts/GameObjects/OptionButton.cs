using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour {

	public GameController controller;
	public AudioSource audioSource;
	[FormerlySerializedAs("handler")] public IOptionButtonHandler _handler;
	private Button button;
	
	void Awake () {
		button = gameObject.GetComponent<Button>();
		button.onClick.AddListener(TaskOnClick);
		audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	void TaskOnClick()
	{
		Text textObject = button.GetComponentInChildren<Text>();
		_handler.Handle(controller, textObject, audioSource);
	}
}
