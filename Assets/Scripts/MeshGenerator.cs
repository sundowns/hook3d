using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
    public static Mesh ConstructWorldMesh(GridWorld world, int cell_width)
    {
        // create a mesh from our grid
        var mesh = new Mesh();
        var vertices = new Vector3[] {
            new Vector3(0, 0, 0) ,
            new Vector3(world.width * cell_width, 0, 0) ,
            new Vector3(world.width * cell_width, 0, world.height * cell_width),
            new Vector3(0, 0, world.height * cell_width)
        };
        mesh.vertices = vertices;
        mesh.uv = new Vector2[] {
            new Vector2(0,0),
            new Vector2(0,1),
            new Vector2(1,1),
            new Vector2(1,0)
        };
        mesh.triangles = new int[] {
            0,2,1,0,3,2
        };

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        return mesh;
    }

}
