using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject IngameMenuUI;
    public GameObject UserSettingUI;

    List<GameObject> uiList;
    int currentIndex = -1;

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
        currentIndex = -1;
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
        if (currentIndex == -1) return;
        uiList[currentIndex].SetActive(false);
        currentIndex--;
    }

    public void PopUpUI()
    {
        if (currentIndex == (uiList.Count - 1)) return;
        currentIndex++;
        uiList[currentIndex].SetActive(true);
    }

    public void PopUpUserSettingUI()
    {
        UserSettingUI.SetActive(true);
    }
    public void PopDownUserSettingUI()
    {
        UserSettingUI.SetActive(false);

        if (currentIndex == (uiList.Count - 1))
        {
            currentIndex--;
        }
    }
}
