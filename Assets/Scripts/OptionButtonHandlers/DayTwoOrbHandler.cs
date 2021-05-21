using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName= "TextAdventure/ButtonHandlers/DayTwoOrbHandler")]
public class DayTwoOrbHandler : IOptionButtonHandler
{
    public override void Handle(IController controller, Text textObject)
    {
        string text = textObject.text;
        if (text == "throw")
        {
//            controller.interactableItems.UseItem(separatedInputWords);
            controller.LogStringWithReturn("you examine your surroundings and see a stone the size of your hand lying in the mud.");
            controller.LogStringWithReturn("picking it up, you feel an intensity in your hand as if you'd run it through the fire.");
            controller.LogStringWithReturn("eyes locked on the bear, you throw the stone. <color=purple>your vision flashes, you do not see the stone leave your hand.</color>");
            controller.LogStringWithReturn("when your vision clears, the bear's head is a mess of gore. Ohm struggles to get out from under the bear.");
            controller.UpdateRoomChoices(controller.startingActions);
            controller.roomNavigation.currentRoom.SetInteractableObjectsInRoom(
                new[] {controller.checkpointManager.checkpointNineBear});
            controller.PrepareObjectsToTakeOrExamine(controller.roomNavigation.currentRoom);
        }
        
        controller.DisplayLoggedText();
    }
}
