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

    private Image _image;

    private void Start()
    {
        _image = this.GetComponentsInChildren<Image>().First();
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    IEnumerator LoadLevel(string sceneName)
    {
        // todo - figure out how to change transition color without that weird bug where it does nothing
     //   _image.color = color;
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
    
    
}
