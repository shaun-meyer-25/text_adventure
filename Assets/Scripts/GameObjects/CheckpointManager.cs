using System;
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
    public List<InteractableObject> checkpointNineItems;
    public InteractableObject checkpointNineBear;
    [HideInInspector] public int checkpoint;

    private void OnEnable()
    {
    }

    void Awake()
        {
        _controller = GetComponent<GameController>();
        _levelLoader = GetComponent<LevelLoader>();
        _characterInteractions = _controller.LoadDictionaryFromCsvFile("characterInteractionDescriptions");
        }

    public void SetCheckpoint(int maybeCheckpoint)
    {
        int previousCheckpoint = checkpoint;
        if (maybeCheckpoint == 1)
        {
            checkpoint = maybeCheckpoint;
            
            _controller.roomNavigation.currentRoom.SetPeopleInRoom(_controller.characters);
            _controller.roomNavigation.currentRoom.SetInteractableObjectsInRoom(checkpointOneItems.ToArray());
            _controller.LoadRoomData();
        }

        if (maybeCheckpoint == 2)
        {
            checkpoint = maybeCheckpoint;
            _controller.travelingCompanions.Add(_controller.characters.First(o => o.noun.Equals("Ohm")));
        }

        if (maybeCheckpoint == 3)
        {
            checkpoint = maybeCheckpoint;

            List<Interaction> interactions =
                new List<Interaction>(_controller.characters.First(o => o.noun.Equals("Ohm")).interactions);
            Interaction interaction = interactions.Find(o => o.action.keyword.Equals("interact"));
            ActionResponse response = (ActionResponse) ScriptableObject.CreateInstance<OhmGetInPosition>().SetRequiredString("north foothills");
            interaction.SetActionResponse(response);
            // if you just "use" the spear here, you will die. 
            // you first should interact with ohm, ohm will circle around. then you can use the spear 
        }

        if (maybeCheckpoint == 4)
        {
            checkpoint = maybeCheckpoint;
            _controller.LogStringWithReturn("you and Ohm successfully flee the vicious bear. it is afternoon now, hunger weighs heavily on you.");
            _controller.allRoomsInGame.Find(o => o.roomName == "north forest").SetInteractableObjectsInRoom(checkpointFourItems.ToArray());
        }

        if (maybeCheckpoint == 5)
        {
            checkpoint = maybeCheckpoint;
        }

        if (maybeCheckpoint == 6)
        {
            checkpoint = maybeCheckpoint;
        }

        if (maybeCheckpoint == 7)
        {
            checkpoint = maybeCheckpoint;
        }
        
        if (maybeCheckpoint == 8)
        {
            checkpoint = maybeCheckpoint;
            
            InteractableObject person = new List<InteractableObject>(_controller.characters)
                .Find(o => o.name == "Ohm");
            List<Interaction> interactions =
                new List<Interaction>(person.interactions);
            Interaction interact = interactions.Find(o => o.action.keyword.Equals("interact"));
            interact.actionResponse = ScriptableObject.CreateInstance<OhmAsksToHoldOrb>();
            _controller.roomNavigation.currentRoom.SetPeopleInRoom(_controller.characters);

        }

        if (maybeCheckpoint == 9)
        {
            checkpoint = maybeCheckpoint;
            
            _controller.roomNavigation.currentRoom.SetInteractableObjectsInRoom(_controller.checkpointManager.checkpointNineItems.ToArray());
            _controller.PrepareObjectsToTakeOrExamine(_controller.roomNavigation.currentRoom);
            InteractableObject item = _controller.checkpointManager.checkpointNineItems.First();

            _controller.interactableItems.nounsInInventory.Add("spear");
            _controller.interactableItems.AddActionResponsesToUseDictionary();
            _controller.LogStringWithReturn("Ohm pauses for a moment, then turns to the wall to grab the two remaining spears. they hand you one and motion outside.");
            _controller.LogStringWithReturn("when you meet their eyes, you see a cold determination. you must be victorious.");
            _controller.LogStringWithReturn("you have obtained a spear.");
            
            _controller.travelingCompanions.Add(_controller.characters.First(o => o.noun.Equals("Ohm")));
        }

        if (maybeCheckpoint == 10)
        {
            checkpoint = maybeCheckpoint;
         //   _controller.
        }
        
        if (maybeCheckpoint == 11)
        {
            checkpoint = maybeCheckpoint;
            _controller.LogStringWithReturn(
                "there is joy on the faces of the others as you lay the carcass on the ground. Tei says they will go get more fuel for the fire, to cook with. " +
                "you skin the bear with the help of the others, which takes until nightfall. there is plenty of meat to cook, " +
                "but Tei does not return with fuel for the fire. this is troubling.");
            _controller.LogStringWithReturn("Ohm, though injured, agrees to go looking for Tei with you.");
            _controller.SetNighttime();
            _controller.travelingCompanions.Remove(_controller.characters.First(o => o.noun.Equals("Ohm")));
        }
        
        for (int i = 0; i < _controller.characters.Length; i++)
        {
            List<Interaction> interactions =
                new List<Interaction>(_controller.characters[i].interactions);
            Interaction interact = interactions.Find(o => o.action.keyword.Equals("interact"));

            if (maybeCheckpoint > 0 && _characterInteractions[_controller.characters[i].keyword][maybeCheckpoint - 1] != "x")
            {
                interact.textResponse =
                    _characterInteractions[_controller.characters[i].keyword][maybeCheckpoint - 1];
            }
        }

        if (maybeCheckpoint - previousCheckpoint == 1)
        {
            SaveGameManager.SaveGame(_controller);
        }
    }
}

 