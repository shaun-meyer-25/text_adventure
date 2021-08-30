using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public static class ConversationHandler 
{
    public static void HandleConversation(IController controller, string response)
    {
        bool continuing = false;
        if (response == "no" && controller.checkpointManager.checkpoint == 8)
        {
            controller.LogStringWithReturn("Ohm looks at you strangely, but does not protest.");
            SetOhmInteraction(controller);
            
            controller.checkpointManager.SetCheckpoint(9);
        }

        if (response == "yes" && controller.checkpointManager.checkpoint == 8)
        {
            AudioSource audio = StaticDataHolder.instance.gameObject.GetComponent<AudioSource>();
            audio.clip = StaticDataHolder.instance.Scarything;
            audio.Play();
            
            controller.volumeManipulation.EffectStart(controller, "firstOrbUse");
            controller.LogStringWithReturn("<color=purple>you refuse to hand it to them</color>");
            controller.LogStringWithReturn("Ohm looks at you strangely, but does not protest.");
            SetOhmInteraction(controller);
            
            controller.checkpointManager.SetCheckpoint(9);
        }

        if (response == "yes" && controller.checkpointManager.checkpoint == 16)
        {
            controller.isSecondButtonDisabled = true;
            GameObject button = GameObject.Find("Option2");
            button.GetComponent<Button>().interactable = false;
            controller.buttonWrapAnimator.SetTrigger("WrapButton");
            controller.LogStringWithReturn("<color=purple>ohm reaches for the orb. push his hand away.</color>");
            return;
        }

        if (response == "no" && controller.checkpointManager.checkpoint == 16)
        {
            if (!controller.isSecondButtonDisabled)
            {
                controller.isSecondButtonDisabled = true;
                GameObject button = GameObject.Find("Option2");
                button.GetComponent<Button>().interactable = false;
                controller.buttonWrapAnimator.SetTrigger("WrapButton");
            }

            controller.LogStringWithReturn("you shove their hand away. they fall backwards. a shout, a look of shock, disbelief in their eyes. then a loud crack, an explosion of red on the rocks.");
            controller.LogStringWithReturn(
                "Onah screams. it cuts through the sound of the storm. they flee further into the mountains with Nua at their side.");
            controller.LogStringWithReturn(
                "<color=purple>you can not return without the others. the one you care for will think you a monster. the one that fell is fine. go after the others.</color>");
            controller.checkpointManager.SetCheckpoint(17);

        }
        
        if (response == "no" && controller.checkpointManager.checkpoint == 14)
        {
            controller.checkpointManager.SetBadEndingCourse();

        }

        if (response == "yes" && controller.checkpointManager.checkpoint == 14)
        {
            AudioSource audio = controller.GetComponent<AudioSource>();
            audio.clip = StaticDataHolder.instance.Scarything;
            audio.Play();
            
            continuing = true;
            controller.volumeManipulation.EffectStart(controller, "firstOrbEncounter");
            controller.volumeManipulation.EffectStart(controller, "chromaticPulse");
            controller.LogStringWithReturn("<color=purple>you can go with them later. you must not be apart from the orb. find it now.</color>");
            controller.DisplayLoggedText();

            List<ConversationChoice> choices = new List<ConversationChoice>();

            ConversationChoice yes = ScriptableObject.CreateInstance<ConversationChoice>();
            yes.keyword = "go with Tei";
            ConversationChoice no = ScriptableObject.CreateInstance<ConversationChoice>();
            no.keyword = "find orb";
        
            choices.Add(yes);
            choices.Add(no);
        
            //ConversationChoice 
            controller.UpdateRoomChoices(choices.ToArray());
        }
        
        if (response == "go with Tei" && controller.checkpointManager.checkpoint == 14)
        {
            controller.volumeManipulation.EffectEnd(controller, "firstOrbEncounter");
            controller.volumeManipulation.EffectEnd(controller, "chromaticPulse");
            controller.travelingCompanions.Add(controller.characters.First(o => o.noun.Equals("Tei")));
            
            controller.checkpointManager.SetCheckpoint(20);
        }
        
        if (response == "find orb" && controller.checkpointManager.checkpoint == 14)
        {
            controller.volumeManipulation.EffectEnd(controller, "chromaticPulse");
            controller.volumeManipulation.EffectEnd(controller, "orbWon");
            controller.audio.Stop();

            controller.checkpointManager.SetBadEndingCourse();

        }
        
        if (response == "no" && controller.checkpointManager.checkpoint == 22)
        {
            FinalCaveController fc = (FinalCaveController) controller;

            fc.audio.clip = fc.OhmsGrasp;
            fc.audio.volume = .4f;
            fc.audio.pitch = 1f;
            fc.audio.Play();
            
            controller.processingDelay = 0.09f;
            controller.LogStringWithReturn("there is a scraping sound, several loud cracks. Ohm's head turns, the stone they're pressed against tearing the flesh on their face and breaking the bones.");
            controller.LogStringWithReturn("a bleeding, mangled face stares at you with eyes glowing the color of the orb. their hand quickly wraps around your throat. you struggle to break free from the grip, but it is too tight. you cannot breathe.");
            controller.checkpointManager.SetCheckpoint(25);

            fc.TriggerEndingSequenceSecond();

        }

        if (response == "yes" && controller.checkpointManager.checkpoint == 22)
        {
            FinalCaveController fc = (FinalCaveController) controller;

            fc.audio.clip = fc.OhmsGrasp;
            fc.audio.volume = .4f;
            fc.audio.pitch = 1f;
            fc.audio.Play();
            controller.LogStringWithReturn("you grab their hand and attempt to pull. however ");
			controller.DisplayLoggedText();
            controller.processingDelay = 0.09f;
			controller.LogStringWithReturn("their grip on your hand tightens to the point of pain. you feel something crunch in your hand.");
			controller.LogStringWithReturn("there is a scraping sound, several loud cracks. Ohm's head turns, the stone they're pressed against tearing the flesh on their face and breaking the bones.");
			controller.LogStringWithReturn("a bleeding, mangled face stares at you with eyes glowing the color of the orb. their hand releases yours and quickly wraps around your throat. you struggle to break free from the grip, but it is too tight. you cannot breathe.");
            controller.checkpointManager.SetCheckpoint(25);

            fc.TriggerEndingSequence();
        }

        if (continuing == false)
        {
            controller.isConversing = false;
            controller.UpdateRoomChoices(controller.startingActions);
        }

    }

    private static void SetOhmInteraction(IController controller)
    {
        List<Interaction> interactions =
            new List<Interaction>(controller.characters.First(o => o.noun.Equals("Ohm")).interactions);
        Interaction interaction = interactions.Find(o => o.action.keyword.Equals("interact"));
        interaction.textResponse = "they look at you strangely.";
        interaction.actionResponse = null;
    }
}
