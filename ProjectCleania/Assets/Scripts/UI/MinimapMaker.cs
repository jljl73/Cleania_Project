using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinimapMaker : MonoBehaviour
{
    Color[] m_colors;
    private void Awake()
    {
        MeshFilter filter = GetComponent<MeshFilter>();
        // Getnavmesh data
        NavMeshTriangulation triangles = NavMesh.CalculateTriangulation();

        
        // Create new mesh with data
        Mesh mesh = new Mesh();
        mesh.vertices = triangles.vertices;
        mesh.triangles = triangles.indices;

        Vector3[] normals = new Vector3[mesh.vertices.Length];
        m_colors = new Color[mesh.vertices.Length];
        for (int i = 0; i < normals.Length; ++i)
        {
            normals[i] = Vector3.up;
            m_colors[i] = Color.red;
        }

        mesh.normals = normals;
        mesh.colors = m_colors;

        // Set to mesh filter
        filter.mesh = mesh;
    }
}
