using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Up, UpLeft, Left, DownLeft, Down, DownRight, Right, UpRight }

public class TwoDimensionalPlayer : MonoBehaviour
{
    Direction direction = Direction.Down;

    void Start()
    {
        
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        //Up
        if (hor == 0 && ver > 0)
            direction = Direction.Up;
        if (hor > 0 && ver > 0)
            direction = Direction.UpRight;
        if (hor > 0 && ver == 0)
            direction = Direction.Right;
        if (hor > 0 && ver < 0)
            direction = Direction.DownRight;
        if (hor == 0 && ver < 0)
            direction = Direction.Down;
        if (hor < 0 && ver < 0)
            direction = Direction.DownLeft;
        if (hor < 0 && ver == 0)
            direction = Direction.Left;
        if (hor < 0 && ver > 0)
            direction = Direction.UpLeft;

        //print("Direction: " + direction.ToString());
    }
}
