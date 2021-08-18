using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningBear : MonoBehaviour
{
    public float Speed;

    private Rigidbody2D rbody;
    
    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 currentPos = rbody.position;

        Vector2 movement = Vector2.left * Speed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        rbody.MovePosition(newPos);
    }
}
