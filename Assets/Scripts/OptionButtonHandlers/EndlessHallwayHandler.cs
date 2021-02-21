using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndlessHallwayHandler : IOptionButtonHandler
{
    override public void Handle(GameController controller, Text textObject, AudioSource audioSource)
    {
        string text = textObject.text;

        if (controller.exitNames.Contains(text))
        {
            int numOfExits = controller.exitNames.Count;
            for (int i = 0; i < numOfExits; i++)
            {
                Choice choice = controller.exitChoices[i];

                if (choice.keyword == text)
                {
                    ScriptableObject.CreateInstance<Go>()
                        .RespondToAction(controller, new string[] {"go", choice.keyword});
                    break;
                }
            }
        }
        else if (text == "observe" || text == "use" || text == "interact")
        {
            audioSource.Play();
            controller.LogStringWithReturn("<color=purple>" + ManipulationEffects.RandomDistortedString(controller) + "</color>");
            IEnumerator coroutine = ManipulationEffects.MessWithBackground(controller);
            StartCoroutine(coroutine);
        }
        else
        {
            for (int i = 0; i < controller.actions.Length; i++)
            {
                var choice = controller.actions[i];

                if (choice.keyword == text)
                {
                    ActionChoice actionChoice = (ActionChoice) choice;
                    actionChoice.RespondToAction(controller, new string[] {choice.keyword});
                }
            }
        }
        controller.DisplayLoggedText();
    }
}
