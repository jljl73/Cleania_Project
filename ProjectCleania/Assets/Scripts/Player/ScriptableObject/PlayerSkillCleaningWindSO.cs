using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillCleaningWind", menuName = "Scriptable Object/PlayerSkill/PlayerSkillCleaningWind")]
public class PlayerSkillCleaningWindSO : ScriptableObject
{
    public string SkillName;
    public string GetSkillName() { return SkillName; }

    [Header("Tip: 변수명을 입력할 수 있습니다.")]
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

        string projectileDuration = ProjectileDuration.ToString();
        tempString = tempString.Replace("ProjectileDuration", projectileDuration);

        string projectileCount = ProjectileCount.ToString();
        tempString = tempString.Replace("ProjectileCount", projectileCount);

        string projectileDamageRatePerSec = (ProjectileDamageRatePerSec * 100).ToString();
        tempString = tempString.Replace("ProjectileDamageRatePerSec", projectileDamageRatePerSec);

        string maxHitPerSameObject = MaxHitPerSameObject.ToString();
        tempString = tempString.Replace("MaxHitPerSameObject", maxHitPerSameObject);

        return tempString;
    }

    [Header("작동 키")]
    public string TriggerKey;
    public string GetTriggerKey() { return TriggerKey; }

    // public bool isAttacking;
    [Header("쿨타임")]
    public float CoolTime;  // 추후 private 처리
    public float GetCoolTime() { return CoolTime; }

    [Header("생성 고유 자원")]
    public float CreatedMP = 0f;
    public float GetCreatedMP() { return CreatedMP; }

    [Header("소모 고유 자원")]
    public float ConsumMP = 0f;
    public float GetConsumMP() { return ConsumMP; }

    [Header("전체 애니메이션 배속")]
    public float SpeedMultiplier = 1.0f;
    public float GetSpeedMultiplier() { return SpeedMultiplier; }

    [Header("내려치기 데미지 비율 (ex. 2.0 = 200% 데미지 적용)")]
    public float SmashDamageRate = 6f;
    public float GetSmashDamageRate() { return SmashDamageRate; }

    [Header("내려치기 범위")]
    public float SmashRange = 2f;
    public float GetSmashRange() { return SmashRange; }

    [Header("회오리 생성 높이")]
    public float ProjectilePositionY = 0.5f;
    public float GetProjectilePositionY() { return ProjectilePositionY; }

    [Header("회오리 유지 시간")]
    public float ProjectileDuration = 2f;
    public float GetProjectileDuration() { return ProjectileDuration; }

    [Header("회오리 갈래 갯수")]
    public int ProjectileCount = 3;
    public int GetProjectileCount() { return ProjectileCount; }

    [Header("초당 회오리 데미지 비율 (ex. 2.0 = 200% 데미지 적용)")]
    public float ProjectileDamageRatePerSec = 5.4f;
    public float GetProjectileDamageRatePerSec() { return ProjectileDamageRatePerSec; }

    [Header("같은 피격체에 대한 최대 피해 적용 횟수")]
    public int MaxHitPerSameObject = 2;
    public int GetMaxHitPerSameObject() { return MaxHitPerSameObject; }

    /*
     땅을 무기로 강하게 내려쳐 타격 범위 적들에게 600%의 피해를 줍니다. 
     타격 이후 생성되는 회오리는 2초간 유지되어 3갈래로 전진하며, 
     진행 방향에 있는 적들에게 초당 540%의 피해를 줍니다. 같은 적에게 최대 2회의 피해를 줍니다.
     */
}
