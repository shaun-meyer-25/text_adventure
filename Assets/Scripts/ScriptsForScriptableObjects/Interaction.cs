using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Interaction
{
    public ActionChoice action;
    [TextArea] public string textResponse;
    [SerializeField] public ActionResponse baseActionResponse;

    [HideInInspector] public ActionResponse actionResponse;

    public ActionResponse ActionResponse
    {
	    get { return actionResponse; }
    }

    public void SetActionResponse(ActionResponse response)
    {
	    actionResponse = response;
    }
}
