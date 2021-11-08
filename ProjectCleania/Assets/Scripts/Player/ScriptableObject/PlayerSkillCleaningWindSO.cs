using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillCleaningWind", menuName = "Scriptable Object/PlayerSkill/PlayerSkillCleaningWind")]
public class PlayerSkillCleaningWindSO : PlayerSKillIDSO
{
    

    [Header("Tip: �������� �Է��� �� �ֽ��ϴ�.")]
    [TextArea]
    public string SkillDetails;
    public string GetSkillDetails()
    {
        string tempString = SkillDetails;

        string coolTime = CoolTime.ToString();
        tempString = tempString.Replace("CoolTime", coolTime);

        string createdMP = CreatedMP.ToString();
        tempString = tempString.Replace("CreatedMP", createdMP);

        string consumMP = ConsumMP.ToString();
        tempString = tempString.Replace("ConsumMP", consumMP);

        string smashDamageRate = (SmashDamageRate * 100).ToString();
        tempString = tempString.Replace("SmashDamageRate", smashDamageRate);

        string smashRange = SmashRange.ToString();
        tempString = tempString.Replace("SmashRange", smashRange);

        string projectileDuration = Duration.ToString();
        tempString = tempString.Replace("ProjectileDuration", projectileDuration);

        string projectileCount = Count.ToString();
        tempString = tempString.Replace("ProjectileCount", projectileCount);

        string projectileDamageRatePerSec = (ProjectileDamageScale * 100).ToString();
        tempString = tempString.Replace("ProjectileDamageRatePerSec", projectileDamageRatePerSec);

        string maxHitPerSameObject = MaxHitPerSameObject.ToString();
        tempString = tempString.Replace("MaxHitPerSameObject", maxHitPerSameObject);

        return tempString;
    }

    [Header("�۵� Ű")]
    public KeyCode TriggerKey;
    public KeyCode GetTriggerKey() { return TriggerKey; }

    // public bool isAttacking;
    [Header("��Ÿ��")]
    public float CoolTime;  // ���� private ó��
    public float GetCoolTime() { return CoolTime; }

    [Header("���� ���� �ڿ�")]
    public float CreatedMP = 0f;
    public float GetCreatedMP() { return CreatedMP; }

    [Header("�Ҹ� ���� �ڿ�")]
    public float ConsumMP = 0f;
    public float GetConsumMP() { return ConsumMP; }

    [Header("��ü �ִϸ��̼� ���")]
    public float SpeedMultiplier = 1.0f;
    public float GetSpeedMultiplier() { return SpeedMultiplier; }

    [Header("����ġ�� ������ ���� (ex. 2.0 = 200% ������ ����)")]
    public float SmashDamageRate = 6f;
    public float GetSmashDamageRate() { return SmashDamageRate; }

    [Header("����ġ�� ����")]
    public float SmashRange = 2f;
    public float GetSmashRange() { return SmashRange; }

    [Header("ȸ���� ���� ����")]
    public float ProjectilePositionY = 0.5f;
    public float GetProjectilePositionY() { return ProjectilePositionY; }

    [Header("ȸ���� ���� �ð�")]
    public float Duration = 2f;
    public float GetDuration() { return Duration; }

    [Header("ȸ���� ���� ����")]
    public int Count = 3;
    public int GetCount() { return Count; }

    [Header("ȸ���� ũ��")]
    public int ProjectileSize = 1;
    public int GetProjectileSize() { return ProjectileSize; }

    [Header("�ʴ� ȸ���� ������ ���� (ex. 2.0 = 200% ������ ����)")]
    public float ProjectileDamageScale = 5.4f;
    public float GetProjectileDamageScale() { return ProjectileDamageScale; }

    [Header("���� �ǰ�ü�� ���� �ִ� ���� ���� Ƚ��")]
    public int MaxHitPerSameObject = 2;
    public int GetMaxHitPerSameObject() { return MaxHitPerSameObject; }

    /*
     ���� ����� ���ϰ� ������ Ÿ�� ���� ���鿡�� 600%�� ���ظ� �ݴϴ�. 
     Ÿ�� ���� �����Ǵ� ȸ������ 2�ʰ� �����Ǿ� 3������ �����ϸ�, 
     ���� ���⿡ �ִ� ���鿡�� �ʴ� 540%�� ���ظ� �ݴϴ�. ���� ������ �ִ� 2ȸ�� ���ظ� �ݴϴ�.
     */
}
