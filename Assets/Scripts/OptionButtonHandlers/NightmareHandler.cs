using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName= "TextAdventure/ButtonHandlers/NightmareHandler")]
public class NightmareHandler : IOptionButtonHandler
{
	override public void Handle(IController controller, Text textObject)
    {
	    MountainNightmareController cont = (MountainNightmareController) controller;
	    cont.StrangerGrow();
    }
}
