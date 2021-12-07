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

    [SerializeField]
    Toggle togglePlayerHP;
    [SerializeField]
    Toggle toggleMonsterHP;
    [SerializeField]
    Toggle togglePlayerName;
    [SerializeField]
    Toggle toggleDamage;
    [SerializeField]
    Toggle toggleCriticalDamage;

    void Awake()
    {
        for (int i = 0; i < LeftContent.childCount; ++i)
        {
            int index = i;
            LeftContent.GetChild(i).GetComponent<Button>().onClick.AddListener(()=>ShowContent(index));
        }

        SliderBGM.onValueChanged.AddListener(ChangeBGMVolume);
        SliderSFX.onValueChanged.AddListener(ChangeSFXVolume);

        SliderBGM.value = UserSetting.BGMVolume;
        SliderSFX.value = UserSetting.SFXVolume;

        LoadSetting();
    }

    void ShowContent(int index)
    {
        for (int i = 0; i < rightContent.childCount; ++i)
            rightContent.GetChild(i).gameObject.SetActive(false);
        rightContent.GetChild(index).gameObject.SetActive(true);
    }

    public void ChangeBGMVolume(float volume)
    {
        UserSetting.BGMVolume = volume;
        BGMPlayer.Instance.ChangeVolume(volume);
        UserSetting.SaveVolume();
    }

    public void ChangeSFXVolume(float volume)
    {
        UserSetting.SFXVolume = volume;
        GameManager.Instance.playerSoundPlayer?.ChangeVolume(volume);
        UserSetting.SaveVolume();
    }

    public void ChangeScreenMode(int value)
    {
        Screen.fullScreen = true;
        Screen.fullScreen = false;
    }

    public void OnChangedPlayerHP(bool value)
    {
        UserSetting.OnPlayerHP = value;
        UserSetting.Save();
    }
    public void OnChangedMonsterHP(bool value)
    {
        UserSetting.OnMonsterHP = value;
        UserSetting.Save();
    }
    public void OnChangedPlayerName(bool value)
    {
        UserSetting.OnPlayerName = value;
        UserSetting.Save();
    }
    public void OnChangedDamage(bool value)
    {
        UserSetting.OnDamage = value;
        UserSetting.Save();
    }
    public void OnChangedCriticalDamage(bool value)
    {
        UserSetting.OnCriticalDamage = value;
        UserSetting.Save();
    }

    void LoadSetting()
    {
        Debug.Log("load Setting");
        togglePlayerHP.isOn = UserSetting.OnPlayerHP;
        toggleMonsterHP.isOn = UserSetting.OnMonsterHP;
        togglePlayerName.isOn = UserSetting.OnPlayerName;
        toggleDamage.isOn = UserSetting.OnDamage;
        toggleCriticalDamage.isOn = UserSetting.OnCriticalDamage;
    }
}
