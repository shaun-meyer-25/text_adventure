using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/UseTeiShell")]
public class UseTeiShell : ActionResponse
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
        if (controller.checkpointManager.checkpoint == 22)
    }
}
