using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName= "TextAdventure/ButtonHandlers/DayTwoOrbHandler")]
public class DayTwoOrbHandler : IOptionButtonHandler
{
    public override void Handle(IController controller, Text textObject)
    {
        string text = textObject.text;
        if (text == "ORB")
        {
            controller.levelLoader.LoadScene("KillBearWithOrb");
        }
    }
}
