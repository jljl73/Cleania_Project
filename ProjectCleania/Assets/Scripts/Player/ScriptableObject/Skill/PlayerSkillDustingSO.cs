using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillDusting", menuName = "Scriptable Object/PlayerSkill/PlayerSkillDusting")]
public class PlayerSkillDustingSO : PlayerSkillSO
{
    [Header("����ġ�� ������ ���� (ex. 2.0 = 200% ������ ����)")]
    public float DamageRate = 5.4f;
    public float GetDamageRate() { return DamageRate; }

    /*
      ����� ���� �ķ�Ĩ�ϴ�. ������ 320%��ŭ�� ���ظ� �ݴϴ�.
     */
}
