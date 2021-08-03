using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("hello");
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

        // todo  -we arent deleting the previous one right. i think we had it right before.
        try
        {
            Playfield.grid[adjustedX, adjustedY] = transform;
            if (Playfield.grid[adjustedX + direction, adjustedY] == transform)
                Playfield.grid[adjustedX + direction, adjustedY] = null;
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.Log(v);
        }
    }
}
