using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject IngameMenuUI;
    public GameObject UserSettingUI;

    List<GameObject> uiList;

    public bool IsActive
    {
        get
        {
            foreach (GameObject ui in uiList)
            {
                if (ui.activeSelf) return true;
            }
            return false;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        // UI 생성 후 집어 넣기
        uiList = new List<GameObject>();
        uiList.Add(IngameMenuUI);
        uiList.Add(UserSettingUI);

        // 초기 상태 active off
        foreach (GameObject ui in uiList)
        {
            ui.SetActive(false);
        }
    }

    void Update()
    {
        // if (!GameManager.IsIngame) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PopDownUI();
        }
    }

    public void PopDownUI()
    {
        if (UserSettingUI.activeSelf)
            UserSettingUI.SetActive(false);
        else if (IngameMenuUI.activeSelf)
            IngameMenuUI.SetActive(false);
    }

    public void PopUpMenuUI()
    {
        IngameMenuUI.SetActive(true);
    }

    public void PopDownMenuUI()
    {
        IngameMenuUI.SetActive(false);
    }

    public void PopUpUserSettingUI()
    {
        UserSettingUI.SetActive(true);
    }
    public void PopDownUserSettingUI()
    {
        UserSettingUI.SetActive(false);
    }

    public void OnClickedGameQuit()
    {
        GameManager.Instance.QuitGame();
    }
}
