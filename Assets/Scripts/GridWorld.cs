using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridWorld
{
    public int width;
    public int height;
    public int[,] grid_array;

    public GridWorld(int width, int height)
    {
        this.width = width;
        this.height = height;

        grid_array = new int[width, height];

        for (int x = 0; x < grid_array.GetLength(0); x++)
        {
            for (int y = 0; y < grid_array.GetLength(1); y++)
            {
                grid_array[x, y] = 0;
            }
        }
    }
}
