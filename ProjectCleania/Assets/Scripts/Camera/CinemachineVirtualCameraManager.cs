using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineVirtualCameraManager : MonoBehaviour
{
    [SerializeField]
    Cinemachine.CinemachineVirtualCamera cVCamera;

    public float ShakeDuration = 1.0f;
    public float ShakeFrequency = 1.0f;

    void Start()
    {
        //cVCamera.Follow = GameManager.Instance.SinglePlayer.transform;
        //cVCamera.LookAt = GameManager.Instance.SinglePlayer.transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            CameraShakeBegin(ShakeFrequency);
    }

    public void CameraShakeBegin(float Amplitude = 1.0f)
    {
        cVCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = ShakeFrequency;
        cVCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Amplitude;
        Invoke("CameraShakeEnd", ShakeDuration);
    }

    void CameraShakeEnd()
    {
        cVCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
    }
}
