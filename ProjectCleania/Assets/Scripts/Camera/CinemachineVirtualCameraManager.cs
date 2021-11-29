using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineVirtualCameraManager : MonoBehaviour
{
    Cinemachine.CinemachineVirtualCamera cVCamera;
    
    public float ShakeDuration = 1.0f;

    void Awake()
    {
        cVCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }

    void Start()
    {
        cVCamera.Follow = GameManager.Instance.SinglePlayer.transform;
        cVCamera.LookAt = GameManager.Instance.SinglePlayer.transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            CameraShakeBegin();
    }

    public void CameraShakeBegin()
    {
        cVCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 1;
        Invoke("CameraShakeEnd", ShakeDuration);
    }

    void CameraShakeEnd()
    {
        cVCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
    }

}
