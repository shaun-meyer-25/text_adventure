using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/OhmGetInPosition")]
public class OhmGetInPosition : ActionResponse
{
    public override bool DoActionResponse(GameController controller)
    {
        if (controller.roomNavigation.currentRoom.roomName == requiredString)
        {
            controller.checkpointManager.ohmInPosition = true;
            if (!controller.displayText.text.Contains("ohm circles around"))
            {
                controller.LogStringWithReturn("ohm circles around to distract the bear. its gaze follows him.");
            }

            List<Interaction> interactions =
                new List<Interaction>(controller.characters.First(o => o.noun.Equals("ohm")).interactions);
            Interaction interaction = interactions.Find(o => o.action.keyword.Equals("interact"));
            interaction.textResponse = "they motion to you to strike";
            interaction.SetActionResponse(null);
            controller.roomNavigation.currentRoom.roomInvestigationDescription = "the bear is focused on ohm. now is your chance to strike.";

            return true;
        }

        return false;
    }
    
    public ScriptableObject SetRequiredString(string requiredString)
    {
        this.requiredString = requiredString;
        return this;
    }
}
