using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    GameObject IngameMenuUIObj;
    GameObject UserSettingUIObj;

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

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        FindMenuUserSetting();
    }

    void Start()
    {
        // UI 생성 후 집어 넣기
        uiList = new List<GameObject>();
        uiList.Add(IngameMenuUIObj);
        uiList.Add(UserSettingUIObj);

        // 초기 상태 active off
        foreach (GameObject ui in uiList)
        {
            ui.SetActive(false);
        }
    }

    void Update()
    {
        // if (!GameManager.IsIngame) return;

        if (IngameMenuUIObj == null || UserSettingUIObj == null)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if ((!UserSettingUIObj.activeSelf) && (!IngameMenuUIObj.activeSelf))
            {
                PopUpMenuUI();
                return;
            }

            PopDownUI();
        }
    }

    public void FindMenuUserSetting()
    {
        IngameMenuUI ingameMenuUI = FindObjectOfType<IngameMenuUI>();
        if (ingameMenuUI != null)
            IngameMenuUIObj = ingameMenuUI.gameObject;
        else
            IngameMenuUIObj = null;

        UserSettingUIManager userSettingUIManager = FindObjectOfType<UserSettingUIManager>();
        if (userSettingUIManager != null)
            UserSettingUIObj = userSettingUIManager.gameObject;
        else
            UserSettingUIObj = null;
    }

    public void PopDownUI()
    {
        if (UserSettingUIObj.activeSelf)
            PopDownUserSettingUI();
        else if (IngameMenuUIObj.activeSelf)
            PopDownMenuUI();
    }

    public void PopUpMenuUI()
    {
        IngameMenuUIObj.SetActive(true);
        GameManager.Instance.SetTimeScale(0);
    }

    public void PopDownMenuUI()
    {
        IngameMenuUIObj.SetActive(false);
        GameManager.Instance.SetTimeScale(1);
    }

    public void PopUpUserSettingUI()
    {
        UserSettingUIObj.SetActive(true);
    }
    public void PopDownUserSettingUI()
    {
        UserSettingUIObj.SetActive(false);
    }

    public void OnClickedGameQuit()
    {
        GameManager.Instance.QuitGame();
    }
}