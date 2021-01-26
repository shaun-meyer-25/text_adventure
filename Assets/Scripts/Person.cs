using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Interact/Person")]
public class Person : InteractChoice
{
    public Person(string keyword)
    {
        this.keyword = keyword;
    }
}
