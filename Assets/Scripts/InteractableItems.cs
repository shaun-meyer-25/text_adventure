using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItems : MonoBehaviour
{

    public Dictionary<string, string> examineDictionary = new Dictionary<string, string>();
    public Dictionary<string, string> takeDictionary = new Dictionary<string, string>();

    [HideInInspector] public List<string> nounsInRoom = new List<string>();
    private GameController controller;

    void Awake()
    {
        controller = GetComponent<GameController>();
    }
    
    private List<string> nounsInInventory = new List<string>();
    public string GetObjectsNotInInventory(Room currentRoom, int i)
    {
        InteractableObject interactableInRoom = currentRoom.InteractableObjectsInRoom[i];

        // Todo - this will make the item appear in the room again if it is removed from inventory
        // need it to be removed permanently from list in room, most likely.
        if (!nounsInInventory.Contains(interactableInRoom.noun))
        {
            nounsInRoom.Add(interactableInRoom.noun);
            return interactableInRoom.description;
        }

        return null;
    }

    public void DisplayInventory()
    {

        if (nounsInInventory.Count > 0)
        {
            controller.LogStringWithReturn("You have ");
            for (int i = 0; i < nounsInInventory.Count; i++)
            {
                controller.LogStringWithReturn(nounsInInventory[i]);
            }
        }
        else
        {
            controller.LogStringWithReturn("You don't have anything right now");
        }
    }
    
    public void ClearCollections()
    {
        examineDictionary.Clear();
        takeDictionary.Clear();
        nounsInRoom.Clear();
    }

    public Dictionary<string, string> Take(string[] separatedInputWords)
    {
        string noun = separatedInputWords[1];
        if (nounsInRoom.Contains(noun))
        {
            nounsInInventory.Add(noun);
            nounsInRoom.Remove(noun);
            RemoveObjectFromRoom(noun);
            return takeDictionary;
        }
        else
        {
            controller.LogStringWithReturn("No " + noun + " here to take.");
            return null;
        }
    }

    void RemoveObjectFromRoom(string noun)
    {
        List<InteractableObject> target =
            new List<InteractableObject>(controller.roomNavigation.currentRoom.InteractableObjectsInRoom);
        
        target.RemoveAll(o => o.noun.Equals(noun));
        controller.roomNavigation.currentRoom.SetInteractableObjectsInRoom(target.ToArray());
    }
}
