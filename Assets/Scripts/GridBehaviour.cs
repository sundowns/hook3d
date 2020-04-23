using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehaviour : MonoBehaviour
{
    public GridWorld grid;

    public Vector3 GetWorldPosition(Vector2 grid_position)
    {
        return this.grid.GetWorldPosition(grid_position);
    }

    public void AddTo(GameObject entity, Vector2 grid_position)
    {
        // TODO: add to occupancy grid and do occupancy checks when we have that
        entity.AddComponent(typeof(GridPosition));
        Move(entity, grid_position);
    }

    public void AttemptMove(GameObject entity, Vector2 delta)
    {
        Debug.Log($"Move by: {delta}");
        var result = entity.GetComponent<GridPosition>().position + delta;
        // TODO: occupancy check
        // check out target cell is a valid grid coordinate
        if (result.x >= 0 && result.x < grid.width && result.y >= 0 && result.y < grid.height)
        {
            // Move our entity
            Move(entity, result);
        }
    }

    private void Move(GameObject entity, Vector2 position)
    {
        // update our grid position
        entity.GetComponent<GridPosition>()?.set(position);
        Vector3 world_pos = GetWorldPosition(position);
        // place our object on the grid plane
        world_pos.y = entity.GetComponent<MeshRenderer>().bounds.size.y / 2;
        entity.transform.position = world_pos;

        // TODO: lets try some coroutine tweening :o
    }
}
