using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Interact/Take")]
public class Take : InteractChoice
{
    public Take(string keyword)
    {
        this.keyword = keyword;
    }
}
