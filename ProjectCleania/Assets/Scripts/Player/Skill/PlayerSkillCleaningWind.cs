using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillCleaningWind : PlayerSkill
{
    public PlayerSkillCleaningWindSO SkillData;

    public GameObject hurricanePrefabs;

    GameObject newProjectile;

    // "����ġ�� ������ ���� (ex. 2.0 = 200% ������ ����)"
    float smashDamageRate = 6f;
    float GetSmashDamageRate() { return smashDamageRate; }

    // "����ġ�� ����"
    float smashRange = 2f;
    float GetSmashRange() { return smashRange; }

    // "ȸ���� ���� �ð�"
    float projectileDuration = 2f;
    float GetProjectileDuration() { return projectileDuration; }

    // "ȸ���� ���� ����"
    int projectileCount = 3;
    int GetProjectileCount() { return projectileCount; }

    // "�ʴ� ȸ���� ������ ���� (ex. 2.0 = 200% ������ ����)"
    float projectileDamageRatePerSec = 5.4f;
    float GetProjectileDamageRatePerSec() { return projectileDamageRatePerSec; }

    // "���� �ǰ�ü�� ���� �ִ� ���� ���� Ƚ��"
    int maxHitPerSameObject = 2;
    int GetMaxHitPerSameObject() { return maxHitPerSameObject; }

    // "ȸ���� ���� ����"
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
        base.AnimationActivate();

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
        
        // ȸ���� ���� ¦��
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
