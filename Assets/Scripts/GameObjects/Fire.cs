using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public FireLevel fireLevel;

    private GameController controller;
    
    public enum FireLevel
    {
        Dead, Low, Roaring
    }    
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<GameController>();
        fireLevel = FireLevel.Low;
    }

    public bool FeedFire()
    {
        if (fireLevel.Equals(FireLevel.Low))
            {
                fireLevel = FireLevel.Roaring;
                controller.LogStringWithReturn("the fire is now roaring.");
                return true;
        } else if (fireLevel.Equals(FireLevel.Roaring))
            {
                controller.LogStringWithReturn("the fire does not need to be fed.");
                return false;
            }
        else
            {
                controller.LogStringWithReturn("the fire is dead, it must be started.");
                return false;
            }
        }
}
