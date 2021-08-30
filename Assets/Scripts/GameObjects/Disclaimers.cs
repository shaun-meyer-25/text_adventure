using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disclaimers : MonoBehaviour
{
    public LevelLoader levelLoader;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NextScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(3f);
        levelLoader.LoadScene("StartMenu");
    }
}
