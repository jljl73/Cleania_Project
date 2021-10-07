using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameMenuUI : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (GameManager.Instance.menuManager != null)
            GameManager.Instance.menuManager.SetNewMenuUI(this.gameObject);
        else
            this.gameObject.SetActive(false);
    }

    public void OnClickExit()
    {
        GameManager.Instance.QuitGame();
    }

    public void OnClickCloseUI()
    {
        if (GameManager.Instance.menuManager != null)
            GameManager.Instance.menuManager.PopDownMenuUI();
    }

    public void OnClickSetting()
    {
        if (GameManager.Instance.menuManager != null)
            GameManager.Instance.menuManager.PopUpUserSettingUI();
    }

    public void OnClickResume()
    {
        if (GameManager.Instance.menuManager != null)
            GameManager.Instance.menuManager.PopDownMenuUI();
    }

}
