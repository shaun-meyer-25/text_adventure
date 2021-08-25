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
    
    private Material _mat;
    private bool _orbGrowing = false;
    private bool _orbShrinking = false;
    private float _intensity;
    private Image _image;
    private static readonly int Fade = Shader.PropertyToID("_Fade");
    private static readonly int Start1 = Animator.StringToHash("Start");
    private static readonly int Opacity = Shader.PropertyToID("_Opacity");

    private void OnEnable()
    {
        _mat = circle.GetComponent<SpriteRenderer>().material;
    }

    private void Start()
    {

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

            if (_intensity <= 0)
            {
                _orbShrinking = false;
                _mat.SetFloat(Opacity, 0);
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    public void LoadSceneOrb(string sceneName)
    {
        _orbGrowing = true;
        _mat.SetFloat(Opacity, 1);
        StartCoroutine(LoadLevelOrb(sceneName));
    }

    public void StartSceneOrb()
    {
        _intensity = 1f;
        _mat.SetFloat(Fade, _intensity);
        _mat.SetFloat(Opacity, 1);
        _orbShrinking = true;
    }

    public void FakeLevelLoadOrb()
    {
        _orbGrowing = true;
        _mat.SetFloat(Opacity, 1);
        StartCoroutine(FakeLevelLoadOrbEnum());
    }

    IEnumerator FakeLevelLoadOrbEnum()
    {
        while (_intensity <= 1)
        {
            yield return null;
        }

        _orbGrowing = false;
        _orbShrinking = true;
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

    private void OnDestroy()
    {
        Destroy(_mat);
    }
}
