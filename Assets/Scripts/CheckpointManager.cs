using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private GameController controller;
    private Dictionary<string, List<string>> characterInteractions;

    public List<InteractableObject> checkpointOneItems;
    public int checkpoint;
    
    void Start()
        {
        checkpoint = 0;
        controller = GetComponent<GameController>();
        characterInteractions = controller.LoadDictionaryFromCsvFile("characterInteractionDescriptions");
        }

    void Update()
    {
        
    }
    
    public void SetCheckpoint(int maybeCheckpoint)
    {
        if (maybeCheckpoint == 1)
        {
            checkpoint = maybeCheckpoint;
            
            for (int i = 0; i < controller.characters.Length; i++)
            {
                controller.characters[i].description =
                    characterInteractions[controller.characters[i].keyword][maybeCheckpoint - 1];
            }
            
            controller.roomNavigation.currentRoom.SetPeopleInRoom(controller.characters);
            controller.roomNavigation.currentRoom.SetInteractableObjectsInRoom(checkpointOneItems.ToArray());
        }

        if (maybeCheckpoint == 2)
        {
            checkpoint = maybeCheckpoint;
            controller.travelingCompanions.Add(controller.characters.First(o => o.noun.Equals("ohm")));
        }
    }
}

