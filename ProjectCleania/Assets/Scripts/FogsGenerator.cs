using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FogsGenerator : MonoBehaviour
{
    public MeshFilter meshFilter;

    void Awake()
    {
        CreateMesh();
    }

    void CreateMesh()
    {
        CombineInstance[] combine = new CombineInstance[441];

        int ct = 0;
        for(int i = -10; i < 11; ++i)
        {
            for (int j = -10; j < 11; ++j)
            {
                combine[ct].mesh = meshFilter.sharedMesh;
                meshFilter.transform.position = new Vector3(i * 10f, 5f, j * 10f);
                combine[ct++].transform = meshFilter.transform.localToWorldMatrix;
            }
        }
        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        transform.gameObject.SetActive(true);
        transform.position = new Vector3(0, 20, 0);

        GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().mesh;
    }
}
