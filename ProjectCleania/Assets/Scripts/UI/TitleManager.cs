using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{

    void Awake()
    {
        UserSetting.Load();
        UserSetting.LoadVolume();
    }

    public void OnClickedStart()
    {
        GameManager.Instance.ChangeScene("CharacterSelect");
    }
    
    public void OnClickedExit()
    {
        GameManager.Instance.QuitGame();
    }
    
}
