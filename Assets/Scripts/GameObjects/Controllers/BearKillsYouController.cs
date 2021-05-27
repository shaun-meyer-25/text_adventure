using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class BearKillsYouController : IController
{

    public float processingDelay = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        displayText.text = "";
        TextProcessing tp = new TextProcessing(this, processingDelay);
        tp.DisplayText("\n" + "the bear stares you down, you realize too late it was ready for you. it rears up on its legs. " +
                       "as the bear lunges at you, your spear buries into its mass, but it pins you to the ground." +
                       "\n\nthe bear roars in pain, oblivious to the person it has pinned, inches from its face. " +
                       "\n\nthen\n\n <color=red>buries its teeth into your neck. pain explodes in your vision, your mind.\n\n" +
                       "you can't breathe as you feel it tearing out your throat.\n\nyou die slowly, painfully. alone.</color>");
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
    
    void Update()
    {
        
    }
}
