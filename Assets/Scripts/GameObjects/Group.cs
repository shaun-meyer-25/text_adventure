using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Group : MonoBehaviour
{
    private float lastFall = 0;
    private Spawner _spawner;
    private Button left;
    private Button right;

    // Start is called before the first frame update
    void Start()
    {
        GameObject button1 = GameObject.Find("Option2");
        GameObject button2 = GameObject.Find("Option3");

        left = button1.GetComponent<Button>();
        right = button2.GetComponent<Button>();
        
        left.onClick.AddListener(MoveLeft);
        right.onClick.AddListener(MoveRight);
        
        _spawner = FindObjectOfType<Spawner>();
        if (!isValidGridPos())
        {
            Debug.Log("GAME OVER");
            Destroy(gameObject);
        }
    }

    public void MoveLeft()
    {
        transform.position += new Vector3(-1, 0, 0);
       
        // See if valid
        if (isValidGridPos())
            // It's valid. Update grid.
            updateGrid();
        else
            // It's not valid. revert.
            transform.position += new Vector3(1, 0, 0);
    }

    public void MoveRight()
    {
        transform.position += new Vector3(1, 0, 0);
       
        // See if valid
        if (isValidGridPos())
            // It's valid. Update grid.
            updateGrid();
        else
            // It's not valid. revert.
            transform.position += new Vector3(-1, 0, 0);
    }
    
    // Update is called once per frame
    void Update() {
        // Rotate
    if (Input.GetKeyDown(KeyCode.UpArrow)) {
        transform.Rotate(0, 0, -90);
       
        // See if valid
        if (isValidGridPos())
            // It's valid. Update grid.
            updateGrid();
        else
            // It's not valid. revert.
            transform.Rotate(0, 0, 90);
    }

    // Move Downwards and Fall
    else if (Input.GetKeyDown(KeyCode.DownArrow) ||
             Time.time - lastFall >= 1) {
        // Modify position
        transform.position += new Vector3(0, -1, 0);

        // See if valid
        if (isValidGridPos()) {
            // It's valid. Update grid.
            updateGrid();
        } else {
            // It's not valid. revert.
            transform.position += new Vector3(0, 1, 0);

            // Clear filled horizontal lines
            //Playfield.deleteFullRows();

            // Spawn next Group
            _spawner.SpawnNext();

            // Disable script
            enabled = false;
            left.onClick.RemoveListener(MoveLeft);
            right.onClick.RemoveListener(MoveRight);
        }

        lastFall = Time.time;
    }
}

    bool isValidGridPos()
    {
        // the child is spawning outside the border rn. need to fix that
        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.roundVec2(child.position);

            try
            {
                if (!Playfield.insideBorder(v)) return false;
                if (Playfield.grid[(int) v.x + 4, (int) v.y + 3] != null &&
                    Playfield.grid[(int) v.x + 4, (int) v.y + 3].parent != transform) return false;
            }
            catch (IndexOutOfRangeException e)
            {
            }
        }

        return true;
    }

    void updateGrid()
    {
        for (int y = -3; y < Playfield.h - 3; ++y) 
            for (int x = -4; x < Playfield.w - 4; ++x) 
                if (Playfield.grid[x + 4, y + 3] != null)
                    if (Playfield.grid[x + 4, y + 3].parent == transform)
                        Playfield.grid[x + 4, y + 3] = null;

        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.roundVec2(child.position);
            Playfield.grid[(int) v.x + 4, (int) v.y + 3] = child;
        }
    }
}
