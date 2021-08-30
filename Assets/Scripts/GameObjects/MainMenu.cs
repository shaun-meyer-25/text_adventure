using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Scene = UnityEngine.SceneManagement.Scene;

public class MainMenu : MonoBehaviour
{
    public Button continueButton;
    public GameObject mainMenu;
    public GameObject newGameConfirm;
    public GameObject thankYou;
    
    private SaveGame _saveGame;
    private IController _controller;

    public void PlayGame()
    {
        if (_saveGame != null)
        {
            mainMenu.SetActive(false);
            newGameConfirm.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Main");
        }
    }

    public void NewGame()
    {
        Debug.Log("new game start");
        File.Delete( Application.persistentDataPath + "/savedGames.gd" );
        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif

        SceneManager.LoadScene("Main");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void Continue()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        SceneManager.LoadSceneAsync(_saveGame.currentScene, LoadSceneMode.Single);
    }
    
    private void Start()
    {
        _saveGame = SaveGameManager.LoadGame();

        if (_saveGame == null)
        {
            continueButton.GetComponentInChildren<Text>().color = Color.gray;
            continueButton.interactable = false;
        }
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        
        _controller = (IController) GameObject.FindObjectOfType(typeof(IController)); 
        SaveGameManager.PopulateGameData(_saveGame, _controller);
        
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
