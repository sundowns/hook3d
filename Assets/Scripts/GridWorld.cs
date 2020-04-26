using UnityEngine;
public class GridWorld
{
    public int width;
    public int height;
    public int cell_width;

    public int[,] grid_array;

    public GameObject[,] occupancy_map;

    public GridWorld(int width, int height, int cell_width)
    {
        this.width = width;
        this.height = height;
        this.cell_width = cell_width;

        grid_array = new int[width, height];
        occupancy_map = new GameObject[width, height];


        for (int x = 0; x < grid_array.GetLength(0); x++)
        {
            for (int y = 0; y < grid_array.GetLength(1); y++)
            {
                grid_array[x, y] = 0;
                occupancy_map[x, y] = null;
            }
        }
    }

    public bool IsOccupied(Vector2 grid_position)
    {
        return occupancy_map[(int)grid_position.x, (int)grid_position.y] != null;
    }

    public void RemoveOccupant(Vector2 grid_position)
    {
        this.occupancy_map[(int)grid_position.x, (int)grid_position.y] = null;
    }

    public void SetOccupant(GameObject entity, Vector2 grid_position)
    {
        this.occupancy_map[(int)grid_position.x, (int)grid_position.y] = entity;
    }

    public Vector3 GetWorldPosition(Vector2 grid_position)
    {
        return new Vector3((float)cell_width / 2f + (grid_position.x * cell_width), 0, (float)cell_width / 2f + (grid_position.y * cell_width));
    }
}
