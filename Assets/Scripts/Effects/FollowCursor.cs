using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class FollowCursor : MonoBehaviour
{
    private Camera _camera;
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = mousePosition;
    }
}
