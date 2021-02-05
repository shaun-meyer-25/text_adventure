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
        if (maybeCheckpoint == 1)
        {
            checkpoint = maybeCheckpoint;
            
            for (int i = 0; i < controller.characters.Length; i++)
            {
                controller.characters[i].description =
                    characterInteractions[controller.characters[i].keyword][maybeCheckpoint - 1];

                if (controller.characters[i].keyword.Equals("ohm"))
                {
                    List<Interaction> interactions = new List<Interaction>(controller.characters[i].interactions);
                    Interaction interaction = interactions.Find(o => o.action.keyword.Equals("interact"));
                    ActionResponse response = (ActionResponse) ScriptableObject.CreateInstance<NPCGivesItem>().SetRequiredString("spear");
                    interaction.actionResponse = response;
                }
            }
            
            controller.roomNavigation.currentRoom.SetPeopleInRoom(controller.characters);

            controller.LoadRoomDataAndDisplayRoomText();
        }

        if (maybeCheckpoint == 2)
        {
            checkpoint = maybeCheckpoint;
            
            
        }
    }
}

