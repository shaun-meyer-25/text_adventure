using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/TeiInvitesOutside")]
public class TeiInvitesOutside : ActionResponse
{
    public override bool DoActionResponse(IController controller)
    {
        if (controller.roomNavigation.currentRoom.roomName == "home cave" && controller.checkpointManager.checkpoint == 14)
        {
            
            controller.isConversing = true;
            
            List<ConversationChoice> choices = new List<ConversationChoice>();

            ConversationChoice yes = CreateInstance<ConversationChoice>();
            yes.keyword = "yes";
            ConversationChoice no = CreateInstance<ConversationChoice>();
            no.keyword = "no";
        
            choices.Add(yes);
            choices.Add(no);
            
            controller.UpdateRoomChoices(choices.ToArray());
            controller.isInteracting = false; 
            
            return true;
        }
        
        return false;
        throw new System.NotImplementedException();
    }
}
