using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserSettingPanel : MonoBehaviour
{
    [SerializeField]
    Transform LeftContent;
    [SerializeField]
    Transform rightContent;

    [Header("»ç¿îµå")]
    [SerializeField]
    Slider SliderBGM;
    [SerializeField]
    Slider SliderSFX;

    void Start()
    {
        for (int i = 0; i < LeftContent.childCount; ++i)
        {
            int index = i;
            LeftContent.GetChild(i).GetComponent<Button>().onClick.AddListener(()=>ShowContent(index));
        }

        SliderBGM.onValueChanged.AddListener(ChangeBGMVolume);
        SliderSFX.onValueChanged.AddListener(ChangeSFXVolume);

        SliderBGM.value = Camera.main.GetComponent<AudioSource>().volume;
        SliderSFX.value = GameManager.Instance.playerSoundPlayer.GetComponent<AudioSource>().volume;
    }

    void ShowContent(int index)
    {
        for (int i = 0; i < rightContent.childCount; ++i)
            rightContent.GetChild(i).gameObject.SetActive(false);
        rightContent.GetChild(index).gameObject.SetActive(true);
    }

    public void ChangeBGMVolume(float volume)
    {
        volume *= 0.01f;
        Camera.main.GetComponent<AudioSource>().volume = volume;
    }

    public void ChangeSFXVolume(float volume)
    {
        volume *= 0.01f;
        GameManager.Instance.playerSoundPlayer.ChangeVolume(volume);
    }

    public void ChangeScreenMode(int value)
    {
        Screen.fullScreen = true;
        Screen.fullScreen = false;
    }
}
