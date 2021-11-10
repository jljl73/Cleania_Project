using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    AudioSource audioSource;
    [Header("ȿ����")]
    [SerializeField] AudioClip m_QuestAssign;
    [SerializeField] AudioClip m_QuestClear;
    [SerializeField] AudioClip m_QuestReward;
    [SerializeField] AudioClip m_Inventory;
    [SerializeField] AudioClip m_Storage;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameManager.Instance.soundPlayer = this;
    }

    public void PlaySound(string value)
    {
        switch(value)
        {
            case "QuestAssign":
                audioSource.clip = m_QuestAssign;
                break;
            case "QuestClear":
                audioSource.clip = m_QuestClear;
                break;
            case "QuestReward":
                audioSource.clip = m_QuestReward;
                break;
            case "Inventory":
                audioSource.clip = m_Inventory;
                break;
            case "Storage":
                audioSource.clip = m_Storage;
                break;
        }
        audioSource.Play();
    }
}
