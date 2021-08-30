using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelButton : MonoBehaviour
{
    public IController Controller;
    private Button button;

    private void Awake()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        if (!Controller.isConversing)
        {
            Controller.isInteracting = false;
            Controller.isObserving = false;
            Controller.isUsing = false;
            Controller.isConversing = false;
            Controller.UpdateRoomChoices(Controller.startingActions);
        }
    }
}
