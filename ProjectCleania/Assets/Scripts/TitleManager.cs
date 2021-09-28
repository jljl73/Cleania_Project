using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public void OnClickedStart()
    {
        GameManager.Instance.ChangeScene("ItemScene");
    }

    public void OnClickedOption()
    {

    }

    public void OnClickedExit()
    {
        Application.Quit();
    }
    
}
