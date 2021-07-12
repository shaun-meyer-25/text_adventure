using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MountainNightmareController : IController
{
    public Text m1a;
    public Text m1b;
    public Text m1c;
    public Text m1d;
    public Text m1e;
    public List<Text> CloseMountainTexts;
    public List<Text> MidMountainTexts;
    public List<Text> FarMountainTexts;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var VARIABLE in CloseMountainTexts)
        {
            if (UnityEngine.Random.Range(0, 10) > 7)
            {
                VARIABLE.text = ManipulationEffects.RandomDistortedString(this);
            }
        }
        
        foreach (var VARIABLE in MidMountainTexts)
        {
            if (UnityEngine.Random.Range(0, 10) > 5)
            {
                VARIABLE.text = ManipulationEffects.RandomString(this);
            }
        }

        foreach (var VARIABLE in FarMountainTexts)
        {
            VARIABLE.text = ManipulationEffects.RandomString(this);

        }
    }
}
