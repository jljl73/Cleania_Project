using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    AudioSource audioSource;

    [Header("효과음")]
    [SerializeField] AudioClip m_QuestAssign;
    [SerializeField] AudioClip m_QuestClear;
    [SerializeField] AudioClip m_QuestReward;
    [SerializeField] AudioClip m_Inventory;
    [SerializeField] AudioClip m_Storage;
    [SerializeField] AudioClip m_NPCInteraction;
    [SerializeField] AudioClip m_Equip;
    [SerializeField] AudioClip m_ItemClick;
    [SerializeField] AudioClip m_ItemBuySell;

    [Header("플레이어")]
    [SerializeField] AudioClip playerskill1;
    [SerializeField] AudioClip playerskill2;
    [SerializeField] AudioClip playerskill3;
    [SerializeField] AudioClip playerskill4;
    //[SerializeField] AudioClip m_Storage;
    //[SerializeField] AudioClip m_NPCInteraction;
    //[SerializeField] AudioClip m_Equip;
    //[SerializeField] AudioClip m_ItemClick;
    //[SerializeField] AudioClip m_ItemBuySell;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameManager.Instance.soundPlayer = this;
    }

    public enum TYPE { QuestAssign, QuestClear, QuestReward, Inventory, Storage, NPCInteraction, Equip,
        ItemClick, ItemBuySell,
    };

    public void PlaySound(TYPE type, int index)
    {
        switch (type)
        {
            case TYPE.QuestAssign:
                audioSource.clip = m_QuestAssign;
                break;
            case TYPE.QuestClear:
                audioSource.clip = m_QuestClear;
                break;
            case TYPE.QuestReward:
                audioSource.clip = m_QuestReward;
                break;
            case TYPE.Inventory:
                audioSource.clip = m_Inventory;
                break;
            case TYPE.Storage:
                audioSource.clip = m_Inventory;
                break;
            case TYPE.NPCInteraction:
                audioSource.clip = m_NPCInteraction;
                break;
            case TYPE.Equip:
                audioSource.clip = m_Equip;
                break;
        }
        audioSource.Play();
    }
}
