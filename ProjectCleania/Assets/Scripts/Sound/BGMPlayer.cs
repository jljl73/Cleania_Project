using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMPlayer : MonoBehaviour, ISoundPlayer
{

    static BGMPlayer _instance;

    public static BGMPlayer Instance
    {
        get => _instance;
    }

    void Start()
    {
        _instance = this;
        SceneManager.sceneLoaded += OnSceneLoaded;
        ChangeVolume(UserSetting.BGMVolume);
        DontDestroyOnLoad(this);
    }
    
    public AudioClip[] bgms;
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "LoadingScene" || mode == LoadSceneMode.Additive) return;
        GetComponent<AudioSource>().clip = bgms[scene.buildIndex];
        GetComponent<AudioSource>().Play();
    }

    public void ChangeVolume(float volume)
    {
        GetComponent<AudioSource>().volume = volume;
    }
}
