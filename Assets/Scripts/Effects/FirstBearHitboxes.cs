using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstBearHitboxes : MonoBehaviour
{
    public Collider2D fatalHitbox;
    private Camera _camera;
    public IController Controller;
    public bool hasThrown = false;

    private void Start()
    {
        _camera = Camera.main;
        StartCoroutine(ChangeSceneAfter20());
    }

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            if (!hasThrown)
            {
                bool isFatal = false;
                bool isHead = false;
                bool inCollider = false;
                Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

                if (fatalHitbox.OverlapPoint(mousePosition))
                {
                    isFatal = true;
                }

                if (isFatal)
                {
                    Controller.LogStringWithReturn("your strike was well-placed. but you lacked the strength of arm. the beast was only wounded, and now your spear is lost.");
                    StartCoroutine(ChangeSceneAfter5());
                }
                else
                {
                    Controller.LogStringWithReturn("your aim was not true. the spear breaks on impact with the stone.");
                    StartCoroutine(ChangeSceneAfter5());
                }
                
                hasThrown = true;
            }
        }
    }
    
    IEnumerator ChangeSceneAfter5()
    {
        yield return new WaitForSeconds(5);
        // todo - bear cry out sound
        
        // Controller.checkpointManager.SetCheckpoint(4); this is being set statically below
        StaticDataHolder.instance.Checkpoint = 4;
        Controller.levelLoader.LoadScene("Main");
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

    }

    IEnumerator ChangeSceneAfter20()
    {
        yield return new WaitForSeconds(20f);
        
        StaticDataHolder.instance.Checkpoint = 4;
        Controller.levelLoader.LoadScene("Main");
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

}
