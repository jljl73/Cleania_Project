using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    Player player;

    void Start()
    {
        player = GameManager.Instance.player;
    }

    void Update()
    {
        // UI 클릭시 리턴
        if (EventSystem.current.IsPointerOverGameObject(-1)) return;

        // 죽으면 플레이어 입력 불가
        if (player.abilityStatus.HP == 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                player.Revive();
            }
            return;
        }

        // 마우스 >>>>>
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            player.Move(Input.mousePosition);
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
        if (Input.GetMouseButton(1))
        {
            player.PlaySkill(1102);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            player.PlaySkill(1199);
        }

        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    GameObject obj = ObjectPool.GetObject(ObjectPool.enumPoolObject.Pollution);
        //    obj.transform.position = player.gameObject.transform.position + player.gameObject.transform.up;
        //    obj.transform.rotation = player.gameObject.transform.rotation;

        //    Pollution pollution = obj.GetComponent<Pollution>();
        //    pollution.SetUp(3, player.abilityStatus, 1);
        //    pollution.Resize(2);
        //}
        // 키보드 <<<<<
    }

}
