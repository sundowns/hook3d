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

    // TODO: add to occupancy grid and do occupancy checks when we have that
    public void AddTo(GameObject entity, Vector2 grid_position)
    {
        entity.AddComponent(typeof(GridPosition));
        Move(entity, grid_position, false);
    }

    // TODO: occupancy check
    public void AttemptMove(GameObject entity, Vector2 delta)
    {
        var grid_locked = entity.GetComponent<GridPosition>();
        if (!grid_locked.isMoving)
        {
            var result = grid_locked.position + delta;
            // check out target cell is a valid grid coordinate
            if (result.x >= 0 && result.x < grid.width && result.y >= 0 && result.y < grid.height)
            {
                Move(entity, result);
            }
        }
        // TODO: else: can we buffer the movement?
    }

    private void Move(GameObject entity, Vector2 position, bool tween = true)
    {
        // update our grid position
        entity.GetComponent<GridPosition>()?.set(position);
        Vector3 world_pos = GetWorldPosition(position);
        // place our object on the grid plane
        var entity_size = entity.GetComponent<MeshRenderer>().bounds.size;
        var final_pos = new Vector3(world_pos.x, entity_size.y / 2, world_pos.z);

        if (tween)
            StartCoroutine(MoveGradually(entity, final_pos, 0.35f));
        else
            entity.transform.position = world_pos;
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
