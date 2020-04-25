using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TerrainGenerator
{
    public static GameObject ConstructTerrain(GridWorld world, int cell_width, Material material)
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


        // Create a gameobject here
        GameObject terrain = new GameObject("Terrain");
        MeshFilter filter = terrain.AddComponent(typeof(MeshFilter)) as MeshFilter;
        filter.mesh = mesh;
        MeshCollider collider = terrain.AddComponent(typeof(MeshCollider)) as MeshCollider;
        collider.sharedMesh = mesh;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        MeshRenderer terrain_renderer = terrain.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        terrain_renderer.material = material;
        // Set the texture scale so it tiles across our mesh
        terrain_renderer.material.SetTextureScale("_MainTex", new Vector2(world.width, world.height));

        return terrain;
    }

}
