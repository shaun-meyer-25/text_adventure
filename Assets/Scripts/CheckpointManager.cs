using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private GameController controller;
    private Dictionary<string, List<string>> characterInteractions;
    
    // Checkpoint 1 flags
    
    // Checkpoint 2 flags
    
    // Checkpoint 3 flags
    public bool ohmInPosition = false;

    public List<InteractableObject> checkpointOneItems;
    public int checkpoint;
    
    void Start()
        {
        checkpoint = 0;
        controller = GetComponent<GameController>();
        characterInteractions = controller.LoadDictionaryFromCsvFile("characterInteractionDescriptions");
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

        if (maybeCheckpoint == 3)
        {
            checkpoint = maybeCheckpoint;
            controller.roomNavigation.currentRoom.description = controller.roomNavigation.currentRoom.description + 
                "\nthere is a cave bear at the mouth of an opening in the rocks. ohm signals you to stop. they brandish " +
                "their spear. it looks like they are forming a plan.";
            
            controller.roomNavigation.currentRoom.roomInvestigationDescription =
                controller.roomNavigation.currentRoom.roomInvestigationDescription +
                "\nthe beast glares at you. it takes a step towards you and lets out a low, menacing growl.";

            List<Interaction> interactions =
                new List<Interaction>(controller.roomNavigation.currentRoom.PeopleInRoom[0].interactions);
            Interaction interaction = interactions.Find(o => o.action.keyword.Equals("interact"));
            ActionResponse response = (ActionResponse) ScriptableObject.CreateInstance<NPCGivesItem>().SetRequiredString("spear");
            interaction.SetActionResponse(response);
            // if you just "use" the spear here, you will die. 
            // you first should interact with ohm, ohm will circle around. then you can use the spear 
        }
    }
}

