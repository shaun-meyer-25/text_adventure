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

        orbLandingSite.description = "spooky times";
        orbLandingSite.roomInvestigationDescription = "spoooooooook";
        orbLandingSite.SetInteractableObjectsInRoom(GameController.checkpointManager.checkpointFiveItems.ToArray());
    }
}
