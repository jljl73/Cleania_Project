using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public MenuManager menuManager = null;
<<<<<<< HEAD
    public Canvas MainCanvas = null;

=======
>>>>>>> 80c7bcb91dccc74dd58aaaf3b163a9a4b068d795
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

    public GameObject SinglePlayer;
    public AbilityStatus PlayerAbility;
    public Status PlayerStatus;
    public EquipmentSlot PlayerEquipments;
    public BuffManager PlayerBuffs;


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        menuManager = FindObjectOfType<MenuManager>();
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
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