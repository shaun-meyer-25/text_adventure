using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorCollider : MonoBehaviour
{
    public FinalCaveController Controller;
    
    // Start is called before the first frame update
    void Start()
    {
        // todo - add collider behaviors for bat box and also for snake heads
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Bats" && Controller.roomNavigation.currentRoom.roomName == "bat room" && Controller.BatsWatching)
        {
            Debug.Log("BATS");
            Controller.BatsFlyingStart();
            Controller.audio.Play();
        }
    }
}
