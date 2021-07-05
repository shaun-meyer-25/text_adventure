using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    private IController _controller;
    private LevelLoader _levelLoader;
    private Dictionary<string, List<string>> _characterInteractions;
    
    // Checkpoint 1 flags
    
    // Checkpoint 2 flags
    
    // Checkpoint 3 flags
    public bool ohmInPosition = false;

    public InteractableObject treeBranch;
    public List<InteractableObject> checkpointOneItems;
    public List<InteractableObject> checkpointFourItems;
    public List<InteractableObject> checkpointFiveItems;
    public InteractableObject checkpointNineBear;
    [HideInInspector] public int checkpoint;

    private void OnEnable()
    {
    }

    void Awake()
        {
        _controller = GetComponent<IController>();
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
            // this is kind of a smell, as we are running the checkpoint after room change so the cleanup code isn't run like it should
            _controller.allRoomsInGame.Find(o => o.roomName == "home cave").RemovePersonFromRoom("Ohm");
        }

        if (maybeCheckpoint == 3)
        {
            checkpoint = maybeCheckpoint;

            List<Interaction> interactions =
                new List<Interaction>(_controller.characters.First(o => o.noun.Equals("Ohm")).interactions);
            Interaction interaction = interactions.Find(o => o.action.keyword.Equals("interact"));
            ActionResponse response = (ActionResponse) ScriptableObject.CreateInstance<OhmGetInPosition>()
                .SetRequiredString("north foothills");
            interaction.SetActionResponse(response);
            // if you just "use" the spear here, you will die. 
            // you first should interact with ohm, ohm will circle around. then you can use the spear 
        }

        if (maybeCheckpoint == 4)
        {
            checkpoint = maybeCheckpoint;
            _controller.LogStringWithReturn(
                "you and Ohm successfully flee the vicious bear. it is afternoon now, hunger weighs heavily on you.");
            _controller.allRoomsInGame.Find(o => o.roomName == "north forest")
                .SetInteractableObjectsInRoom(checkpointFourItems.ToArray());
            _controller.LoadRoomData();
            _controller.roomNavigation.SetExitLabels(_controller.roomNavigation.currentRoom
                .GetExits(_controller.checkpointManager.checkpoint));
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

            // todo - we are currently calling this twice (once when going to sleep, once on scene load) which seems awfully bad
            if (_controller.roomNavigation.currentRoom.roomName != "sleep")
            {
                InteractableObject person = new List<InteractableObject>(_controller.characters)
                    .Find(o => o.name == "Ohm");
                List<Interaction> interactions =
                    new List<Interaction>(person.interactions);
                Interaction interact = interactions.Find(o => o.action.keyword.Equals("interact"));
                interact.actionResponse = ScriptableObject.CreateInstance<OhmAsksToHoldOrb>();
                Debug.Log("WHEN WE SET CHAPTER 8 ppl The ROOM NAME IS - " +
                          _controller.roomNavigation.currentRoom.roomName);
                _controller.roomNavigation.currentRoom.SetPeopleInRoom(_controller.characters);
            }
        }

        if (maybeCheckpoint == 9)
        {
            checkpoint = maybeCheckpoint;

            _controller.interactableItems.nounsInInventory.Add("spear");
            _controller.interactableItems.AddActionResponsesToUseDictionary();
            _controller.LogStringWithReturn(
                "Ohm pauses for a moment, then turns to the wall to grab the two remaining spears. they hand you one and motion outside.");
            _controller.LogStringWithReturn(
                "when you meet their eyes, you see a cold determination. you must be victorious.");
            _controller.LogStringWithReturn("you have obtained a spear.");

            _controller.travelingCompanions.Add(_controller.characters.First(o => o.noun.Equals("Ohm")));
        }

        if (maybeCheckpoint == 10)
        {
            checkpoint = maybeCheckpoint;
        }

        if (maybeCheckpoint == 11)
        {
            checkpoint = maybeCheckpoint;
            _controller.interactableItems.nounsInInventory.Add("spear");
            _controller.interactableItems.nounsInInventory.Add("carcass");
            _controller.isDaytime = true;
            _controller.SetDaylight();

            _controller.fifthButton.gameObject.SetActive(true);
            _controller.interactableItems.AddActionResponsesToUseDictionary();
            _controller.roomNavigation.currentRoom =
                _controller.allRoomsInGame.Find(o => o.roomName == "watering hole");
            _controller.LogStringWithReturn(
                "your vision returns. the bear lies dead with your spear lodged in its heart.");
            _controller.LogStringWithReturn(
                "Ohm cries out in pain beneath the now dead beast. you help them out from under the carcass.");
            _controller.LogStringWithReturn("you obtain a bear carcass");
            _controller.roomNavigation.SetExitLabels(_controller.roomNavigation.currentRoom
                .GetExits(_controller.checkpointManager.checkpoint));
            _controller.travelingCompanions.Add(_controller.characters.First(o => o.noun.Equals("Ohm")));
            Room home = _controller.allRoomsInGame.Find(o => o.roomName == "home cave");
            Room wateringHole = _controller.allRoomsInGame.Find(o => o.roomName == "watering hole");
            foreach (var c in _controller.characters)
            {
                if (c.noun == "Ohm")
                {
                    wateringHole.AddPersonToRoom(c);
                }
                else
                {
                    home.AddPersonToRoom(c);
                }
            }
            FirstGrowth();
            //StartCoroutine(SecondGrowthAnimationChain());
        }

        if (maybeCheckpoint == 12)
        {
            checkpoint = maybeCheckpoint;
            _controller.LogStringWithReturn(
                "there is joy on the faces of the others as you lay the carcass on the ground. Tei says they will go get more fuel for the fire, to cook with. " +
                "you skin the bear with the help of the others, which takes until nightfall. there is plenty of meat to cook, " +
                "but Tei does not return with fuel for the fire. this is troubling.");
            _controller.LogStringWithReturn("you must go look for Tei.");
            _controller.SetNighttime();
            _controller.travelingCompanions.Remove(_controller.characters.First(o => o.noun.Equals("Ohm")));
            _controller.roomNavigation.currentRoom.RemovePersonFromRoom("Tei");
        }

        if (maybeCheckpoint == 13)
        {
            _controller.fifthButton.SetActive(true);
            checkpoint = maybeCheckpoint;
            _controller.fifthButton.GetComponentInChildren<Animator>().SetTrigger("Grow1");
        }

        if (maybeCheckpoint == 14)
        {
            checkpoint = maybeCheckpoint;
            _controller.roomNavigation.currentRoom.SetPeopleInRoom(_controller.characters);
            InteractableObject person = new List<InteractableObject>(_controller.characters)
                .Find(o => o.name == "Nua");
            List<Interaction> interactions =
                new List<Interaction>(person.interactions);
            Interaction interact = interactions.Find(o => o.action.keyword.Equals("interact"));
            interact.actionResponse = ScriptableObject.CreateInstance<TakeOrbFromNua>();
        }
        
        if (maybeCheckpoint == 15)
        {
            checkpoint = maybeCheckpoint;
            InteractableObject person = new List<InteractableObject>(_controller.characters)
                .Find(o => o.name == "Nua");
            List<Interaction> interactions =
                new List<Interaction>(person.interactions);
            Interaction interact = interactions.Find(o => o.action.keyword.Equals("interact"));
            interact.actionResponse = null;

            Room r = _controller.roomNavigation.currentRoom;
            _controller.LoadRoomData();
            _controller.roomNavigation.SetExitLabels(_controller.roomNavigation.currentRoom
                .GetExits(_controller.checkpointManager.checkpoint));
            r.RemovePersonFromRoom("Nua");
            r.RemovePersonFromRoom("Onah");
            
            _controller.travelingCompanions.Add(_controller.characters.First(o => o.noun.Equals("Ohm")));
        }

        if (maybeCheckpoint == 16)
        {
            InteractableObject person = new List<InteractableObject>(_controller.characters)
                .Find(o => o.name == "Ohm");
            _controller.travelingCompanions.Add(person);
            List<Interaction> interactions =
                new List<Interaction>(person.interactions);
            Interaction interact = interactions.Find(o => o.action.keyword.Equals("interact"));
            OhmFalls so = ScriptableObject.CreateInstance<OhmFalls>();
            so.requiredString = "mountains2";
            interact.actionResponse = so;
            checkpoint = maybeCheckpoint;
        }

        if (maybeCheckpoint == 17)
        {
            checkpoint = maybeCheckpoint;
            _controller.LoadRoomData();

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

    public void FirstGrowth()
    {
        _controller.fifthButton.GetComponentInChildren<Animator>().SetTrigger("Grow0");
    }
    
    public IEnumerator SecondGrowthAnimationChain()
    {
        _controller.fifthButton.GetComponentInChildren<Animator>().SetTrigger("Grow1");
        yield return new WaitForSeconds(2.5f);
        _controller.useButtonAnimator.SetTrigger("Use-Grow1");
    }
}

 