using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/UseTeiShell")]
public class UseTeiShell : ActionResponse
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool DoActionResponse(IController controller)
    {
        if (controller.checkpointManager.checkpoint == 22 && controller.roomNavigation.currentRoom.roomName == "ohm encounter")
        {
            controller.volumeManipulation.EffectEnd(controller, "strangling");
            controller.LogStringWithReturn("in an act of desperation you take the only thing you're carrying with you and ram it into Ohm's arm. the grip persists. you ram it into their shoulder, their neck.");
            controller.LogStringWithReturn("Ohm screams in the voice that is no longer theirs. blood runs from their neck and their grip loosens. you run back through the cave the way you came.");
            controller.LogStringWithReturn("with their howls of rage and pain still echoing through the cave, you move the stone back over to cover the tunnel. you run out of the cave.");
            controller.LogStringWithReturn("Tei is standing at the mouth of the cave. they see Ohm's blood on you and reach for you. you take them in an embrace.");
            controller.LogStringWithReturn("you are safe. you are together. that dreadful thing will never touch your mind again.");
            
            // this runs about 60 seconds. at around the 40 second mark you are outside. we should cancel the orb light
            FinalCaveController fc = (FinalCaveController) controller;
            fc.DisabledButtons = true;
            fc.UpdateRoomChoices(fc.startingActions);
            fc.StartCoroutine(fc.GoodEndingWon());
            return true;
        }

        return false;
    }
}
