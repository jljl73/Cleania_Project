using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ComeBackLobby : MonoBehaviour
{
    private static ComeBackLobby singleton;

    static public void Init()
    {
        if (singleton == null)
        {
            GameObject lobbyBack = new GameObject("_lobbyBack_");
            singleton = lobbyBack.AddComponent<ComeBackLobby>();
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("SimpleLobby");
    }
}
