using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/NothingHappens")]
public class NothingHappens : ActionResponse
{
    public override bool DoActionResponse(IController controller)
    {
        return false;
    }
}
