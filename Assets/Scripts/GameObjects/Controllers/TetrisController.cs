using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisController : IController
{
    private List<string> undisplayedSentences = new List<string>();
    private TextProcessing _textProcessing;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _textProcessing = new TextProcessing(this, processingDelay);
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
