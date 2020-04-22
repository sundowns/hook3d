using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    // Parameters
    [Range(1, 100)]
    public int cell_width = 10;
    [Range(1, 100)]
    public int columns = 4;
    [Range(1, 100)]
    public int rows = 4;
    public Material floor_material;

    // Prefabs
    public GameObject block_prefab;

    // Generated
    private GridWorld grid;
    private Vector3 grid_origin;
    private GameObject terrain;

    void GenerateWorld()
    {
        Debug.Log("Generating new grid");
        grid = new GridWorld(columns, rows, cell_width);
        Debug.Log("Generating Mesh");
        terrain = TerrainGenerator.ConstructTerrain(grid, cell_width, floor_material);
        UpdateTerrain();
    }

    void UpdateTerrain()
    {
        // Attach our terrain as a child gameobject
        terrain.transform.parent = transform;
        this.grid_origin = new Vector3(-grid.width / 2 * cell_width, 0, -grid.height / 2 * cell_width);
    }

    // Spawn player at a grid position
    void SpawnBlock(Vector2 position)
    {
        var world_position = grid.GetWorldPosition(position);
        var block = Instantiate(block_prefab, new Vector3(world_position.x, 10, world_position.y), block_prefab.transform.rotation);
        block.transform.parent = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateWorld();

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
