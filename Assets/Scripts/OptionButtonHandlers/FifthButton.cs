using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            Controller.volumeManipulation.EffectStart(Controller, "enableBloom");
            Controller.levelLoader.LoadSceneOrb("KillBearWithOrb");
        } else if (Controller.roomNavigation.currentRoom.roomName == "south forest" &&
            Controller.checkpointManager.checkpoint == 13)
        {
            Controller.LogStringWithReturn("<color=purple>it senses your desperation. let it into your mind, let it show you the way.</color>");
            Controller.volumeManipulation.EffectStart(Controller, "enableBloom");
            Controller.levelLoader.LoadSceneOrb("Find Tei Maze");
        }
        else
        {
            Controller.LogStringWithReturn("the orb pulses slightly.");
            Controller.DisplayLoggedText();
        }
        
        
    }
    
    IEnumerator ChangeSceneAfter3()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Find Tei Maze");
    }
}
