using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        // We add +1 to the x axis every frame.
        // Time.deltaTime is the time it took to complete the last frame
        // The result of this is that the object moves one unit on the x axis every second
        transform.position += new Vector3(-1 * Time.deltaTime, -1 * Time.deltaTime, 0);
    }
}
