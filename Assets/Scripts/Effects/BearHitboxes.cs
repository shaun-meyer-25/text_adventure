using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BearHitboxes : MonoBehaviour
{
    public Collider2D fatalHitbox;
    private Camera _camera;
    public IController Controller;
    public bool isDead = false;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            if (!isDead)
            {
                bool isFatal = false;
                bool isHead = false;
                bool inCollider = false;
                Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

                if (fatalHitbox.OverlapPoint(mousePosition))
                {
                    isFatal = true;
                    isDead = true;
                }

                if (isFatal)
                {
                    SteamAchivements sa = FindObjectOfType<SteamAchivements>();
                    if (sa != null)
                    {
                        sa.SetAchievement("KILLED_BEAR");
                    }
                    StartCoroutine(ChangeSceneAfter5());
                }
                else
                {
                    Controller.LogStringWithReturn("reality only allows one chance. that will not be a fatal strike.");
                }
            }
        }
    }
    
    IEnumerator ChangeSceneAfter5()
    {
        Controller.LogStringWithReturn("that strike will be fatal. the bear will fall to you.");
        yield return new WaitForSeconds(5);
        // todo - bear cry out sound
        
        
        // Controller.checkpointManager.SetCheckpoint(11); this is being set statically below
        StaticDataHolder.instance.Checkpoint = 11;
        Controller.levelLoader.LoadScene("Second Day Afternoon");
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

    }
}
