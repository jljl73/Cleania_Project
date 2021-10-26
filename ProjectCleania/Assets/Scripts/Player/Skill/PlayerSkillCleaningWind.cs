using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillCleaningWind : PlayerSkill
{
    public PlayerSkillCleaningWindSO SkillData;

    public GameObject hurricanePrefabs;

    GameObject newProjectile;

    // "내려치기 데미지 비율 (ex. 2.0 = 200% 데미지 적용)"
    float smashDamageRate = 6f;
    float GetSmashDamageRate() { return smashDamageRate; }

    // "내려치기 범위"
    float smashRange = 2f;
    float GetSmashRange() { return smashRange; }

    // "회오리 유지 시간"
    float projectileDuration = 2f;
    float GetProjectileDuration() { return projectileDuration; }

    // "회오리 갈래 갯수"
    int projectileCount = 3;
    int GetProjectileCount() { return projectileCount; }

    // "초당 회오리 데미지 비율 (ex. 2.0 = 200% 데미지 적용)"
    float projectileDamageRatePerSec = 5.4f;
    float GetProjectileDamageRatePerSec() { return projectileDamageRatePerSec; }

    // "같은 피격체에 대한 최대 피해 적용 횟수"
    int maxHitPerSameObject = 2;
    int GetMaxHitPerSameObject() { return maxHitPerSameObject; }

    // "회오리 생성 높이"
    float projectilePositionY = 0.5f;
    float GetprojectilePositionY() { return projectilePositionY; }

    private void Awake()
    {
        UpdateSkillData();
    }

    protected new void Start()
    {
        base.Start();
        GameManager.Instance.player.OnLevelUp += UpdateSkillData;
        animator.SetFloat("CleaningWind multiplier", SpeedMultiplier);
    }

    public void UpdateSkillData()
    {
        SkillName = SkillData.GetSkillName();
        SkillDetails = SkillData.GetSkillDetails();
        CoolTime = SkillData.GetCoolTime();
        CreatedMP = SkillData.GetCreatedMP();
        ConsumMP = SkillData.GetConsumMP();
        SpeedMultiplier = SkillData.GetSpeedMultiplier();

        SkillSlotDependency = SkillData.GetTriggerKey();

        smashDamageRate = SkillData.GetSmashDamageRate();
        smashRange = SkillData.GetSmashRange();
        projectilePositionY = SkillData.GetProjectilePositionY();
        projectileCount = SkillData.GetProjectileCount();
        projectileDamageRatePerSec = SkillData.GetProjectileDamageRatePerSec();
        maxHitPerSameObject = SkillData.GetMaxHitPerSameObject();

    }

    public override void AnimationActivate()
    {
        //animator.SetInteger("Skill", 3);
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSkill3", true);
        animator.SetTrigger("CleaningWind");
    }

    public override void Deactivate()
    {
        animator.SetBool("OnSkill3", false);
        animator.SetBool("OnSkill", false);
    }

    override public void Activate()
    {
        Quaternion yAngle = transform.rotation;
        
        // 회오리 갯수 짝수
        for (int i = 1; i <= projectileCount; i++)
        {
            Quaternion tempYAngle = yAngle;
            tempYAngle *= Quaternion.Euler(0f, -90.0f + (180.0f / (projectileCount + 1)) * i, 0f);

            newProjectile = Instantiate(hurricanePrefabs, transform.position + Vector3.up * projectilePositionY, tempYAngle);
            Projectile proj = newProjectile.GetComponent<Projectile>();
            proj.PitcherStatus = OwnerAbilityStatus;
            proj.ResetSetting(maxHitPerSameObject, projectileDamageRatePerSec, projectileDuration);
        }

        #region
        // Quaternion left = transform.rotation;
        // Quaternion right = transform.rotation;

        // left *= Quaternion.Euler(0, 30.0f, 0.0f);
        // right *= Quaternion.Euler(0, -30.0f, 0.0f);

        //newProjectile = Instantiate(hurricanePrefabs, transform.position, left);
        //Projectile proj = newProjectile.GetComponent<Projectile>();
        //proj.abilityStatus = abilityStatus;
        //proj.ResetSetting(maxHitPerSameObject, projectileDamageRatePerSec, projectileDuration);

        //newProjectile = Instantiate(hurricanePrefabs, transform.position, transform.rotation);
        //newProjectile.GetComponent<Projectile>().abilityStatus = abilityStatus;

        //newProjectile = Instantiate(hurricanePrefabs, transform.position, right);
        //newProjectile.GetComponent<Projectile>().abilityStatus = abilityStatus;
        #endregion
    }
}
