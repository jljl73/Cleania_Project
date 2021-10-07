using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    GameObject ingameMenuUIObj;
    GameObject userSettingUIObj;

    List<GameObject> uiList = null;

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
       //  FindMenuUserSetting();
    }

    void Start()
    {
        // UI 생성 후 집어 넣기
        // ClearUIList();
        // UpdateUIList();
    }

    void Update()
    {
        // if (!GameManager.IsIngame) return;

        if (ingameMenuUIObj == null || userSettingUIObj == null)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if ((!userSettingUIObj.activeSelf) && (!ingameMenuUIObj.activeSelf))
            {
                PopUpMenuUI();
                return;
            }

            PopDownUI();
        }
    }

    public void SetNewUserSetting(GameObject userSettingUIObj)
    {
        if (uiList == null)
            uiList = new List<GameObject>();
        else if (uiList.Count == 2)
        {
            uiList.Clear();
            uiList = new List<GameObject>();
        }

        this.userSettingUIObj = userSettingUIObj;

        uiList.Add(userSettingUIObj);
        userSettingUIObj.SetActive(false);
    }

    public void SetNewMenuUI(GameObject menuUIObj)
    {
        if (uiList.Count == 2)
        {
            uiList.Clear();
            uiList = new List<GameObject>();
        }

        this.ingameMenuUIObj = menuUIObj;

        uiList.Add(ingameMenuUIObj);
        ingameMenuUIObj.SetActive(false);
    }

    void ClearUIList()
    {
        if (uiList == null)
            uiList = new List<GameObject>();
        else
        {
            print("haha");
            uiList.Clear();
            uiList = new List<GameObject>();
        }
    }

    //public void FindMenuUserSetting()
    //{
    //    IngameMenuUI ingameMenuUI = FindObjectOfType<IngameMenuUI>();
    //    if (ingameMenuUI != null)
    //        ingameMenuUIObj = ingameMenuUI.gameObject;
    //    else
    //        ingameMenuUIObj = null;

    //    UserSettingUIManager userSettingUIManager = FindObjectOfType<UserSettingUIManager>();
    //    if (userSettingUIManager != null)
    //        userSettingUIObj = userSettingUIManager.gameObject;
    //    else
    //        userSettingUIObj = null;
    //}

    public void PopDownUI()
    {
        if (userSettingUIObj.activeSelf)
            PopDownUserSettingUI();
        else if (ingameMenuUIObj.activeSelf)
            PopDownMenuUI();
    }

    public void PopUpMenuUI()
    {
        ingameMenuUIObj.SetActive(true);
        GameManager.Instance.SetTimeScale(0);
    }

    public void PopDownMenuUI()
    {
        ingameMenuUIObj.SetActive(false);
        GameManager.Instance.SetTimeScale(1);
    }

    public void PopUpUserSettingUI()
    {
        userSettingUIObj.SetActive(true);
    }
    public void PopDownUserSettingUI()
    {
        userSettingUIObj.SetActive(false);
    }

    public void OnClickedGameQuit()
    {
        GameManager.Instance.QuitGame();
    }
}