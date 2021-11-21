using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillDusting", menuName = "Scriptable Object/PlayerSkill/PlayerSkillDusting")]
public class PlayerSkillDustingSO : PlayerSkillSO
{
    [Header("내려치기 데미지 비율 (ex. 2.0 = 200% 데미지 적용)")]
    public float DamageRate = 5.4f;
    public float GetDamageRate() { return DamageRate; }

    /*
      무기로 적을 후려칩니다. 적에게 320%만큼의 피해를 줍니다.
     */
}
