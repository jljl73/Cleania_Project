using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSetting
{
    static public bool OnPlayerHP { get; set; }
    static public bool OnMonsterHP { get; set; }
    static public bool OnPlayerName { get; set; }
    static public bool OnDamage { get; set; }
    static public bool OnCriticalDamage { get; set; }
    static public float BGMVolume { get; set; }
    static public float SFXVolume { get; set; }

    static public void Save()
    {
        int i = 0;
        i += UserSetting.OnPlayerHP ? 1 : 0;
        i += UserSetting.OnMonsterHP ? 1 << 1 : 0;
        i += UserSetting.OnPlayerName ? 1 << 2 : 0;
        i += UserSetting.OnDamage ? 1 << 3 : 0;
        i += UserSetting.OnCriticalDamage ? 1 << 4 : 0;
        PlayerPrefs.SetInt("Setting", i);
    }

    static public void Load()
    {
        int i = int.MaxValue;
        if (PlayerPrefs.HasKey("Setting"))
            i = PlayerPrefs.GetInt("Setting");

        OnPlayerHP = ((i & 1) > 0) ? true : false;
        OnMonsterHP = ((i & 1 << 1) > 0) ? true : false;
        OnPlayerName = ((i & 1 << 2) > 0) ? true : false;
        OnDamage = ((i & 1 << 3) > 0) ? true : false;
        OnCriticalDamage = ((i & 1 << 4) > 0) ? true : false;
    }

    static public void SaveVolume()
    {
        PlayerPrefs.SetFloat("BGMVolume", BGMVolume);
        PlayerPrefs.SetFloat("SFXVolume", SFXVolume);
    }

    static public void LoadVolume()
    {
        BGMVolume = 1.0f;
        SFXVolume = 1.0f;

        if (PlayerPrefs.HasKey("BGMVolume"))
            BGMVolume = PlayerPrefs.GetFloat("BGMVolume");
        if (PlayerPrefs.HasKey("SFXVolume"))
            SFXVolume = PlayerPrefs.GetFloat("SFXVolume");
    }
}
