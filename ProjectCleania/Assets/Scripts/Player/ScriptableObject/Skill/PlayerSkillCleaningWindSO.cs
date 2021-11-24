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

    [Header("기모으기 이팩트 크기 반경")]
    [SerializeField]
    float GatherEnergySize = 1f;
    public float GetGatherEnergySize() { return GatherEnergySize; }

    [Header("내려치기 데미지 비율 (ex. 2.0 = 200% 데미지 적용)")]
    [SerializeField]
    float SmashDamageRate = 6f;
    public float GetSmashDamageRate() { return SmashDamageRate; }

    [Header("내려치기 반경")]
    [SerializeField]
    float SmashRange = 2f;
    public float GetSmashRange() { return SmashRange; }

    [Header("회오리 생성 높이")]
    [SerializeField]
    float ProjectilePositionY = 0.5f;
    public float GetProjectilePositionY() { return ProjectilePositionY; }

    [Header("회오리 속도")]
    [SerializeField]
    float speed = 5f;
    public float GetSpeed() { return speed; }

    [Header("회오리 유지 시간")]
    [SerializeField]
    float Duration = 2f;
    public float GetDuration() { return Duration; }

    [Header("회오리 갈래 갯수")]
    [SerializeField]
    int Count = 3;
    public int GetCount() { return Count; }

    [Header("회오리 크기")]
    [SerializeField]
    int ProjectileSize = 1;
    public int GetProjectileSize() { return ProjectileSize; }

    [Header("초당 회오리 데미지 비율 (ex. 2.0 = 200% 데미지 적용)")]
    [SerializeField]
    float ProjectileDamageScale = 5.4f;
    public float GetProjectileDamageScale() { return ProjectileDamageScale; }

    [Header("같은 피격체에 대한 최대 피해 적용 횟수")]
    [SerializeField]
    int MaxHitPerSameObject = 2;
    public int GetMaxHitPerSameObject() { return MaxHitPerSameObject; }

    /*
     땅을 무기로 강하게 내려쳐 타격 범위 적들에게 600%의 피해를 줍니다. 
     타격 이후 생성되는 회오리는 2초간 유지되어 3갈래로 전진하며, 
     진행 방향에 있는 적들에게 초당 540%의 피해를 줍니다. 같은 적에게 최대 2회의 피해를 줍니다.
     */
}
