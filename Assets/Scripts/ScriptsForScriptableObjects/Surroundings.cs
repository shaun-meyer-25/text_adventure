using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Observe/Surroundings")]
public class Surroundings : ObserveChoice
{
    public Surroundings(string keyword)
    {
        this.keyword = keyword;
    }
}
