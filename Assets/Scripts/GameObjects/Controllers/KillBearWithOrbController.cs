using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBearWithOrbController : IController
{
    public float processingDelay = 0.4f;
    public Texture2D reticle;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(reticle, Vector2.zero, CursorMode.Auto);
        displayText.text = "";
        TextProcessing tp = new TextProcessing(this, processingDelay);
        tp.DisplayText("what is up");
    }

    public IEnumerator TypeSentence(Dictionary<int, Tuple<string, char>> charactersAndTheirColors )
    {
        for (int i = 0; i < charactersAndTheirColors.Count; i++)
        {
            Tuple<string, char> value = charactersAndTheirColors[i];
            displayText.text += "<color=" + value.Item1 + ">" + value.Item2 + "</color>";

            yield return new WaitForSeconds(processingDelay);
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
