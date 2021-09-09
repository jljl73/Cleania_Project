using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float speed = 4.0f;
    public Vector3 cameraPosition;

    void Update()
    {
        transform.position = player.transform.position + cameraPosition;
    }
}
