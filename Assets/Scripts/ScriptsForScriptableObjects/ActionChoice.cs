using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionChoice : Choice {
	public abstract void RespondToAction(IController controller, string[] separatedInputWords);
}
