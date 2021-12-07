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
        CombineInstance[] combine = new CombineInstance[529];

        int ct = 0;
        for(int i = 0; i < 23; ++i)
        {
            for (int j = 0; j < 23; ++j)
            {
                combine[ct].mesh = meshFilter.sharedMesh;
                meshFilter.transform.position = new Vector3(i * 10f, 5f, j * 10f);
                combine[ct++].transform = meshFilter.transform.localToWorldMatrix;
            }
        }
        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        transform.gameObject.SetActive(true);
        transform.position = new Vector3(0, 50, 0);

        GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().mesh;
    }
}
