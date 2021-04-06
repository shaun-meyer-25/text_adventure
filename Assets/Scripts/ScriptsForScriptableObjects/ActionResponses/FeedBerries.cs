using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/FeedBerries")]
public class FeedBerries : ActionResponse
{
    public override bool DoActionResponse(GameController controller)
    {
        if (controller.roomNavigation.currentRoom.roomName == requiredString)
        {
            controller.LogStringWithReturn("most mask their disappointment, but it is unmistakable. too many days without a substantial meal." +
                                           " this cannot continue much longer. a successful hunt must come soon." +
                                           "\n\nnothing left now but to go to bed.");


            return true;
        }

        return false;
    }
}
