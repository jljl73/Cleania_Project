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

    void SaveSetting()
    {
        int i = 0;
        i += UserSetting.OnPlayerHP ? 1 : 0;
        i += UserSetting.OnMonsterHP ? 1 << 1 : 0;
        i += UserSetting.OnPlayerName ? 1 << 2: 0;
        i += UserSetting.OnDamage ? 1 << 3 : 0;
        i += UserSetting.OnCriticalDamage ? 1 << 4 : 0;
        PlayerPrefs.SetInt("Setting", i);
    }

    void LoadSetting()
    {
        int i = int.MaxValue;
        if(PlayerPrefs.HasKey("Setting"))
            i = PlayerPrefs.GetInt("Setting");

        togglePlayerHP.isOn = UserSetting.OnPlayerHP = ((i & 1) > 0) ? true : false;
        toggleMonsterHP.isOn = UserSetting.OnMonsterHP = ((i & 1 << 1) > 0) ? true : false;
        togglePlayerName.isOn = UserSetting.OnPlayerName = ((i & 1 << 2) > 0) ? true : false;
        toggleDamage.isOn = UserSetting.OnDamage = ((i & 1 << 3) > 0) ? true : false;
        toggleCriticalDamage.isOn = UserSetting.OnCriticalDamage = ((i & 1 << 4) > 0) ? true : false;
    }

    public void OnChangedPlayerHP(bool value)
    {
        UserSetting.OnPlayerHP = value;
        SaveSetting();
    }
    public void OnChangedMonsterHP(bool value)
    {
        UserSetting.OnMonsterHP = value;
        SaveSetting();
    }
    public void OnChangedPlayerName(bool value)
    {
        UserSetting.OnPlayerName = value;
        SaveSetting();
    }
    public void OnChangedDamage(bool value)
    {
        UserSetting.OnDamage = value;
        SaveSetting();
    }
    public void OnChangedCriticalDamage(bool value)
    {
        UserSetting.OnCriticalDamage = value;
        SaveSetting();
    }
}
