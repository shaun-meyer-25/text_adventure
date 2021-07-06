using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime;
    public Color color;
    public GameObject circle;

    private Image _image;
    private Animator _circleAnimator;

    private void Start()
    {
        _image = this.GetComponentsInChildren<Image>().First();
        _circleAnimator = new List<Animator>(this.GetComponentsInChildren<Animator>()).Find(o => o.name == "Circle");
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    public void LoadSceneOrb(string sceneName)
    {
        // todo - maybe have a circle appear on screen and widen while we amp up the bloom, as if we're being drawn into the orb?
        circle.SetActive(true);
    }
    

    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
    
    
}
