using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinimapMaker : MonoBehaviour
{
    private void Awake()
    {
        MeshFilter filter = GetComponent<MeshFilter>();
        // Getnavmesh data
        NavMeshTriangulation triangles = NavMesh.CalculateTriangulation();

        // Create new mesh with data
        Mesh mesh = new Mesh();
        mesh.vertices = triangles.vertices;
        mesh.triangles = triangles.indices;

        // Set to mesh filter
        filter.mesh = mesh;
    }
}
