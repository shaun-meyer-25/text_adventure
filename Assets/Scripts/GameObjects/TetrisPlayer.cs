using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisPlayer : MonoBehaviour
{
    public TetrisController Controller;
    
    private Spawner _spawner;
    private static readonly int Fade = Shader.PropertyToID("_Fade");
    private List<GameObject> blocksAtEnd = new List<GameObject>();
    private bool endingTriggered;
    
    // Start is called before the first frame update
    void Start()
    {
        _spawner = FindObjectOfType<Spawner>();
        endingTriggered = false;
    }

    public IEnumerator DissolveBlocks()
    {
        float fade = 1f;

        while (fade >= 0)
        {
            foreach (var block in blocksAtEnd)
            {
                block.GetComponent<SpriteRenderer>().material.SetFloat(Fade, fade);
            }

            fade -= .01f;
            yield return null;
        }
    }

    void Ending()
    {
        StartCoroutine(DissolveBlocks());
        Controller.LogStringWithReturn("you did it.");
        Controller.DisplayLoggedText();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (IsCompletelySurrounded() && !endingTriggered)
        {
            Destroy(_spawner);
            for (int y = -3; y < Playfield.h - 3; ++y) 
            for (int x = -4; x < Playfield.w - 4; ++x)
                if (Playfield.grid[x + 4, y + 3] != null)
                {
                    blocksAtEnd.Add(Playfield.grid[x + 4, y + 3].gameObject);
                }

            endingTriggered = true;
            Ending();
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Modify position
            transform.position += new Vector3(-1, 0, 0);
       
            // See if valid
            if (isValidPos())
                // It's valid. Update grid.
                updateGrid(-1);
            else
                // It's not valid. revert.
                transform.position += new Vector3(1, 0, 0);
        }

        // Move Right
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            // Modify position
            transform.position += new Vector3(1, 0, 0);
       
            // See if valid
            if (isValidPos())
                // It's valid. Update grid.
                updateGrid(1);
            else
                // It's not valid. revert.
                transform.position += new Vector3(-1, 0, 0);
        }
    }

    bool IsCompletelySurrounded()
    {
        Vector2 v = Playfield.roundVec2(transform.position);
        int adjustedX = (int) v.x + 4;
        int adjustedY = (int) v.y + 3;

        Vector2 left = new Vector2(adjustedX - 1, adjustedY);
        Vector2 right = new Vector2(adjustedX + 1, adjustedY);

        if (
            (!Playfield.insideBorder(left) || Playfield.grid[adjustedX - 1, adjustedY] != null) &&
            (!Playfield.insideBorder(right) || Playfield.grid[adjustedX + 1, adjustedY] != null) &&
            Playfield.grid[adjustedX, adjustedY + 1] != null
        )
        {
            return true;
        }
        return false;
    }
    
    bool isValidPos()
    {
        Vector2 v = Playfield.roundVec2(transform.position);

        try
            {
                if (!Playfield.insideBorder(v)) return false;
                if (Playfield.grid[(int) v.x + 4, (int) v.y + 3] != null) return false;
            }
            catch (IndexOutOfRangeException e)
            {
            }

        return true;
    }

    private void updateGrid(int direction)
    {
        Vector2 v = Playfield.roundVec2(transform.position);

        int adjustedX = (int) v.x + 4;
        int adjustedY = (int) v.y + 3;

        try
        {
            for (int y = -3; y < Playfield.h - 3; ++y) 
            for (int x = -4; x < Playfield.w - 4; ++x) 
                if (Playfield.grid[x + 4, y + 3] != null)
                    if (Playfield.grid[x + 4, y + 3] == transform)
                        Playfield.grid[x + 4, y + 3] = null;
            
            Playfield.grid[adjustedX, adjustedY] = transform;

        }
        catch (IndexOutOfRangeException e)
        {
            Debug.Log(v);
        }
    }
}
