﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPosition : MonoBehaviour
{
    public Vector2 position;
    public bool isMoving = false;
    public bool isOccupier = true;

    public void translate(Vector2 delta)
    {
        position = position + delta;
    }

    public void set(Vector2 position)
    {
        this.position = position;
    }
}
