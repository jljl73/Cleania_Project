using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillCleaningWind", menuName = "Scriptable Object/PlayerSkill/PlayerSkillCleaningWind")]
public class PlayerSkillCleaningWindSO : PlayerSkillSO
{
    public override string GetSkillDetails()
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

    [Header("������� ����Ʈ ũ�� �ݰ�")]
    [SerializeField]
    float GatherEnergySize = 1f;
    public float GetGatherEnergySize() { return GatherEnergySize; }

    [Header("����ġ�� ������ ���� (ex. 2.0 = 200% ������ ����)")]
    [SerializeField]
    float SmashDamageRate = 6f;
    public float GetSmashDamageRate() { return SmashDamageRate; }

    [Header("����ġ�� �ݰ�")]
    [SerializeField]
    float SmashRange = 2f;
    public float GetSmashRange() { return SmashRange; }

    [Header("ȸ���� ���� ����")]
    [SerializeField]
    float ProjectilePositionY = 0.5f;
    public float GetProjectilePositionY() { return ProjectilePositionY; }

    [Header("ȸ���� �ӵ�")]
    [SerializeField]
    float speed = 5f;
    public float GetSpeed() { return speed; }

    [Header("ȸ���� ���� �ð�")]
    [SerializeField]
    float Duration = 2f;
    public float GetDuration() { return Duration; }

    [Header("ȸ���� ���� ����")]
    [SerializeField]
    int Count = 3;
    public int GetCount() { return Count; }

    [Header("ȸ���� ũ��")]
    [SerializeField]
    int ProjectileSize = 1;
    public int GetProjectileSize() { return ProjectileSize; }

    [Header("�ʴ� ȸ���� ������ ���� (ex. 2.0 = 200% ������ ����)")]
    [SerializeField]
    float ProjectileDamageScale = 5.4f;
    public float GetProjectileDamageScale() { return ProjectileDamageScale; }

    [Header("���� �ǰ�ü�� ���� �ִ� ���� ���� Ƚ��")]
    [SerializeField]
    int MaxHitPerSameObject = 2;
    public int GetMaxHitPerSameObject() { return MaxHitPerSameObject; }

    /*
     ���� ����� ���ϰ� ������ Ÿ�� ���� ���鿡�� 600%�� ���ظ� �ݴϴ�. 
     Ÿ�� ���� �����Ǵ� ȸ������ 2�ʰ� �����Ǿ� 3������ �����ϸ�, 
     ���� ���⿡ �ִ� ���鿡�� �ʴ� 540%�� ���ظ� �ݴϴ�. ���� ������ �ִ� 2ȸ�� ���ظ� �ݴϴ�.
     */
}
