using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BearHitboxes : MonoBehaviour
{
    public Collider2D headHitbox;
    public Collider2D fatalHitbox;
    public Collider2D nonfatalHitbox;
    public Animator animator;
    private Camera _camera;
    public IController Controller;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bool isFatal = false;
            bool isHead = false;
            bool inCollider = false;
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            if (headHitbox.OverlapPoint(mousePosition))
            {
                isHead = true;
                inCollider = true;
            }

            if (fatalHitbox.OverlapPoint(mousePosition))
            {
                inCollider = true;
                isFatal = true;
            }

            if (nonfatalHitbox.OverlapPoint(mousePosition))
            {
                inCollider = true;
                
            }

            if (!isFatal && inCollider)
            {
                if (isHead)
                {
                    Controller.LogStringWithReturn("the bones in its head are too strong. you will only injure it this way.");
                }
                else
                {
                    Controller.LogStringWithReturn("you will only injure it this way. you must strike its heart.");
                }
                animator.enabled = true;
                animator.GetComponentInParent<SpriteRenderer>().enabled = true;
                animator.SetTrigger("BearChomp");
                StartCoroutine(DisableAfter2());
            } else if (isFatal)
            {
                StartCoroutine(ChangeSceneAfter5());
            }

        }
    }

    IEnumerator DisableAfter2()
    {
        yield return new WaitForSeconds(2);

        animator.enabled = false;
        animator.GetComponentInParent<SpriteRenderer>().enabled = false;
    }

    IEnumerator ChangeSceneAfter5()
    {
        Controller.LogStringWithReturn("that blow will be fatal. the bear will fall to you.");
        yield return new WaitForSeconds(5);
        // Controller.checkpointManager.SetCheckpoint(11); this is being set statically below
        StaticDataHolder.instance.Checkpoint = 11;
        Controller.levelLoader.LoadScene("Second Day Afternoon");
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

    }
}
