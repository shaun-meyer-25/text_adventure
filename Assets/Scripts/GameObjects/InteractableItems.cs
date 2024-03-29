using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractableItems : MonoBehaviour
{
    public List<InteractableObject> usableItemList;
    public List<InteractableObject> interactableOnly;
    public Dictionary<string, string> examineDictionary = new Dictionary<string, string>();
    public Dictionary<string, string> takeDictionary = new Dictionary<string, string>();

    [HideInInspector] public List<string> nounsInRoom = new List<string>();

    private Dictionary<string, ActionResponse> useDictionary = new Dictionary<string, ActionResponse>();
    private IController controller;

    void Awake()
    {
        controller = GetComponent<IController>();
    }
    public List<string> nounsInInventory = new List<string>();
    
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

    public void AddActionResponsesToUseDictionary()
    {
        for (int i = 0; i < nounsInInventory.Count; i++)
        {
            string noun = nounsInInventory[i];

            InteractableObject interactableObjectInInventory = GetInteractableObjectFromUsableList(noun);
            if (interactableObjectInInventory == null)
                continue;

            for (int j = 0; j < interactableObjectInInventory.interactions.Length; j++)
            {
                Interaction interaction = interactableObjectInInventory.interactions[j];

                if (interaction.ActionResponse == null)
                    continue;

                if (!useDictionary.ContainsKey(noun) && interaction.action.keyword == "use")
                {
                    useDictionary.Add(noun, interaction.ActionResponse);
                }
            }
        }
    }

    InteractableObject GetInteractableObjectFromUsableList(string noun)
    {
        for (int i = 0; i < usableItemList.Count; i++)
        {
            if (usableItemList[i].noun == noun)
            {
                return usableItemList[i];
            }
        }

        return null;
    }

    public void DisplayInventory()
    {

        if (nounsInInventory.Count > 0)
        {
            controller.LogStringWithReturn("you have ");
            for (int i = 0; i < nounsInInventory.Count; i++)
            {
                controller.LogStringWithReturn(nounsInInventory[i]);
            }
        }
        else
        {
            controller.LogStringWithReturn("you don't have anything right now.");
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
        if (interactableOnly.Find(o => o.noun == noun) != null)
        {
            return null;
        }
        if (nounsInRoom.Contains(noun))
        {
            nounsInInventory.Add(noun);
            AddActionResponsesToUseDictionary();
            nounsInRoom.Remove(noun);
            RemoveObjectFromRoom(noun);
            return takeDictionary;
        }
        else
        {
            controller.LogStringWithReturn("no " + noun + " here to take.");
            return null;
        }
    }

    public List<InteractableObject> InteractableObjectsInInventory()
    {
        List<InteractableObject> inventoryItems = new List<InteractableObject>();
        for (int i = 0; i < nounsInInventory.Count; i++)
        {
            inventoryItems.Add(usableItemList.Find(o => o.noun.Equals(nounsInInventory[i])));
        }

        return inventoryItems;
    }

    public void UseItem(string[] separatedInputWords)
    {
        string nounToUse = separatedInputWords[1];

        if (nounsInInventory.Contains(nounToUse))
        {
            if (useDictionary.ContainsKey(nounToUse))
            {
                bool actionResult = useDictionary[nounToUse].DoActionResponse(controller);
                if (!actionResult)
                {
                    controller.LogStringWithReturn("nothing happens.");
                }
                else
                {
                    if (nounToUse != "orb")
                    {
                        useDictionary.Remove(nounToUse);
                        nounsInInventory.Remove(nounToUse);                        
                    }
                }
            }
            else
            {
                if (nounToUse == "tei's shell")
                {
                    controller.LogStringWithReturn("you can't use tei's shell.");
                }
                else
                {
                    controller.LogStringWithReturn("you can't use the " + nounToUse + ".");
                }
            }
        }
        else
        {
            controller.LogStringWithReturn("there is no " + nounToUse + " in your inventory to use");
        }
    }
    
    void RemoveObjectFromRoom(string noun)
    {
        List<InteractableObject> target =
            new List<InteractableObject>(controller.roomNavigation.currentRoom.InteractableObjectsInRoom);
        InteractableObject item = target.Find(o => o.noun.Equals(noun));

        target.RemoveAll(o => o.noun.Equals(noun));
        controller.roomNavigation.currentRoom.SetInteractableObjectsInRoom(target.ToArray());

        controller.interactionDescriptionsInRoom.Remove(item.description);
    }
}
