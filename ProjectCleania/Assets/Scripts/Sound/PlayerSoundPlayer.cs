using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundPlayer : MonoBehaviour
{
    AudioSource audioSource;

    [Header("플레이어")]
    [SerializeField] AudioClip playerskill1;
    [SerializeField] AudioClip playerskill2;
    [SerializeField] AudioClip playerskill3;
    [SerializeField] AudioClip playerskill4;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameManager.Instance.playerSoundPlayer = this;
    }

    public enum TYPE
    {
        Skill1, Skill2, Skill3, Skill4
    };

    public void PlaySound(TYPE type)
    {
        switch (type)
        {
            case TYPE.Skill1:
                audioSource.clip = playerskill1;
                break;
            case TYPE.Skill2:
                audioSource.clip = playerskill2;
                break;
            case TYPE.Skill3:
                audioSource.clip = playerskill3;
                break;
            case TYPE.Skill4:
                audioSource.clip = playerskill4;
                break;
        }
        audioSource.Play();
    }
}
