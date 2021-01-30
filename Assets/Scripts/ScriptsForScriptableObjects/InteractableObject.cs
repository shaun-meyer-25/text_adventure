using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "TextAdventure/Interactable Object")]
public class InteractableObject : Choice
{
    public string noun = "name";
    [TextArea] public string description = "Description of where it is in room";
    public Interaction[] interactions;
}
