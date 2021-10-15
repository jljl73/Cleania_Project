using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameMenuUI : MonoBehaviour
{
    public GameObject backToSelectScenePanel;
    public GameObject exitPanel;

    GameObject clickedPanel;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (GameManager.Instance.menuManager != null)
            GameManager.Instance.menuManager.SetNewMenuUI(this.gameObject);
        else
            this.gameObject.SetActive(false);
    }

    public void OnOffThisPanel()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void OnClickedExit()
    {
        exitPanel.SetActive(true);
        clickedPanel = exitPanel;
    }

    public void OnClickedCloseUI()
    {
        if (GameManager.Instance.menuManager != null)
            GameManager.Instance.menuManager.PopDownMenuUI();
    }

    public void OnClickedSetting()
    {
        if (GameManager.Instance.menuManager != null)
            GameManager.Instance.menuManager.PopUpUserSettingUI();
    }

    public void OnClickedBackToSelectScene()
    {
        backToSelectScenePanel.SetActive(true);
        clickedPanel = backToSelectScenePanel;
    }

    public void OnOKBackToSelectScene()
    {
        GameManager.Instance.ChangeScene("CharacterSelect");
    }

    public void OnOKExitGame()
    {
        GameManager.Instance.QuitGame();
    }

    public void OnClosePanel()
    {
        if(clickedPanel != null)
            clickedPanel.SetActive(false);
    }

}
