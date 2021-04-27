using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class IOptionButtonHandler : ScriptableObject
{
    public abstract void Handle(GameController controller, Text buttonSelection);
}
