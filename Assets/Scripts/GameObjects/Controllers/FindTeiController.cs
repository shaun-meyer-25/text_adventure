using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindTeiController : IController
{
    private TextProcessing _textProcessing;

    public Texture2D reticle;

    public List<Text> buttonTexts;


    // Start is called before the first frame update
    void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
        volumeManipulation = gameObject.AddComponent<VolumeManipulation>();

        Cursor.SetCursor(reticle, Vector2.zero, CursorMode.Auto);
        displayText.text = "";
        _textProcessing = new TextProcessing(this, processingDelay);

        volumeManipulation.EffectStart(this, "firstOrbEncounter");

        _textProcessing.DisplayText("you will need to find them quickly.");
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
        foreach (var VARIABLE in buttonTexts)
        {
            VARIABLE.text = ManipulationEffects.RandomDistortedString();
        }
    }

    public override void LogStringWithReturn(string s)
    {
        StopAllCoroutines();
        displayText.text = "";
        _textProcessing.DisplayText(s);
    }
}
