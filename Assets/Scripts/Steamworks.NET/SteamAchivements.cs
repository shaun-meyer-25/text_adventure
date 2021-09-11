using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamAchivements : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAchievement(string name)
    {
        if (!SteamManager.Initialized)
        {
            Debug.Log("steam manager is not initialized!");
            return;
        }
        
        Debug.Log(SteamUserStats.SetAchievement(name));
        SteamUserStats.StoreStats();
    }
}
