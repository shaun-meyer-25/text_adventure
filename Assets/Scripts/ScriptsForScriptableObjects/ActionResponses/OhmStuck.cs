using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/OhmStuck")]
public class OhmStuck : ActionResponse
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool DoActionResponse(IController controller)
    {
        controller.LogStringWithReturn("Ohm responds to your footfalls. they reach their free hand out, grasping at the air between you. they cannot see you.");
        List<ConversationChoice> choices = new List<ConversationChoice>();

        controller.isConversing = true;
        ConversationChoice yes = ScriptableObject.CreateInstance<ConversationChoice>();
        yes.keyword = "yes";
        ConversationChoice no = ScriptableObject.CreateInstance<ConversationChoice>();
        no.keyword = "no";
        
        choices.Add(no); 
        choices.Add(yes);

        controller.LogStringWithReturn("grab Ohm's hand?");
        controller.DisplayLoggedText();
        return true;
    }
}
