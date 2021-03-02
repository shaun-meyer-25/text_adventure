using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueScreen : MonoBehaviour
{

    public void LoadGame()
    {
        SaveGameManager.LoadGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
