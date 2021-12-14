using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public MenuManager menuManager = null;
    public Canvas MainCanvas = null;

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

    public static bool Exists()
    {
        return _instance != null ? true : false;
    }

    public GameObject SinglePlayer;
    public AbilityStatus PlayerAbility;
    public Status PlayerStatus;
    public Equipable PlayerEquipments;
    public Buffable PlayerBuffs;
    public PlayerController player;
    public ChatManager chatManager;
    public UIManager uiManager;
    public DialogManager dialogManager;
    public NPCManager npcManager;
    public SoundPlayer soundPlayer;
    public PlayerSoundPlayer playerSoundPlayer;
    public string nextSceneName;

    public InputManager inputManager;
    public InputField IF_Chat;
    public bool CutScenePlayed;

    public bool isChatting
    {
        get
        {
            if (IF_Chat == null) return false;
            return IF_Chat.isFocused;
        }
    }

    public UI_BalanceStatus_v1 cheatWindow;

    SavedData savedData = new SavedData();
    public SavedData SavedData
    { get => savedData; }


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        menuManager = FindObjectOfType<MenuManager>();
    }

    private void Start()
    {
        SavedData.Start();
    }

    private void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }

    private void OnApplicationQuit()
    {
        SavedData.OnApplicationQuit();        
    }

    public void ChangeScene(string sceneName)
    {
        nextSceneName = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    void Reset()
    {
        MainCanvas = null;
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