using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeiLevelTransition : MonoBehaviour
{
    public IController controller;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "Player")
        {
            SteamAchivements sa = FindObjectOfType<SteamAchivements>();
            if (sa != null)
            {
                sa.SetAchievement("FOUND_TEI");
            }
            StaticDataHolder.instance.Checkpoint = 13;
            controller.levelLoader.LoadScene("Second Day Evening");
        }
    }
}
