using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThankYouController : IController
{
    // Start is called before the first frame update
    void Start()
    {
        displayText.text = "";
        TextProcessing tp = new TextProcessing(this);
        tp.DisplayText("\n" + "that concludes this demo of 'a curse from beyond'. thank you so much for playing. " +
                       "the rest of this tale is coming soon.\n\n<color=purple>see you then</color>", .04f);
    }

    void Update()
    {
        
    }
}