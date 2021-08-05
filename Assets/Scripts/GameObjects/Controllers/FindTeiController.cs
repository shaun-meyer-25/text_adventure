using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindTeiController : IController
{
    private TextProcessing _textProcessing;
    private Vector2 _playerStart;
    private Vector2 _beast1Start;
    private Vector2 _beast2Start;
    private Vector2 _beast3Start;
    
    public Texture2D reticle;

    public List<Text> buttonTexts;
    public PlayerMovement Player;
    public Beast Beast1;
    public Beast Beast2;
    public Beast Beast3;

    // Start is called before the first frame update
    void Start()
    {
        _playerStart = Player.transform.position;
        _beast1Start = Beast1.transform.position;
        _beast2Start = Beast2.transform.position;
        _beast3Start = Beast3.transform.position; 
        checkpointManager = GetComponent<CheckpointManager>();
        levelLoader = FindObjectOfType<LevelLoader>();
        volumeManipulation = gameObject.AddComponent<VolumeManipulation>();

        Cursor.SetCursor(reticle, Vector2.zero, CursorMode.Auto);
        displayText.text = "";
        _textProcessing = new TextProcessing(this, processingDelay);

        volumeManipulation.EffectStart(this, "firstOrbEncounter");

       // _textProcessing.DisplayText("you will need to find them quickly.");
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

    public void ResetLevel()
    {
        // flash screen purple or some shit with a sound effect

        volumeManipulation.EffectStart(this, "screenWipe");
        Beast1.transform.position = _beast1Start;
        Beast2.transform.position = _beast2Start;
        Beast3.transform.position = _beast3Start;
        Player.transform.position = _playerStart;
        
        Beast1.StopChase();
        Beast2.StopChase();
        Beast3.StopChase();
    }
}
