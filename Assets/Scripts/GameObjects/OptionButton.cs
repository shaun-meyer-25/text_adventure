using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour {

	public GameController controller;
	[FormerlySerializedAs("handler")] public IOptionButtonHandler _handler;
	private Button button;
	
	void Awake () {
		button = gameObject.GetComponent<Button>();
		button.onClick.AddListener(TaskOnClick);
	}
	
	void TaskOnClick()
	{
		Text textObject = button.GetComponentInChildren<Text>();
		_handler.Handle(controller, textObject);
	}
}
