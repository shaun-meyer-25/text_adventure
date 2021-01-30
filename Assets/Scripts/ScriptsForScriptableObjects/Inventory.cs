using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Observe/Inventory")]
public class Inventory : ObserveChoice
{
    public Inventory(string keyword)
    {
        this.keyword = keyword;
    }
}
