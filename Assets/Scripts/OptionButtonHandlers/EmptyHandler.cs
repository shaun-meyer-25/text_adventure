using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName= "TextAdventure/ButtonHandlers/EmptyHandler")]
public class EmptyHandler : IOptionButtonHandler
{
    public override void Handle(IController controller, Text buttonSelection)
    {
        
    }
}
