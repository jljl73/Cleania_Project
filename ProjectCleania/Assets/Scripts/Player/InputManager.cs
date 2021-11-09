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
        // UI Ŭ���� ����
        if (EventSystem.current.IsPointerOverGameObject(-1)) return;

        // ������ �÷��̾� �Է� �Ұ�
        if (player.abilityStatus.HP == 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                player.Revive();
            }
            return;
        }

        // ���콺 >>>>>
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            player.Move(Input.mousePosition);
        }
        // ���콺 <<<<<


        // Ű���� >>>>>
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
        // Ű���� <<<<<
    }

}
