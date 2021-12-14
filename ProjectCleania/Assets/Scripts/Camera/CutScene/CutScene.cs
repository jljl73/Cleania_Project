using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
//using UnityEngine

public class CutScene : MonoBehaviour
{
    void Start()
    {
        if (GameManager.Instance.CutScenePlayed)
            gameObject.SetActive(false);
        else
            GameManager.Instance.CutScenePlayed = true;
    }

}
