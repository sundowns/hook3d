using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class WorldGenerator : MonoBehaviour
{
    // Parameters
    [Range(1, 100)]
    public int cell_width = 10;
    [Range(1, 100)]
    public int columns = 4;
    [Range(1, 100)]
    public int rows = 4;

    // Prefabs
    public GameObject block_prefab;

    // Generated
    private GridWorld grid;
    private Mesh mesh;
    private Vector3 grid_origin;

    void GenerateWorld()
    {
        Debug.Log("Generating new grid");
        grid = new GridWorld(columns, rows, cell_width);
        Debug.Log("Generating Mesh");
        mesh = MeshGenerator.ConstructWorldMesh(grid, cell_width);
    }

    void UpdateMesh()
    {
        if (mesh)
        {
            GetComponent<MeshFilter>().mesh = mesh;
            GetComponent<MeshCollider>().sharedMesh = mesh;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            this.grid_origin = new Vector3(-grid.width / 2 * cell_width, 0, -grid.height / 2 * cell_width);
        }
        else
        {
            Debug.Log("Attempted to update mesh but mesh is not defined!");
        }
    }

    // Spawn player at a grid position
    void SpawnBlock(Vector2 position)
    {
        var world_position = grid.GetWorldPosition(position);
        var block = Instantiate(block_prefab, new Vector3(world_position.x, 10, world_position.y), Quaternion.identity);
        block.transform.parent = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateWorld();
        UpdateMesh();

        // SpawnPlayer(new Vector2(2, 2));
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                SpawnBlock(new Vector2(x, y));
            }
        }
    }
}
