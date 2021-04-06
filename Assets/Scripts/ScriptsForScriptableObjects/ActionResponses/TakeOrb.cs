using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/TakeOrb")]
public class TakeOrb : ActionResponse
{
    public override bool DoActionResponse(GameController controller)
    {
        controller.Invoke("DelayedSceneLoad", 15f);
        return true;
    }
}
