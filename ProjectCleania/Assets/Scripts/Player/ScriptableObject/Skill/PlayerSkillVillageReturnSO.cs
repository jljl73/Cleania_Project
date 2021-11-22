using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillVillageReturn", menuName = "Scriptable Object/PlayerSkill/PlayerSkillVillageReturn")]
public class PlayerSkillVillageReturnSO : PlayerSkillSO
{
    [Header("���� �ð�")]
    public float TimeCost = 6f;
    public float GetTimeCost() { return TimeCost; }

    [Header("���� �̸�")]
    public string VillageName = "MY_UIScene";
    public string GetVillageName() { return VillageName; }

    [Header("��ȯ ��ǥ")]
    public Vector3 ReturnPosition;
    public Vector3 GetReturnPosition() { return ReturnPosition; }
}
