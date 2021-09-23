using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class minimapTest : MonoBehaviour
{
    // Start is called before the first frame update
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
