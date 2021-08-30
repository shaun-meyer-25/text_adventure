using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDataHolder : MonoBehaviour
{
    public static StaticDataHolder instance;

    public int Checkpoint;
    public List<string> NounsInInventory;

    public AudioClip Wind;
    public AudioClip Heartbeat;
    public AudioClip Fire;
    public AudioClip FirstDaySong;
    public AudioClip SecondDaySong;
    public AudioClip Scarything;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            instance.Checkpoint = Checkpoint;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
