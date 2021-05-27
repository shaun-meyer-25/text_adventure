using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/MakeConversation")]
public class MakeConversation : ActionResponse
{
    public override bool DoActionResponse(IController controller)
    {
        return true;
    }
}