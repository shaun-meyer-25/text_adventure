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

    private bool _orbGrowing = false;
    private bool _orbShrinking = false;
    private float _intensity;
    private Image _image;
    private Material _mat;
    private static readonly int Fade = Shader.PropertyToID("_Fade");
    private static readonly int Start1 = Animator.StringToHash("Start");

    private void Start()
    {
        _image = this.GetComponentsInChildren<Image>().First();
        _image.color = color;
        _mat = circle.GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        if (_orbGrowing)
        {
            _intensity += .002f;
            _mat.SetFloat(Fade, _intensity);
        }

        if (_orbShrinking)
        {
            _intensity -= .002f;
            _mat.SetFloat(Fade, _intensity);
        }
        
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    public void LoadSceneOrb(string sceneName)
    {
        circle.SetActive(true);
        _orbGrowing = true;
        StartCoroutine(LoadLevelOrb(sceneName));
    }

    public void StartSceneOrb()
    {
        circle.SetActive(true);
        _intensity = 1f;
        _mat.SetFloat(Fade, _intensity);
        _orbShrinking = true;
        StartCoroutine(StartingSceneWithOrb());
    }

    IEnumerator StartingSceneWithOrb()
    {
        yield return new WaitForSeconds(2);
        _orbShrinking = false;
        circle.SetActive(false);
    }

    IEnumerator LoadLevelOrb(string sceneName)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
    

    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger(Start1);

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
    
    
}
