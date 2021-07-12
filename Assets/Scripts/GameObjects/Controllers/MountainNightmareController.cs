using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MountainNightmareController : IController
{
    public List<Text> CloseMountainTexts;
    public List<Text> MidMountainTexts;
    public List<Text> FarMountainTexts;

    public GameObject cameraTextbox;
    public Transform stranger;
    // Start is called before the first frame update
    void Start()
    {
        volumeManipulation = gameObject.AddComponent<VolumeManipulation>();
        volumeManipulation.EnableEffect("Lens Distortion");
        volumeManipulation.EnableEffect("Chromatic Aberration");

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

    public void StrangerGrow()
    {
        StartCoroutine(Grow());
    }

    IEnumerator Grow()
    {
        float x = stranger.transform.localScale.x;
        float y = stranger.transform.localScale.y;
        while (x <= 15)
        {
            x += 2f;
            y += 2f;
            stranger.transform.localScale = new Vector3(x, y, 0);
            yield return null;
        }
        volumeManipulation.EffectStart(this, "fallingNightmare");

    }

    public void FallingText()
    {
        cameraTextbox.SetActive(true);
        StartCoroutine(SceneChange());
    }

    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(7);
        //todo - crack, crunch sound
        SceneManager.LoadScene("Third Day", LoadSceneMode.Single);
    }
}
