using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSpawner : MonoBehaviour
{
    public float ZRotation;
    public float XSpeed;
    public float YSpeed;
    public GameObject snakePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnSnake()
    {
        GameObject go = Instantiate(snakePrefab, new Vector3(transform.position.x, transform.position.y,0 ), Quaternion.identity);
        Snakes s = go.GetComponent<Snakes>();
        s.XSpeed = XSpeed;
        s.YSpeed = YSpeed;
        go.transform.Rotate(0,0, ZRotation);
    }
}
