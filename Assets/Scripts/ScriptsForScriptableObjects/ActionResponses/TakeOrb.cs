using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/TakeOrb")]
public class TakeOrb : ActionResponse
{
    public override bool DoActionResponse(GameController controller)
    {
        controller.LogStringWithReturn("you feel... powerful. you hold the orb in both hands, no longer aware of the burning in your feet. " +
                                       "the hunger in your gut subsides, you see yourself standing triumphant over the bear.\n\n" +
                                       "the orb ceases its glow. the beasts and bugs resume their night sounds. you ought to go home now, it is dangerous.\n\n" +
                                       "" +
                                       "but we will protect you.");
        return true;
    }
}
