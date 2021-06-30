using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beast : MonoBehaviour
{
    private bool _isChasing = false;
    private GameObject _prey;

    public float maxSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isChasing)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, _prey.transform.position, maxSpeed);
        }
    }

    public void ChasePlayer(GameObject obj)
    {
        Debug.Log("chasing player");
        _isChasing = true;
        _prey = obj;
    }
}
