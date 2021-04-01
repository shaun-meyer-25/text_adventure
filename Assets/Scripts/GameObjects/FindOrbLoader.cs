using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindOrbLoader : MonoBehaviour
{
    public GameController GameController;
    
    // Start is called before the first frame update
    void Start()
    {
        Room orbLandingSite = GameController.allRoomsInGame.Find(o => o.roomName == "west coast");

        orbLandingSite.description = "there is a large crater in the normally smooth sand";
        orbLandingSite.roomInvestigationDescription = "the ground still glows in spots. the sea itself appears restless from this disturbance.";
        orbLandingSite.SetInteractableObjectsInRoom(GameController.checkpointManager.checkpointFiveItems.ToArray());
    }
}
