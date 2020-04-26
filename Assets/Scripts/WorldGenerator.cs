using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridBehaviour))]
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
    public GameObject player_prefab;

    // Generated
    private GameObject terrain;

    void GenerateWorld()
    {
        var grid = new GridWorld(columns, rows, cell_width);
        GetComponent<GridBehaviour>().grid = grid;
        terrain = TerrainGenerator.ConstructTerrain(grid, cell_width, floor_material);
        UpdateTerrain();
    }

    void UpdateTerrain()
    {
        // Attach our terrain as a child gameobject
        terrain.transform.parent = transform;
    }

    // Spawn block at a grid position
    void SpawnPrefab(GameObject prefab, Vector2 position)
    {
        var grid_behaviour = GetComponent<GridBehaviour>();
        var world_position = grid_behaviour.GetWorldPosition(position);
        var entity = Instantiate(prefab, new Vector3(0, 0, 0), prefab.transform.rotation, this.transform);

        // Add our prefab to the grid world
        grid_behaviour.AddTo(entity, position);
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateWorld();

        SpawnPrefab(player_prefab, new Vector2(0, 0));
        SpawnPrefab(block_prefab, new Vector2(1, 1));
    }
}
