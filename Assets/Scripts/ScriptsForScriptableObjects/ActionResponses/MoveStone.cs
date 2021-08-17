using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/MoveStone")]
public class MoveStone : ActionResponse
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool DoActionResponse(IController _controller)
    {
        FinalCaveController controller = (FinalCaveController) _controller;
        
        if (controller.checkpointManager.checkpoint == 21)
        {
            controller.LogStringWithReturn("you notice how much strength the orb gave you, and how weak you feel without it. it takes everything you have to shift the stone.");
            controller.LogStringWithReturn("as the stone rolls away, it reveals a small tunnel beyond.");
            controller.LogStringWithReturn("the noise disturbs the snakes. they begin to slither more quickly.");

            controller.HigherSnakeSpawnRateActive = true;
            controller.RandomSnakeSpawnStop();
            controller.RandomSnakeSpawnStart();
            
            controller.checkpointManager.SetCheckpoint(22);
            return true;
        }
        else
        {
            controller.LogStringWithReturn("there is a scrape on your shoulder from where you braced against the stone. despite the darkness, you feel pride in your strength.");
            return false;
        }
    }
}
