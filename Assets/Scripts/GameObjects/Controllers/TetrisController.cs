using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TetrisController : IController
{
    public List<SpriteRenderer> UISprites;
    public List<Image> UIImages;

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
    
    public override void DisplayLoggedText ()
    {
        displayText.text = "";

        string logAsText = string.Join ("\n", actionLog.ToArray ());
        List<string> pastLog = new List<string>(logAsText.Split('\n'));
        while (logAsText.Length > 10000)
        {
            pastLog.RemoveRange(0, pastLog.Count / 2);
            logAsText = string.Join("\n", pastLog.ToArray());
        }
        foreach (var line in pastLog)
        {
            displayText.text += "\n<color=" + currentColor + ">" + line + "</color>";
        }

        string undisplayedLogAsText = string.Join ("\n\n", undisplayedSentences.ToArray ());
        while (undisplayedLogAsText.Length > 10000)
        {
            List<string> log = new List<string>(undisplayedLogAsText.Split('\n'));
            log.RemoveRange(0, log.Count / 2);
            undisplayedLogAsText = string.Join("\n", log.ToArray());
        }
		
        _textProcessing.StopTypingCoroutine();
		
        _textProcessing.DisplayText("\n" + undisplayedLogAsText);
        foreach (var sentence in undisplayedSentences)
        {
            actionLog.Add(sentence + "\n");
        }
        undisplayedSentences.Clear();
    }
	
    public override void LogStringWithReturn(string stringToAdd) {
        if (stringToAdd != "")
        {
            undisplayedSentences.Add(stringToAdd);
        }
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
}
