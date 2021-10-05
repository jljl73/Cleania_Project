using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    MenuManager menuManager;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject newGameObject = new GameObject("GameManager");
                _instance = newGameObject.AddComponent<GameManager>();
            }
            return _instance;
        }
    }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        menuManager = FindObjectOfType<MenuManager>();
    }

    public void PopUpMenu()
    {
        menuManager.PopUpMenuUI();
    }

    public void PopUpUserSetting()
    {
        menuManager.PopUpUserSettingUI();
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        OnSceneLoad();
    }

    void OnSceneLoad()
    {
        menuManager.FindMenuUserSetting();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetTimeScale(int value)
    {
        Time.timeScale = value;
    }

}