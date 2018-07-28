using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputAction : Choice {
	public abstract void RespondToInput (GameController controller, string[] separatedInputWords);
}
