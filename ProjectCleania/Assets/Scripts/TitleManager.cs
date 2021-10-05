using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public void OnClickedStart()
    {
        GameManager.Instance.ChangeScene("CharacterSelect");
    }

    public void OnClickedOption()
    {
        GameManager.Instance.PopUpUserSetting();
    }

    public void OnClickedExit()
    {
        GameManager.Instance.QuitGame();
    }
    
}
