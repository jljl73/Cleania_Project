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

        // 마우스 >>>>>
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            player.Move(Input.mousePosition);
        }
        // 마우스 <<<<<


        // 키보드 >>>>>
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.DoSkill(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.DoSkill(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            player.DoSkill(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            player.DoSkill(3);
        }
        if (Input.GetMouseButton(1))
        {
            player.DoSkill(5);
        }
        // 키보드 <<<<<
    }

}
