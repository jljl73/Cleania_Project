using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillVillageReturn", menuName = "Scriptable Object/PlayerSkill/PlayerSkillVillageReturn")]
public class PlayerSkillVillageReturnSO : PlayerSkillSO
{
    [Header("시전 시간")]
    public float TimeCost = 6f;
    public float GetTimeCost() { return TimeCost; }

    [Header("마을 이름")]
    public string VillageName = "MY_UIScene";
    public string GetVillageName() { return VillageName; }

    [Header("귀환 좌표")]
    public Vector3 ReturnPosition;
    public Vector3 GetReturnPosition() { return ReturnPosition; }
}
