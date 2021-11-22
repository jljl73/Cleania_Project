using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillDehydration", menuName = "Scriptable Object/PlayerSkill/PlayerSkillDehydration")]
public class PlayerSkillDehydrationSO : PlayerSkillSO
{
    [Header("데미지 비율 (ex. 2.0 = 200% 데미지 적용)")]
    public float DamageRate = 3.0f;
    public float GetDamageRate() { return DamageRate; }

    [Header("데미지 범위")]
    public float DamageRange = 1.0f;
    public float GetDamageRange() { return DamageRange; }

    /*
      빙글빙글 돌며 이동하며, 이동 경로에 있는 모든 적에게 초당 300%만큼의 피해를 줍니다.
     */
}
