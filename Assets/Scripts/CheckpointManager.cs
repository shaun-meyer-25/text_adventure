using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    private GameController _controller;
    private LevelLoader _levelLoader;
    private Dictionary<string, List<string>> _characterInteractions;
    
    // Checkpoint 1 flags
    
    // Checkpoint 2 flags
    
    // Checkpoint 3 flags
    public bool ohmInPosition = false;

    public List<InteractableObject> checkpointOneItems;
    public List<InteractableObject> checkpointFourItems;
    public List<InteractableObject> checkpointFiveItems;
    [HideInInspector] public int checkpoint;
    
    void Start()
        {
        _controller = GetComponent<GameController>();
        _levelLoader = GetComponent<LevelLoader>();
        _characterInteractions = _controller.LoadDictionaryFromCsvFile("characterInteractionDescriptions");
        }

    public void SetCheckpoint(int maybeCheckpoint)
    {
        if (maybeCheckpoint == 1)
        {
            checkpoint = maybeCheckpoint;
            
            _controller.roomNavigation.currentRoom.SetPeopleInRoom(_controller.characters);
            _controller.roomNavigation.currentRoom.SetInteractableObjectsInRoom(checkpointOneItems.ToArray());
            _controller.LoadRoomData();
            SaveGameManager.SaveGame(_controller);
        }

        if (maybeCheckpoint == 2)
        {
            checkpoint = maybeCheckpoint;
            _controller.travelingCompanions.Add(_controller.characters.First(o => o.noun.Equals("ohm")));
            SaveGameManager.SaveGame(_controller);
        }

        if (maybeCheckpoint == 3)
        {
            checkpoint = maybeCheckpoint;
            _controller.roomNavigation.currentRoom.description = _controller.roomNavigation.currentRoom.description + 
                "\nthere is a cave bear at the mouth of an opening in the rocks. ohm signals you to stop. they brandish " +
                "their spear. it looks like they are forming a plan.";
            
            _controller.roomNavigation.currentRoom.roomInvestigationDescription =
                _controller.roomNavigation.currentRoom.roomInvestigationDescription +
                "\nthe beast glares at you. it takes a step towards you and lets out a low, menacing growl.";

            List<Interaction> interactions =
                new List<Interaction>(_controller.characters.First(o => o.noun.Equals("ohm")).interactions);
            Interaction interaction = interactions.Find(o => o.action.keyword.Equals("interact"));
            ActionResponse response = (ActionResponse) ScriptableObject.CreateInstance<OhmGetInPosition>().SetRequiredString("north foothills");
            interaction.SetActionResponse(response);
            // if you just "use" the spear here, you will die. 
            // you first should interact with ohm, ohm will circle around. then you can use the spear 
            SaveGameManager.SaveGame(_controller);
        }

        if (maybeCheckpoint == 4)
        {
            checkpoint = maybeCheckpoint;
            _controller.LogStringWithReturn("you and ohm successfully flee the vicious bear. it is afternoon now, hunger weighs heavily on you.");
            _controller.allRoomsInGame.Find(o => o.roomName == "north forest").SetInteractableObjectsInRoom(checkpointFourItems.ToArray());
            SaveGameManager.SaveGame(_controller);
        }

        if (maybeCheckpoint == 5)
        {
            checkpoint = maybeCheckpoint;
            _controller.allRoomsInGame.Find(o => o.roomName == "west coast").SetInteractableObjectsInRoom(checkpointFiveItems.ToArray());
            
            SaveGameManager.SaveGame(_controller);
        }
        
        for (int i = 0; i < _controller.characters.Length; i++)
        {
            List<Interaction> interactions =
                new List<Interaction>(_controller.characters[i].interactions);
            Interaction interact = interactions.Find(o => o.action.keyword.Equals("interact"));

            if (_characterInteractions[_controller.characters[i].keyword][maybeCheckpoint - 1] != "x")
            {
                interact.textResponse =
                    _characterInteractions[_controller.characters[i].keyword][maybeCheckpoint - 1];
            }
        }
    }
}

 