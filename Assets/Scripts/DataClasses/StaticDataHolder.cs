using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDataHolder : MonoBehaviour
{
    public static StaticDataHolder instance;

    public int Checkpoint;
    public List<string> NounsInInventory;

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
