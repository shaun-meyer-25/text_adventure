using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDataHolder : MonoBehaviour
{
    public static StaticDataHolder instance;

    public int Checkpoint;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            instance.Checkpoint = 0;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
