using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
//using UnityEngine

public class CutScene : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            GetComponent<PlayableDirector>().time = GetComponent<PlayableDirector>().duration;
    }
}
