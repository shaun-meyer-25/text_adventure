using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public AudioClip FinalSong;
    private AudioSource _audioSource;
    private AudioSource _audioSource1;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        StartCoroutine(StartMusic());
        StartCoroutine(ChangeBackToMenu());
        StartCoroutine(CutMusic());
    }

    IEnumerator ChangeBackToMenu()
    {
        yield return new WaitForSeconds(60f);
        SceneManager.LoadScene("StartMenu");
    }

    IEnumerator CutMusic()
    {
        yield return new WaitForSeconds(58f);
        StartCoroutine(FadeAudioSource.StartFade(_audioSource, 1.6f, 0));
    }

    IEnumerator StartMusic()
    {
        yield return new WaitForSeconds(3f);

        _audioSource.clip = FinalSong;
        _audioSource.Play();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
