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
    public static void SaveGame(GameController controller)
    {
        SaveGame game = new SaveGame();
        game.actionLog = controller.actionLog;
        game.checkpointReached = controller.checkpointManager.checkpoint;
        game.currentScene = SceneManager.GetActiveScene().name;
        game.fireLevel = controller.fire.fireLevel;
        game.nounsInInventory = controller.interactableItems.nounsInInventory;
        game.travelingCompanions = controller.travelingCompanions;

        for (int i = 0; i < controller.allRoomsInGame.Count; i++)
        {
            if (controller.allRoomsInGame[i].PeopleInRoom.Length > 0)
            {
                for (int j = 0; j < controller.allRoomsInGame[i].PeopleInRoom.Length; j++)
                {
                    game.mapOfPeopleToLocation.Add(controller.allRoomsInGame[i].PeopleInRoom[j], 
                        controller.allRoomsInGame[i]);
                }
            }
        } 

        for (int i = 0; i < controller.allRoomsInGame.Count; i++)
        {
            if (controller.allRoomsInGame[i].InteractableObjectsInRoom.Length > 0)
            {
                for (int j = 0; j < controller.allRoomsInGame[i].InteractableObjectsInRoom.Length; j++)
                {
                    game.mapOfThingsToLocation.Add(controller.allRoomsInGame[i].InteractableObjectsInRoom[j], 
                        controller.allRoomsInGame[i]);
                }
            }
        }
        
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create (Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, game);
        file.Close();
    }

    public static void LoadGame()
    {
        SaveGame saveGame;
        if(File.Exists(Application.persistentDataPath + "/savedGames.gd")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            saveGame = (SaveGame) bf.Deserialize(file);
            file.Close();

            SceneManager.LoadScene(saveGame.currentScene, LoadSceneMode.Single);
            GameController controller = (GameController) GameObject.FindObjectOfType(typeof(GameController));

            for (int i = 0; i < saveGame.mapOfThingsToLocation.Count; i++)
            {
                
                InteractableObject intObj = saveGame.mapOfThingsToLocation.Keys.ToList()[i];
                Room location = controller.allRoomsInGame.Find(o => o.roomName == saveGame.mapOfThingsToLocation[intObj].roomName);
                
                location.AddObjectToRoom(intObj);
            }
            
            for (int i = 0; i < saveGame.mapOfPeopleToLocation.Count; i++)
            {
                
                InteractableObject person = saveGame.mapOfPeopleToLocation.Keys.ToList()[i];
                Room location = controller.allRoomsInGame.Find(o => o.roomName == saveGame.mapOfPeopleToLocation[person].roomName);
                
                location.AddPersonToRoom(person);
            }

            controller.actionLog = saveGame.actionLog;
            controller.checkpointManager.checkpoint = saveGame.checkpointReached;
            controller.fire.fireLevel = saveGame.fireLevel;
            controller.interactableItems.nounsInInventory = saveGame.nounsInInventory;
            controller.travelingCompanions = saveGame.travelingCompanions;
        }
        else
        {
            Debug.Log("no save file");
            SceneManager.LoadScene("Main");
        }
    }
}
