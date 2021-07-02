using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed = 1f;

    private Rigidbody2D rbody;
    private FindTeiController _controller;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        _controller = FindObjectOfType<FindTeiController>();
    }

    void FixedUpdate()
    {
        Vector2 currentPos = rbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);

        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        rbody.MovePosition(newPos);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Beast beast = other.GetComponentInParent<Beast>();
        beast.ChasePlayer(this.gameObject);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name.Contains("Beast"))
        {
            _controller.ResetLevel();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("pm trigger exit");
    }
}
