using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillCleaningWind : PlayerSkill
{
    public PlayerSkillCleaningWindSO SkillData;

    public GameObject hurricanePrefabs;

    GameObject newProjectile;

    float gatherEnergySize = 1f;
    float GetGatherEnergySize() { return gatherEnergySize; }

    float smashDamageRate = 6f;
    public float GetSmashDamageRate() { return smashDamageRate; }

    float smashRange = 2f;
    public float GetSmashRange() { return smashRange; }

    float duration = 2f;
    public float GetDuration() { return duration; }

    int count = 3;
    public int GetCount() { return count; }

    float projectileSize = 1;
    public float GetProjectileSize() { return projectileSize; }

    float projectileDamageScale = 5.4f;
    public float GetDamageScale() { return projectileDamageScale; }

    // "같은 피격체에 대한 최대 피해 적용 횟수"
    int maxHitPerSameObject = 2;
    public int GetMaxHitPerSameObject() { return maxHitPerSameObject; }

    // "회오리 생성 높이"
    float projectilePositionY = 0.5f;
    public float GetprojectilePositionY() { return projectilePositionY; }

    public override int ID { get { return SkillData.ID; } protected set { id = value; } }

    private void Awake()
    {
        UpdateSkillData();
    }

    protected new void Start()
    {
        base.Start();
        GameManager.Instance.player.OnLevelUp += UpdateSkillData;
        animator.SetFloat("CleaningWind multiplier", SpeedMultiplier);

        ResizeEffect();
    }

    void ResizeEffect()
    {
        effectController[0].Scale = gatherEnergySize;
        effectController[1].Scale = smashRange * 0.3333f;
    }

    public void UpdateSkillData()
    {
        ID = SkillData.ID;
        SkillName = SkillData.GetSkillName();
        SkillDetails = SkillData.GetSkillDetails();
        CoolTime = SkillData.GetCoolTime();
        CreatedMP = SkillData.GetCreatedMP();
        ConsumMP = SkillData.GetConsumMP();
        SpeedMultiplier = SkillData.GetSpeedMultiplier();

        SkillSlotDependency = SkillData.GetTriggerKey();

        duration = SkillData.GetDuration();

        gatherEnergySize = SkillData.GetGatherEnergySize();
        smashDamageRate = SkillData.GetSmashDamageRate();
        smashRange = SkillData.GetSmashRange();
        projectilePositionY = SkillData.GetProjectilePositionY();
        count = SkillData.GetCount();
        projectileDamageScale = SkillData.GetProjectileDamageScale();
        projectileSize = SkillData.GetProjectileSize();
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
        // 내려치기
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, smashRange * 3.5f);
        foreach (Collider collider in overlappedColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                // 자폭 데미지!
                AbilityStatus abil = collider.gameObject.GetComponent<AbilityStatus>();
                if (abil != null)
                    abil.AttackedBy(OwnerAbilityStatus, smashDamageRate);
            }
        }

        // 회오리 발사
        Quaternion yAngle = transform.rotation;
        
        // 회오리 갯수 짝수
        for (int i = 1; i <= count; i++)
        {
            Quaternion tempYAngle = yAngle;
            tempYAngle *= Quaternion.Euler(0f, -90.0f + (180.0f / (count + 1)) * i, 0f);

            newProjectile = Instantiate(hurricanePrefabs, transform.position + Vector3.up * projectilePositionY, tempYAngle);
            Projectile proj = newProjectile.GetComponent<Projectile>();
            proj.SetUp(maxHitPerSameObject, duration, OwnerAbilityStatus, projectileDamageScale);
            proj.Resize(projectileSize);
        }
    }
}
