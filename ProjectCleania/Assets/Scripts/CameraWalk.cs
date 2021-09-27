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

    private Vector3[] terrainVertexes;
    private Vector3 terrainCenter;
    private Terrain[] terrains;
    private float terrainWidth;
    private float terrainHeight;

    private void Awake()
    {
        miniMapCamera = GetComponent<Camera>();
        terrains = FindObjectsOfType<Terrain>();
    }

    private void Start()
    {
        terrainVertexes = new Vector3[4];
        cameraViewingSize = miniMapCamera.orthographicSize * 2; // Size x 2가 실제 보는 사각형 사이즈

        // 모든 터레인의 꼭지점을 구해서 터레인 집합의 중점을 구한다.
        terrainCenter = Vector3.zero;
        terrainWidth = terrains[0].terrainData.size.x;
        terrainHeight = terrains[0].terrainData.size.z;

        // 꼭지점 4곳을 '전체 터레인이 회전하지 않음'을 가정하여 구함.
        if (terrains.Length % 2 == 0)
        {
            terrainWidth = (terrains.Length / 4) * terrainWidth;
            terrainHeight = (terrains.Length / 4) * terrainHeight;
        }
        else
        {
            terrainWidth = Mathf.Floor(terrains.Length / 4) * terrainWidth + terrainWidth * 0.5f;
            terrainHeight = Mathf.Floor(terrains.Length / 4) * terrainHeight + terrainHeight * 0.5f;
        }

        terrainVertexes[0] = new Vector3(terrainCenter.x + terrainWidth * 0.5f, terrainCenter.y, terrainCenter.z + terrainHeight * 0.5f);
        terrainVertexes[1] = new Vector3(terrainCenter.x + terrainWidth * 0.5f, terrainCenter.y, terrainCenter.z - terrainHeight * 0.5f);
        terrainVertexes[2] = new Vector3(terrainCenter.x - terrainWidth * 0.5f, terrainCenter.y, terrainCenter.z - terrainHeight * 0.5f);
        terrainVertexes[3] = new Vector3(terrainCenter.x - terrainWidth * 0.5f, terrainCenter.y, terrainCenter.z + terrainHeight * 0.5f);
        foreach (Vector3 vec in terrainVertexes)
        {
            print(vec);
        }
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
    //    Gizmos.DrawSphere(Vector3.zero, 2);

    //    if (terrainVertexes.Length == 4)
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawSphere(terrainVertexes[0], 2);
    //        Gizmos.DrawSphere(new Vector3(terrainVertexes[0].x - cameraViewingSize * 0.5f, terrainVertexes[0].y, terrainVertexes[0].z - cameraViewingSize * 0.5f), 2);

    //        Gizmos.color = Color.green;
    //        Gizmos.DrawSphere(terrainVertexes[1], 2);
    //        Gizmos.DrawSphere(new Vector3(terrainVertexes[1].x - cameraViewingSize * 0.5f, terrainVertexes[1].y, terrainVertexes[1].z + cameraViewingSize * 0.5f), 2);
            
    //        Gizmos.color = Color.blue;
    //        Gizmos.DrawSphere(terrainVertexes[2], 2);
    //        Gizmos.DrawSphere(new Vector3(terrainVertexes[2].x + cameraViewingSize * 0.5f, terrainVertexes[2].y, terrainVertexes[2].z + cameraViewingSize * 0.5f), 2);
            
    //        Gizmos.color = Color.cyan;
    //        Gizmos.DrawSphere(terrainVertexes[3], 2);
    //        Gizmos.DrawSphere(new Vector3(terrainVertexes[3].x + cameraViewingSize * 0.5f, terrainVertexes[3].y, terrainVertexes[3].z - cameraViewingSize * 0.5f), 2);
    //    }
    //}
}


/*
 
 CameraWalk
- 기능 : 플레이어를 미니맵 규칙에 맞게 따라다닌다.
- 필수 요소
    - 맵의 꼭지점
    - 미니맵이 보고 있는 크기
    - 플레이어 위치
- 구현 방법
    - 맵의 특정 내 구역에 플레이어가 위치할 때 카메라가 어떻게 따라다닐지 설정.

 */
