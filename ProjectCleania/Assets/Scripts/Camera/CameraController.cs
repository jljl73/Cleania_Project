using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    public float speed = 4.0f;
    public Vector3 cameraPosition;

    private void Start()
    {
        player = GameManager.Instance.SinglePlayer;
    }

    void Update()
    {
        transform.position = player.transform.position + cameraPosition;
    }
}
