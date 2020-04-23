using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehaviour : MonoBehaviour
{
    public GridWorld grid;

    public Vector2 GetWorldPosition(Vector2 grid_position)
    {
        return this.grid.GetWorldPosition(grid_position);
    }

    public void AddTo(GameObject entity, Vector2 grid_position)
    {
        // TODO: add to occupancy grid and do occupancy checks when we have that
        var grid_locked = entity.AddComponent(typeof(GridPosition)) as GridPosition;
        grid_locked.position = grid_position;
    }

    public void AttemptMove(GameObject entity, Vector2 delta)
    {
        Debug.Log($"Move by: {delta}");
        entity.GetComponent<GridPosition>()?.translate(delta);
        // TODO: lookup unity default orientation, grid should be alligned so 0,0 is top left (or whatever unity default is)
        // TODO: we are going to have to update its world position somehow
    }
}
