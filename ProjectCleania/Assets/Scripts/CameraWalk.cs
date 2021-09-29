using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWalk : MonoBehaviour
{
    public GameObject player;
    public float speed = 4.0f;
    public Vector3 cameraPosition;
    private Camera miniMapCamera;
    private float cameraViewingSize;

    public Vector3 TerrainCenter = Vector3.zero;
    private Vector3[] terrainVertexes;
    private Terrain[] terrains;
    private float terrainWidth;
    private float terrainHeight;

    private void Awake()
    {
        miniMapCamera = GetComponent<Camera>();
        terrains = FindObjectsOfType<Terrain>();
        print("terrains.Length : " + terrains.Length);
    }

    private void Start()
    {
        terrainVertexes = new Vector3[4];
        cameraViewingSize = miniMapCamera.orthographicSize * 2; // Size x 2�� ���� ���� �簢�� ������

        // ��� �ͷ����� �������� ���ؼ� �ͷ��� ������ ������ ���Ѵ�.
        terrainWidth = terrains[0].terrainData.size.x;
        terrainHeight = terrains[0].terrainData.size.z;

        // ������ 4���� '��ü �ͷ����� ȸ������ ����'�� �����Ͽ� ����.
        if (terrains.Length % 2 == 0)
        {
            terrainWidth = (terrains.Length / 4) * terrainWidth;
            terrainHeight = (terrains.Length / 4) * terrainHeight;
        }
        else
        {
            terrainWidth = Mathf.Floor(terrains.Length / 4) * terrainWidth + terrainWidth;
            terrainHeight = Mathf.Floor(terrains.Length / 4) * terrainHeight + terrainHeight;
        }

        terrainVertexes[0] = new Vector3(TerrainCenter.x + terrainWidth * 0.5f, TerrainCenter.y, TerrainCenter.z + terrainHeight * 0.5f);
        terrainVertexes[1] = new Vector3(TerrainCenter.x + terrainWidth * 0.5f, TerrainCenter.y, TerrainCenter.z - terrainHeight * 0.5f);
        terrainVertexes[2] = new Vector3(TerrainCenter.x - terrainWidth * 0.5f, TerrainCenter.y, TerrainCenter.z - terrainHeight * 0.5f);
        terrainVertexes[3] = new Vector3(TerrainCenter.x - terrainWidth * 0.5f, TerrainCenter.y, TerrainCenter.z + terrainHeight * 0.5f);
        //foreach (Vector3 vec in terrainVertexes)
        //{
        //    print(vec);
        //}
    }

    void Update()
    {
        if ((player.transform.position.x > (terrainVertexes[0].x - cameraViewingSize * 0.5) && player.transform.position.z > (terrainVertexes[0].z) - cameraViewingSize * 0.5) ||
            (player.transform.position.x > (terrainVertexes[1].x - cameraViewingSize * 0.5) && player.transform.position.z < (terrainVertexes[1].z + cameraViewingSize * 0.5)) ||
            (player.transform.position.x < (terrainVertexes[2].x + cameraViewingSize * 0.5) && player.transform.position.z < (terrainVertexes[2].z + cameraViewingSize * 0.5)) ||
            (player.transform.position.x < (terrainVertexes[3].x + cameraViewingSize * 0.5) && player.transform.position.z > (terrainVertexes[3].z - cameraViewingSize * 0.5)))
        {
            // print("Conner!");
        }
        else if ((player.transform.position.x > (terrainVertexes[0].x - cameraViewingSize * 0.5)) || (player.transform.position.x < (terrainVertexes[2].x + cameraViewingSize * 0.5)))
        {
            // print("Left or Right!");
            transform.position = new Vector3(transform.position.x, player.transform.position.y, player.transform.position.z) + cameraPosition;
        }
        else if ((player.transform.position.z > (terrainVertexes[0].z - cameraViewingSize * 0.5)) || (player.transform.position.z < (terrainVertexes[1].z + cameraViewingSize * 0.5)))
        {
            // print("Top or bottom!");
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z) + cameraPosition;
        }
        else
        {
            // print("Near center!");
            transform.position = player.transform.position + cameraPosition;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(TerrainCenter, 10);

    //    if (terrainVertexes.Length == 4)
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawSphere(terrainVertexes[0], 5);
    //        Gizmos.DrawSphere(new Vector3(terrainVertexes[0].x - cameraViewingSize * 0.5f, terrainVertexes[0].y, terrainVertexes[0].z - cameraViewingSize * 0.5f), 5);

    //        Gizmos.color = Color.green;
    //        Gizmos.DrawSphere(terrainVertexes[1], 5);
    //        Gizmos.DrawSphere(new Vector3(terrainVertexes[1].x - cameraViewingSize * 0.5f, terrainVertexes[1].y, terrainVertexes[1].z + cameraViewingSize * 0.5f), 5);

    //        Gizmos.color = Color.blue;
    //        Gizmos.DrawSphere(terrainVertexes[2], 5);
    //        Gizmos.DrawSphere(new Vector3(terrainVertexes[2].x + cameraViewingSize * 0.5f, terrainVertexes[2].y, terrainVertexes[2].z + cameraViewingSize * 0.5f), 5);

    //        Gizmos.color = Color.cyan;
    //        Gizmos.DrawSphere(terrainVertexes[3], 5);
    //        Gizmos.DrawSphere(new Vector3(terrainVertexes[3].x + cameraViewingSize * 0.5f, terrainVertexes[3].y, terrainVertexes[3].z - cameraViewingSize * 0.5f), 5);
    //    }
    //}
}


/*
 
 CameraWalk
- ��� : �÷��̾ �̴ϸ� ��Ģ�� �°� ����ٴѴ�.
- �ʼ� ���
    - ���� ������
    - �̴ϸ��� ���� �ִ� ũ��
    - �÷��̾� ��ġ
- ���� ���
    - ���� Ư�� �� ������ �÷��̾ ��ġ�� �� ī�޶� ��� ����ٴ��� ����.

 */
