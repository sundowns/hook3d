using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
// [RequireComponent(typeof(MeshCollider))]
public class GridManager : MonoBehaviour
{
    private GridWorld grid;

    [Range(1, 1000)]
    public int cell_width;

    // Start is called before the first frame update
    void Start()
    {
        this.GenerateWorld();
    }

    void GenerateWorld()
    {
        this.grid = new GridWorld(8, 8);
        this.ConstructWorldMesh(this.grid);
    }

    void ConstructWorldMesh(GridWorld world)
    {
        // create a mesh from our grid
        var mesh = new Mesh();
        var vertices = new Vector3[] {
            new Vector3(0, 0, 0),
            new Vector3(world.width * cell_width, 0, 0),
            new Vector3(world.width * cell_width, world.height * cell_width, 0),
            new Vector3(0, world.height * cell_width, 0)
        };
        mesh.vertices = vertices;
        mesh.uv = new Vector2[] {
            new Vector2(0,0),
            new Vector2(0,1),
            new Vector2(1,1),
            new Vector2(1,0)
        };
        mesh.triangles = new int[] {
            0,1,2,0,2,3
        };

        GetComponent<MeshFilter>().mesh = mesh;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        Debug.Log("wtf??");
    }

}
