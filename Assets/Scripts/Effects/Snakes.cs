using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snakes : MonoBehaviour
{
    public float Speed = 1.6f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(Speed * Time.deltaTime, Speed * Time.deltaTime, 0);

    }
}
