using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineVirtualCameraManager : MonoBehaviour
{
    Cinemachine.CinemachineVirtualCamera cinemachineVirtualCamera;

    void Awake()
    {
        cinemachineVirtualCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }

    void Start()
    {
        cinemachineVirtualCamera.Follow = GameManager.Instance.SinglePlayer.transform;
        cinemachineVirtualCamera.LookAt = GameManager.Instance.SinglePlayer.transform;
    }
}
