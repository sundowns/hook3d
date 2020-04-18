using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
// [RequireComponent(typeof(MeshCollider))]
public class GridManager : MonoBehaviour
{
    private GridWorld grid;

    [MinLength(1)]
    public int cell_width;

    // Start is called before the first frame update
    void Start()
    {
        this.GenerateWorld();
    }

    void GenerateWorld()
    {
        this.grid = new GridWorld(8, 8);

        var mesh = ConstructWorldMesh();
        GetComponent<MeshFilter>().mesh = mesh;

    }

    Mesh ConstructWorldMesh(GridWorld world)
    {
        // create a mesh from our grid
        var mesh = new Mesh();
        var vertices = new Vector3[];
        for (int x = 0; x < world.grid_array.GetLength(0); x++)
        {
            for (int y = 0; y < world.grid_array.GetLength(1); y++)
            {
                // TODO: need to create a mesh from our 2D grid array :thinking:
            }
        }
    }

}
