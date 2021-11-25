using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    Player player;

    [SerializeField]
    EnemySpawnerManager enemySpawnerManager;
    public bool PlayerMovable;

    //public Vector3 force = Vector3.right;
    static public GameObject clickedObject = null;
    Ray ray;
    RaycastHit raycastHit;

    private void Awake()
    {
        GameManager.Instance.inputManager = this;
    }

    void Start()
    {
        player = GameManager.Instance.player;
        enemySpawnerManager = FindObjectOfType<EnemySpawnerManager>();
    }

    void Update()
    {
        
        
        // UI 클릭시 리턴
        if (EventSystem.current.IsPointerOverGameObject(-1)) return;

        // 죽으면 플레이어 입력 불가
        if (player.abilityStatus.HP == 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                player.PlaySkill(1190);
            }
            return;
        }

        // 마우스 >>>>>
        if (Input.GetMouseButtonDown(0))
        {
            clickedObject = null;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layerMask = 1 << 10 | 1 << 6 | 1 << 15;
            if (Physics.Raycast(ray, out raycastHit, 300.0f, layerMask))
            {
                clickedObject = raycastHit.collider.gameObject;
                Debug.Log(clickedObject.name);
            }
        }

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            
            if (PlayerMovable)
                player.Move(Input.mousePosition);
        }

        if (Input.GetMouseButton(1))
        {
            player.PlaySkill(1102);
        }

        if (Input.GetMouseButtonUp(1))
        {
            player.StopSkill(1102);
        }
        // 마우스 <<<<<


        // 키보드 >>>>>
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.PlaySkill(1103);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.PlaySkill(1104);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            player.PlaySkill(1105);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            player.PlaySkill(1106);
        }
        if (Input.GetKey(KeyCode.C))
        {
            player.PlaySkill(1101);
        }
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            player.PlaySkill(1199);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.PlaySkill(1198);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            player.PlaySkill(1196);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            player.PlaySkill(1197);
        }

        
        // 키보드 <<<<<

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            enemySpawnerManager.SpawnStart();
        }

        //if (Input.GetKeyDown(KeyCode.Alpha9))
        //{
        //    player.playerMove.AddForce(force);
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha8))
        //{
        //    player.playerMove.Pulled(false, Vector3.zero);
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha7))
        //{
        //    player.playerMove.Pulled(true, new Vector3(12.7f, 0f, 8.2f));
        //}
    }

}
