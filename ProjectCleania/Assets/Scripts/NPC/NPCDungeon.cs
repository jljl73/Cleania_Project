using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDungeon : MonoBehaviour
{
    public Text TextLevel;
    public Text TextTimer;
    public Text TextExp;
    public Text TextClean;
    public Text TextItem;

    int level;
    int timer;
    int exp;
    int clean;

    public void Enter()
    {
        Debug.Log("씬이름 입력하세요");
        //GameManager.Instance.ChangeScene("")
    }

    public void LevelUp(bool isUp)
    {
        if (isUp && level < 10)
            ++level;
        else if (!isUp && level > 1)
            --level;

        TextLevel.text = level.ToString() + "단계";
    }
    
}

