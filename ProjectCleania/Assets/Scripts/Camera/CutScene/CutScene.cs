using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
//using UnityEngine

public class CutScene : MonoBehaviour
{
    PlayableDirector director;

    [SerializeField]
    GameObject[] objects;

    void Start()
    {
        director = GetComponent<PlayableDirector>();
        for (int i = 0; i < objects.Length; ++i)
            objects[i].SetActive(false);
        director.stopped += StartScene;
    }

    void StartScene(PlayableDirector aDirector)
    {
        for (int i = 0; i < objects.Length; ++i)
            objects[i].SetActive(true);
    }

}
