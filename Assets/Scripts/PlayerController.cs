using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Left,
    Right,
    Up,
    Down,
    None
}

public static class DirectionExtensions
{
    public static Vector2 GetDelta(this Direction direction)
    {
        switch (direction)
        {
            case Direction.Down:
                return new Vector2(1, 0);
            case Direction.Up:
                return new Vector2(-1, 0);
            case Direction.Left:
                return new Vector2(0, -1);
            case Direction.Right:
                return new Vector2(0, 1);
            default:
                return new Vector2(0, 0);
        }
    }
}

public class PlayerController : MonoBehaviour
{
    private bool can_throw_hook;

    private GridBehaviour grid_controller;

    void Start()
    {
        grid_controller = GameObject.FindWithTag("World").GetComponent<GridBehaviour>();
        can_throw_hook = true;
    }

    // Update is called once per frame
    void Update()
    {
        Direction action = Direction.None;

        if (Input.GetButtonDown("Left"))
        {
            action = Direction.Left;
        }
        else if (Input.GetButtonDown("Right"))
        {
            action = Direction.Right;
        }
        else if (Input.GetButtonDown("Up"))
        {
            action = Direction.Up;
        }
        else if (Input.GetButtonDown("Down"))
        {
            action = Direction.Down;
        }

        if (action != Direction.None)
        {
            grid_controller.AttemptMove(this.gameObject, action.GetDelta());
        }


        if (this.can_throw_hook)
        {
            if (Input.GetButtonDown("Fire"))
            {
                grid_controller.AttemptFire(this.gameObject, action.GetDelta());
            }
        }
    }


}
