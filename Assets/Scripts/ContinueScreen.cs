using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueScreen : MonoBehaviour
{
    private GameController _controller;
    private SaveGame _saveGame;
    
    public void LoadGame()
    {
        _saveGame = SaveGameManager.LoadGame();
        SceneManager.LoadSceneAsync(_saveGame.currentScene, LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(this.gameObject);
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        if (scene.name != "Death Menu")
        {
            _controller = (GameController) GameObject.FindObjectOfType(typeof(GameController));
            SaveGameManager.PopulateGameData(_saveGame, _controller);
        }

        Debug.Log(mode);
    }
}
