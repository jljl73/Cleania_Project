using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillDehydration", menuName = "Scriptable Object/PlayerSkill/PlayerSkillDehydration")]
public class PlayerSkillDehydrationSO : PlayerSkillSO
{
    [Header("������ ���� (ex. 2.0 = 200% ������ ����)")]
    public float DamageRate = 3.0f;
    public float GetDamageRate() { return DamageRate; }

    [Header("������ ����")]
    public float DamageRange = 1.0f;
    public float GetDamageRange() { return DamageRange; }

    /*
      ���ۺ��� ���� �̵��ϸ�, �̵� ��ο� �ִ� ��� ������ �ʴ� 300%��ŭ�� ���ظ� �ݴϴ�.
     */
}
