using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bats : MonoBehaviour
{
    public List<GameObject> Eyes;

    public void HaltEyes()
    {
        StopCoroutine("LightUpEyes");
    }

    public void StartEyes()
    {
        StartCoroutine("LightUpEyes");
    }
    
    public IEnumerator LightUpEyes()
    {
        foreach (var obj in Eyes)
        {
            obj.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ShutEyes()
    {
        foreach (var obj in Eyes)
        {
            obj.SetActive(false);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
