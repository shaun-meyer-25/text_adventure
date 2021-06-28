using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FifthButton : MonoBehaviour
{

    public IController Controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (Controller.checkpointManager.checkpoint == 10 &&
            Controller.roomNavigation.currentRoom.roomName == "watering hole")
        {
            Controller.levelLoader.LoadScene("KillBearWithOrb");
        }
        else
        {
            Controller.LogStringWithReturn("the orb pulses slightly.");
            Controller.DisplayLoggedText();
        }
        
        
    }
}
