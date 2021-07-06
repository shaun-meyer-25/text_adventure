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
         if (requiredString == "enableBloom")
         {
             VolumeComponent c = volume.profile.components.Find(o => o.name == "Bloom(Clone)");
             Bloom bloom = (Bloom) c;
             c.active = true; 
         }
         if (requiredString == "firstOrbEncounter")
         {
             
             VolumeComponent c = volume.profile.components.Find(o => o.name == "Bloom(Clone)");
             Bloom bloom = (Bloom) c;
             c.active = true; 
             StartCoroutine("Pulse", bloom);
         }
         if (requiredString == "firstOrbUse") {
             
             VolumeComponent c = volume.profile.components.Find(o => o.name == "Bloom(Clone)");
             Bloom bloom = (Bloom) c;
             ChromaticAberration ca = 
                 (ChromaticAberration) volume.profile.components.Find(o => o.name == "ChromaticAberration(Clone)");
             c.active = true; 
             StartCoroutine(PulseMultiple(bloom, ca));
         }
         else if (requiredString == "screenWipe")
         {
             VolumeComponent c = volume.profile.components.Find(o => o.name == "Bloom(Clone)");
             Bloom bloom = (Bloom) c;
             ChromaticAberration ca = 
                 (ChromaticAberration) volume.profile.components.Find(o => o.name == "ChromaticAberration(Clone)");
             Vignette v = (Vignette) volume.profile.components.Find(o => o.name == "Vignette(Clone)");
             StopCoroutine("Pulse");
             StartCoroutine(ResetLevel(controller, bloom, v, ca));
             
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


    public IEnumerator PulseMultiple(Bloom bloom, ChromaticAberration ca)
    {
        float seconds = 5;

        while (seconds > 0)
        {
            bloom.intensity.value = Mathf.Sin(Time.realtimeSinceStartup * 2.0f) + 1.5f;
            ca.intensity.value = Mathf.Sin(Time.realtimeSinceStartup);
            seconds -= Time.deltaTime;
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

     public IEnumerator ResetLevel(IController controller, Bloom bloom, Vignette v, ChromaticAberration ca)
     {
         controller.light.intensity = 2.0f;
         ca.active = true;
         v.active = true;
         float seconds = 2f;
         bloom.intensity.value = 450f;
         int count = 0;
         while (seconds > 0)
         {

             bloom.intensity.value -= .8f;
             controller.light.intensity -= .0035f;
             //controller.audio.volume += .001f;
             seconds -= Time.deltaTime;
             count += 1;
             yield return null;
         }
//         controller.audio.Stop();
         ca.active = false;
         v.active = false;
         controller.light.intensity = 0f;
         StartCoroutine("Pulse", bloom);
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
