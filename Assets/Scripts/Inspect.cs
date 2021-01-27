using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Observe/Inspect")]
public class Inspect : ObserveChoice
{
    public Inspect(string keyword)
    {
        this.keyword = keyword;
    }
}
