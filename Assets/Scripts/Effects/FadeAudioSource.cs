using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FadeAudioSource {

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }

        audioSource.volume = 1f;
        audioSource.Stop();
        yield break;
    }

    public static IEnumerator StartNew(AudioSource audioSource, float wait, AudioClip newAudio, float newVolume)
    {
        yield return new WaitForSeconds(wait);
        
        audioSource.clip = newAudio;
        audioSource.volume = newVolume;
        audioSource.Play();
        
        
    }

}