using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum WorldEntity
{
    None,
    Player,
    Wall,
    Goal
}

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
    public GameObject goal_prefab;

    // Generated
    private GameObject terrain;

    // Constructs an empty world grid and matching plane
    void GenerateWorld()
    {
        var grid = new GridWorld(columns, rows, cell_width);
        GetComponent<GridBehaviour>().grid = grid;
        terrain = TerrainGenerator.ConstructTerrain(grid, cell_width, floor_material);
        // Attach our terrain as a child gameobject
        terrain.transform.parent = transform;
    }

    WorldEntity[,] GenerateWorldData()
    {
        var world_data = new WorldEntity[columns, rows];

        // Add some random walls TODO: remove
        world_data[1, 1] = WorldEntity.Wall;
        world_data[2, 2] = WorldEntity.Wall;
        world_data[1, 3] = WorldEntity.Wall;
        world_data[5, 2] = WorldEntity.Wall;
        world_data[3, 3] = WorldEntity.Wall;
        world_data[2, 3] = WorldEntity.Wall;
        world_data[5, 4] = WorldEntity.Wall;
        world_data[2, 5] = WorldEntity.Wall;

        // Place a goal
        world_data[0, 1] = WorldEntity.Goal;

        // Add our player
        world_data[0, 0] = WorldEntity.Player;

        return world_data;
    }

    void PopulateWorld(WorldEntity[,] world_data)
    {
        for (int x = 0; x < world_data.GetLength(0); x++)
        {
            for (int y = 0; y < world_data.GetLength(1); y++)
            {
                switch (world_data[x, y])
                {
                    case WorldEntity.Player:
                        SpawnPlayer(new Vector2(x, y));
                        break;
                    case WorldEntity.Goal:
                        SpawnGoal(new Vector2(x, y));
                        break;
                    case WorldEntity.Wall:
                        SpawnPrefab(block_prefab, new Vector2(x, y));
                        break;
                }

            }
        }
    }

    // Spawn block at a grid position
    void SpawnPrefab(GameObject prefab, Vector2 position, string name = null, bool isOccupier = true)
    {
        var grid_behaviour = GetComponent<GridBehaviour>();
        var world_position = grid_behaviour.GetWorldPosition(position);
        var entity = Instantiate(prefab, new Vector3(0, 0, 0), prefab.transform.rotation, this.transform);
        if (name != null)
        {
            entity.name = name;
        }

        // Add our prefab to the grid world
        grid_behaviour.AddTo(entity, position, isOccupier);
    }

    void SpawnPlayer(Vector2 position)
    {
        SpawnPrefab(player_prefab, position, "Player");
    }

    void SpawnGoal(Vector2 position)
    {
        SpawnPrefab(goal_prefab, position, "Goal", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateWorld();
        PopulateWorld(GenerateWorldData());
    }
}
