using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehaviour : MonoBehaviour
{
    public GridWorld grid;

    [Range(0, 5)]
    public float move_tween_duration;

    public Vector3 GetWorldPosition(Vector2 grid_position)
    {
        return this.grid.GetWorldPosition(grid_position);
    }

    // TODO: add to occupancy grid and do occupancy checks when we have that
    public void AddTo(GameObject entity, Vector2 grid_position)
    {
        if (!grid.IsOccupied(grid_position))
        {
            entity.AddComponent(typeof(GridPosition));
            Move(entity, grid_position, false);
        }
        else
        {
            Debug.LogWarning($"Attempted to add entity to already occupied position: {grid_position}");
        }
    }

    public void AttemptMove(GameObject entity, Vector2 delta)
    {
        var grid_locked = entity.GetComponent<GridPosition>();
        if (!grid_locked.isMoving)
        {
            var target = grid_locked.position + delta;
            // check out target cell is a valid grid coordinate
            if (target.x >= 0 && target.x < grid.width && target.y >= 0 && target.y < grid.height && !grid.IsOccupied(target))
            {
                Move(entity, target);
            }
        }
        // TODO: else: can we buffer the movement?
    }

    // Assumes the target position is a valid move
    private void Move(GameObject entity, Vector2 grid_position, bool tween = true)
    {
        var grid_locked = entity.GetComponent<GridPosition>();
        // mark the entity's old cell as empty
        grid.RemoveOccupant(grid_locked.position);

        // update our grid position
        grid_locked.set(grid_position);
        Vector3 world_pos = GetWorldPosition(grid_position);
        var entity_size = entity.GetComponent<MeshRenderer>().bounds.size;
        var final_pos = new Vector3(world_pos.x, entity_size.y / 2, world_pos.z);

        // occupy the new cell
        grid.SetOccupant(entity, grid_position);

        if (tween)
            StartCoroutine(MoveGradually(entity, final_pos, move_tween_duration));
        else
            entity.transform.position = final_pos;
    }

    private IEnumerator MoveGradually(GameObject entity, Vector3 target_position, float duration)
    {
        var grid_locked = entity.GetComponent<GridPosition>();
        grid_locked.isMoving = true;

        float elapsedTime = 0;
        float ratio = elapsedTime / duration;
        Vector3 start_position = entity.transform.position;
        while (ratio < 1f)
        {
            elapsedTime += Time.deltaTime;
            ratio = elapsedTime / duration;
            entity.transform.position = Vector3.Lerp(start_position, target_position, ratio);
            yield return null;
        }
        grid_locked.isMoving = false;
    }
}
