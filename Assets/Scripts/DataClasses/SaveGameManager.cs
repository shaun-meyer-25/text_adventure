using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;
using System.Linq;

public static class SaveGameManager
{
    public static void SaveGame(IController controller)
    {
        SaveGame game = new SaveGame();
        game.actionLog = controller.actionLog;
        game.checkpointReached = controller.checkpointManager.checkpoint;
        game.currentScene = SceneManager.GetActiveScene().name;
        game.nounsInInventory = controller.interactableItems.nounsInInventory;
        game.travelingCompanions = controller.travelingCompanions.Select(o => o.name).ToList();
        game.currentRoom = controller.roomNavigation.currentRoom.roomName;
        game.isDaylight = controller.isDaytime;
        game.currentTextColor = controller.currentColor;
        game.orbButtonActive = controller.fifthButton.activeSelf;
        if (game.orbButtonActive)
        {
            if (controller.checkpointManager.checkpoint < 11)
            {
                game.orbButtonPhase = "0";
            }
            else if (controller.checkpointManager.checkpoint == 11 || controller.checkpointManager.checkpoint == 12)
            {
                game.orbButtonPhase = "1";
            }
            else if (controller.checkpointManager.checkpoint == 13 || controller.checkpointManager.checkpoint == 14)
            {
                game.orbButtonPhase = "2";
            }
        }
        
        for (int i = 0; i < controller.allRoomsInGame.Count; i++)
        {
            if (controller.allRoomsInGame[i].PeopleInRoom.Length > 0)
            {
                Debug.Log("room name is " + controller.allRoomsInGame[i].roomName); 
                for (int j = 0; j < controller.allRoomsInGame[i].PeopleInRoom.Length; j++)
                {
                    Debug.Log("the person name is - " + controller.allRoomsInGame[i].PeopleInRoom[j].name);
                    game.mapOfPeopleToLocation.Add(controller.allRoomsInGame[i].PeopleInRoom[j].name, 
                        controller.allRoomsInGame[i].roomName);
                }
            }
        } 

        for (int i = 0; i < controller.allRoomsInGame.Count; i++)
        {
            if (controller.allRoomsInGame[i].InteractableObjectsInRoom.Length > 0)
            {
                for (int j = 0; j < controller.allRoomsInGame[i].InteractableObjectsInRoom.Length; j++)
                {
                    game.mapOfThingsToLocation.Add(controller.allRoomsInGame[i].InteractableObjectsInRoom[j].name, 
                        controller.allRoomsInGame[i].roomName);
                }
            }
        }
        
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create (Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, game);
        file.Close();
    }

    public static SaveGame LoadGame()
    {
        SaveGame saveGame;
        if(File.Exists(Application.persistentDataPath + "/savedGames.gd")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            saveGame = (SaveGame) bf.Deserialize(file);
            file.Close();
            return saveGame;
        }
        else
        {
            Debug.Log("no save file");
            return null;
        }
    }

    public static void PopulateGameData(SaveGame saveGame, IController controller)
    {
        controller.isDaytime = saveGame.isDaylight;
        if (saveGame.isDaylight)
        {
            if (saveGame.currentRoom == "home cave")
            {
                controller.SetNighttime();
            }
            else
            {
                controller.SetDaylight();
            }
        }
        else
        {
            controller.SetNighttime();
        }

        for (int i = 0; i < saveGame.mapOfThingsToLocation.Count; i++)
        {
            string objectName = saveGame.mapOfThingsToLocation.Keys.ToList()[i];
            InteractableObject intObj = controller.interactableItems.usableItemList
                .Find(o => o.name == objectName);
            Room location = controller.allRoomsInGame.Find(o => o.roomName == saveGame.mapOfThingsToLocation[objectName]);

            if (location != null)
            {
                if (location.InteractableObjectsInRoom == null || !location.InteractableObjectsInRoom.Contains(intObj))
                {
                    location.AddObjectToRoom(intObj);
                }
            }
        }
            
        for (int i = 0; i < saveGame.mapOfPeopleToLocation.Count; i++)
        {
            string personName = saveGame.mapOfPeopleToLocation.Keys.ToList()[i];
            InteractableObject person = new List<InteractableObject>(controller.characters)
                .Find(o => o.name == personName);
            Room location = controller.allRoomsInGame.Find(o => o.roomName == saveGame.mapOfPeopleToLocation[personName]);

            if (location != null)
            {
                if (location.PeopleInRoom == null || !location.PeopleInRoom.Contains(person))
                {
                    location.AddPersonToRoom(person);
                }
            }
        }

        controller.actionLog = saveGame.actionLog;
        controller.checkpointManager.checkpoint = saveGame.checkpointReached;
        controller.fire.fireLevel = saveGame.fireLevel;
        controller.interactableItems.nounsInInventory = saveGame.nounsInInventory;
        controller.roomNavigation.currentRoom = controller.allRoomsInGame.Find(o => o.roomName == saveGame.currentRoom);
        controller.interactableItems.AddActionResponsesToUseDictionary();
        controller.fifthButton.SetActive(saveGame.orbButtonActive);

        if (saveGame.orbButtonActive)
        {
            SpriteRenderer sr = controller.fifthButton.GetComponentsInChildren<SpriteRenderer>()
                .First(o => o.gameObject.name == "Animations");
          //  sr.sprite = Sprite.
        }
        
        controller.checkpointManager.SetCheckpoint(saveGame.checkpointReached);

        List<InteractableObject> travelingCompanions = new List<InteractableObject>();
        for (int i = 0; i < saveGame.travelingCompanions.Count; i++)
        {
            travelingCompanions.Add(new List<InteractableObject>(controller.characters)
                .Find(o => o.name == saveGame.travelingCompanions[i]));
        }

        controller.travelingCompanions = travelingCompanions; 
    }
}
