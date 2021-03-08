using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveGame
{
    public Dictionary<string, string> mapOfThingsToLocation;
    public Dictionary<string, string> mapOfPeopleToLocation;
    public List<string> nounsInInventory;
    public int checkpointReached;
    public List<string> actionLog;
    public Fire.FireLevel fireLevel;
    public string currentScene;
    public List<string> travelingCompanions;
    public string currentRoom;

    public SaveGame()
    {
        mapOfThingsToLocation = new Dictionary<string, string>();
        mapOfPeopleToLocation = new Dictionary<string, string>();
        nounsInInventory = new List<string>();
        actionLog = new List<string>();
        travelingCompanions = new List<string>();
    }
}
