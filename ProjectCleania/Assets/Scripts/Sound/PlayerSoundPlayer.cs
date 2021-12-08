using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundPlayer : MonoBehaviour, ISoundPlayer
{
    AudioSource audioSource;

    [Header("플레이어")]
    [SerializeField] AudioClip fairyWingsAudio;
    [SerializeField] AudioClip sweepingAudio;
    [SerializeField] AudioClip cleaningWindAudio;
    [SerializeField] AudioClip[] refreshingLeapForwardAudio;
    [SerializeField] AudioClip dustingAudio;
    [SerializeField] AudioClip dehydrationAudio;
    [SerializeField] AudioClip katharsisAudio;
    [SerializeField] AudioClip dieAudio;
    [SerializeField] AudioClip levelUpCAudio;
    [SerializeField] AudioClip drinkPotionAudio;
    [SerializeField] AudioClip hPRestoreAudio;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameManager.Instance.playerSoundPlayer = this;
    }

    public enum TYPE
    {
        FairyWings, Sweeping, CleaningWind, RefreshingLeapForward, Dusting, Dehydration, Katharsis, Die, LevelUp, DrinkPotion, HPRestore
    };

    public void PlaySound(TYPE type, int idx = 0)
    {
        switch (type)
        {
            case TYPE.FairyWings:
                audioSource.clip = fairyWingsAudio;
                break;
            case TYPE.Sweeping:
                audioSource.clip = sweepingAudio;
                break;
            case TYPE.CleaningWind:
                audioSource.clip = cleaningWindAudio;
                break;
            case TYPE.RefreshingLeapForward:
                audioSource.clip = refreshingLeapForwardAudio[idx];
                break;
            case TYPE.Dusting:
                audioSource.clip = dustingAudio;
                break;
            case TYPE.Dehydration:
                audioSource.clip = dehydrationAudio;
                break;
            case TYPE.Katharsis:
                audioSource.clip = katharsisAudio;
                break;
            case TYPE.Die:
                audioSource.clip = dieAudio;
                break;
            case TYPE.LevelUp:
                audioSource.clip = levelUpCAudio;
                break;
            case TYPE.DrinkPotion:
                audioSource.clip = drinkPotionAudio;
                break;
            case TYPE.HPRestore:
                audioSource.clip = hPRestoreAudio;
                break;
        }
        audioSource.Play();
    }
    public void StopSound()
    {
        audioSource.Stop();
    }


    public void ChangeVolume(float volume)
    {
        
        GetComponent<AudioSource>().volume = volume;
    }
}
