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
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            // Modify position
            transform.position += new Vector3(-1, 0, 0);
       
            // See if valid
            if (isValidPos())
                // It's valid. Update grid.
                updateGrid();
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
                updateGrid();
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
                if (Playfield.grid[(int) v.x + 4, (int) v.y + 3] != null &&
                    Playfield.grid[(int) v.x + 4, (int) v.y + 3].parent != transform) return false;
            }
            catch (IndexOutOfRangeException e)
            {
            }

        return true;
    }

    private void updateGrid()
    {
        for (int y = -3; y < Playfield.h - 3; ++y) 
        for (int x = -4; x < Playfield.w - 4; ++x) 
            if (Playfield.grid[x + 4, y + 3] != null)
                if (Playfield.grid[x + 4, y + 3] == transform)
                    Playfield.grid[x + 4, y + 3] = null;
        
        Vector2 v = Playfield.roundVec2(transform.position);

        Playfield.grid[(int) v.x + 4, (int) v.y + 3] = transform;
    }
}
