using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    PlayerController player;

    //[SerializeField]
    //EnemySpawnerManager enemySpawnerManager;
    public bool PlayerMovable = true;

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
        // enemySpawnerManager = FindObjectOfType<EnemySpawnerManager>();
    }

    void Update()
    {
        // UI 클릭시 리턴
        if (EventSystem.current.IsPointerOverGameObject(-1)) return;

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
                player.OrderMovementTo(Input.mousePosition);
        }

        if (Input.GetMouseButton(1))
        {
            player.OrderSkillID(1102);
        }

        if (Input.GetMouseButtonUp(1))
        {
            player.OrderSkillStop(1102);
        }


        // 마우스 <<<<<


        // 키보드 >>>>>
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //player.PlaySkill(1103);
            player.OrderSkillID(1103);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // player.PlaySkill(1104);
            player.OrderSkillID(1104);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // player.PlaySkill(1105);
            player.OrderSkillID(1105);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // player.PlaySkill(1106);
            player.OrderSkillID(1106);
        }
        if (Input.GetKey(KeyCode.C))
        {
            // player.PlaySkill(1101);
            player.OrderSkillID(1101);
        }
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            // player.PlaySkill(1199);
            player.OrderSkillID(1199);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // player.PlaySkill(1198);
            player.OrderSkillID(1198);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            // player.PlaySkill(1196);
            player.OrderSkillID(1196);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            // player.PlaySkill(1197);
            player.OrderSkillID(1197);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            player.OrderSkillID(1194);
        }


        // 키보드 <<<<<

        //if (Input.GetKeyDown(KeyCode.Alpha0))
        //{
        //    enemySpawnerManager.SpawnStart();
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha8))
        //{
        //    player.Pulled(false, Vector3.zero);
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha7))
        //{
        //    player.Pulled(true, new Vector3(12.7f, 0f, 8.2f));
        //}

        if (Input.GetKeyDown(KeyCode.S))
        {
            player.gameObject.GetComponent<StatusAilment>()?.RestrictBehavior(StatusAilment.BehaviorRestrictionType.Silence, 10f);
        }
    }

}
