using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponse/MakeConversation")]
public class MakeConversation : ActionResponse
{
    public override bool DoActionResponse(GameController controller)
    {
        return true;
    }
}