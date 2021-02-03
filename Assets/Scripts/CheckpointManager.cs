using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private GameController controller;
    private Dictionary<string, List<string>> characterInteractions;

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
        if (maybeCheckpoint == 0)
        {
            checkpoint = maybeCheckpoint;
            for (int i = 0; i < controller.characters.Length; i++)
            {
                controller.characters[i].description =
                    characterInteractions[controller.characters[i].keyword][maybeCheckpoint];
            }

            controller.roomNavigation.currentRoom.SetPeopleInRoom(controller.characters);
        }
    }
}
