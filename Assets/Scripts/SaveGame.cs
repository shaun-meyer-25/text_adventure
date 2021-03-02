using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveGame
{
    public Dictionary<InteractableObject, Room> mapOfThingsToLocation;
    public Dictionary<InteractableObject, Room> mapOfPeopleToLocation;
    public List<string> nounsInInventory;
    public int checkpointReached;
    public List<string> actionLog;
    public Fire.FireLevel fireLevel;
    public string currentScene;
    public List<InteractableObject> travelingCompanions;

    public SaveGame()
    {
        mapOfThingsToLocation = new Dictionary<InteractableObject, Room>();
        mapOfPeopleToLocation = new Dictionary<InteractableObject, Room>();
        nounsInInventory = new List<string>();
        checkpointReached = 0;
        actionLog = new List<string>();
        fireLevel = Fire.FireLevel.Dead;
        currentScene = SceneManager.GetActiveScene().name;
        travelingCompanions = new List<InteractableObject>();
    }
}
