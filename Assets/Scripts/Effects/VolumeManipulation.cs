using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class VolumeManipulation : MonoBehaviour
{

    private Volume volume;

    private void Awake()
    {
        volume = FindObjectOfType<Volume>();

    }

    private void OnEnable()
    {
        Bloom b = (Bloom) volume.profile.components.Find(o => o.name == "Bloom(Clone)");
        ChromaticAberration ca = (ChromaticAberration) volume.profile.components.Find(o => o.name == "ChromaticAberration(Clone)");
        LensDistortion ld = (LensDistortion) volume.profile.components.Find(o => o.name == "LensDistortion(Clone)");
        Vignette v = (Vignette) volume.profile.components.Find(o => o.name == "Vignette(Clone)");
        b.active = false;
        ca.active = false;
        ld.active = false;
        v.active = false;
    }

    public void EnableEffect(string s)
    {
        if (s == "Lens Distortion")
        {
            LensDistortion ld = (LensDistortion) volume.profile.components.Find(o => o.name == "LensDistortion(Clone)");
            ld.active = true;
        }

        if (s == "Chromatic Aberration")
        {
            ChromaticAberration ca = (ChromaticAberration) volume.profile.components.Find(o => o.name == "ChromaticAberration(Clone)");
            ca.active = true;
        }
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

         if (requiredString == "fallingNightmare")
         {
             MountainNightmareController cont = (MountainNightmareController) controller;
             cont.FallingText();
             LensDistortion ld = (LensDistortion) volume.profile.components.Find(o => o.name == "LensDistortion(Clone)");
             StartCoroutine(FallingNightmare(ld));
         }

         if (requiredString == "respondToNua")
         {
             VolumeComponent c = volume.profile.components.Find(o => o.name == "Bloom(Clone)");
             Bloom bloom = (Bloom) c;
             bloom.active = true;
             ChromaticAberration ca = 
                 (ChromaticAberration) volume.profile.components.Find(o => o.name == "ChromaticAberration(Clone)");
             ca.active = true;
             StartCoroutine(RespondToNua(controller, bloom, ca));

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
         else if (requiredString == "enterFinalCave")
         {
             controller.background.color = Color.black;
             controller.currentColor = "white";
         } 
         else if (requiredString == "ohmFinal")
         {
             FinalCaveController fc = (FinalCaveController) controller;

             fc.GlobalLight.GetComponent<Animator>().enabled = true;
             Color color;

             if (ColorUtility.TryParseHtmlString("#FB63FD", out color))
                 fc.GlobalLight.color = color;
             // profit 
         }
         else if (requiredString == "strangling")
         {
             VolumeComponent c = volume.profile.components.Find(o => o.name == "Vignette(Clone)");
             Vignette vignette = (Vignette) c;
             vignette.active = true;
             vignette.color.value = Color.black; 

             StartCoroutine("LosingConsciousness", vignette);
             StartCoroutine("DyingInOhmsGrip", controller);
         }

         if (requiredString == "caveBearAround")
         {
             Debug.Log("todo - caveBearAround");
         }
     }

    public IEnumerator FallingNightmare(LensDistortion ld)
    {
        float offset = 0;
        while (offset >= -40)
        {
            offset -= 1f;
            ld.center.Override(new Vector2(-.2f, offset));
            yield return null;
        }
    }

    public IEnumerator LosingConsciousness(Vignette v)
    {
        // todo - choking sound
        float intensity = 0f;
        while (intensity <= 1f)
        {
            intensity += .0003f;
            v.intensity.value = intensity;
            yield return null;
        }
    }

    public IEnumerator DyingInOhmsGrip(IController controller)
    {
        yield return new WaitForSeconds(5f);
        controller.levelLoader.LoadScene("Credits");
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
         var isaudioNotNull = controller.audio != null;
         while (seconds > 0) 
         {
             bloom.intensity.value += .1f;
             if (isaudioNotNull)
             {
                 controller.audio.volume += .001f;
             }
             seconds -= Time.deltaTime;
             yield return null;
         }

         if (isaudioNotNull)
         {
             controller.audio.Stop();
         }

         bloom.active = false;
     }
     
     public IEnumerator RespondToNua(IController controller, Bloom bloom, ChromaticAberration ca)
     {
         float seconds = 3;
         while (seconds > 0) 
         {
             bloom.intensity.value += .2f;
             seconds -= Time.deltaTime;
             yield return null;
         }

         
         ca.active = false;
         bloom.active = false;
         controller.LogStringWithReturn("your eyes widen, your fists clench. you wrench the orb out of the young one's hands. they fall hard on the ground as their grasp on it slips.");
         controller.LogStringWithReturn("Onah shrieks. they grab Nua by the hand and run out of the cave.");
         controller.LogStringWithReturn(
             "Ohm rises to their feet. they give you a harsh glance, then motion for you to try to follow after. you need to make this right.");
         controller.processingDelay = .02f;
         controller.DisplayLoggedText();
         controller.useButtonAnimator.SetTrigger("Use-Grow1");
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
         } else if (requiredString == "strangling")
         {
             VolumeComponent c = volume.profile.components.Find(o => o.name == "Vignette(Clone)");
             Vignette v = (Vignette) c;
             v.active = false;
             StopCoroutine("LosingConsciousness");
             StopCoroutine("DyingInOhmsGrip");
         }
     }
}
