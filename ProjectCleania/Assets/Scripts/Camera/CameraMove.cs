using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    GameObject player;
    public Vector3 cameraPosition;

    void Start()
    {
        player = GameManager.Instance.SinglePlayer;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = player.transform.position + cameraPosition;
    }
}
