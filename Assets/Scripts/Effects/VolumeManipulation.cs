using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeManipulation : MonoBehaviour
{

    private Volume volume;

    private void Awake()
    {
        volume = FindObjectOfType<Volume>();

    }

    public void EffectStart(IController controller, string requiredString)
     {
         if (requiredString == "firstOrbEncounter")
         {
             
             VolumeComponent c = volume.profile.components.Find(o => o.name == "Bloom(Clone)");
             Bloom bloom = (Bloom) c;
             c.active = true; 
             StartCoroutine("Pulse", bloom);
         }

         if (requiredString == "caveBearAround")
         {
             Debug.Log("todo - caveBearAround");
         }
     }

     public IEnumerator Pulse(Bloom bloom)
     {
         while (true)
         {
             bloom.intensity.value = Mathf.Sin(Time.realtimeSinceStartup * 2.0f) + 1.5f;
             yield return null;
         }
     }

     public IEnumerator SwellThenFizzle(IController controller, Bloom bloom)
     {
         float seconds = 6;
         while (seconds > 0) 
         {
             bloom.intensity.value += .1f;
             controller.audio.volume += .001f;
             seconds -= Time.deltaTime;
             yield return null;
         }
         controller.audio.Stop();
         bloom.active = false;
     }

     public void EffectEnd(IController controller, string requiredString)
     {
         if (requiredString == "firstOrbEncounter")
         {
             VolumeComponent c = volume.profile.components.Find(o => o.name == "Bloom(Clone)");
             Bloom bloom = (Bloom) c;
             StopCoroutine("Pulse");
             StartCoroutine(SwellThenFizzle(controller, bloom));
         }
     }
}
