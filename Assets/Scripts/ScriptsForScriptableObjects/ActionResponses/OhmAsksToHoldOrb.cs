using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/OhmAsksToHoldOrb")]
public class OhmAsksToHoldOrb : ActionResponse
{ 
    public override bool DoActionResponse(GameController controller)
    {
        controller.isConversing = true;

        List<ConversationChoice> choices = new List<ConversationChoice>();

        ConversationChoice yes = CreateInstance<ConversationChoice>();
        yes.keyword = "yes";
        ConversationChoice no = CreateInstance<ConversationChoice>();
        no.keyword = "no";
        
        choices.Add(yes);
        choices.Add(no);
        
        //ConversationChoice 
        controller.UpdateRoomChoices(choices.ToArray());
        controller.isInteracting = false;
        return true;
    }
}
