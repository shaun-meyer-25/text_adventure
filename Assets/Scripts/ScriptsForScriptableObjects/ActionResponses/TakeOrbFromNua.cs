using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeOrbFromNua : ActionResponse
{
    public override bool DoActionResponse(IController controller)
    {
        
        controller.volumeManipulation.EffectStart(controller, "respondToNua");
        controller.checkpointManager.SetCheckpoint(15);
        controller.UpdateRoomChoices(controller.startingActions);
        controller.isInteracting = false;
        GameObject button = GameObject.Find("Option4");
        button.GetComponent<Button>().interactable = false;
        controller.isFourthButtonDisabled = true;
        return true;
    }
}
