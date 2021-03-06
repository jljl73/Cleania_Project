using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillCleaningWind : PlayerSkill
{
    [SerializeField]
    PlayerSkillCleaningWindSO skillData;

    [SerializeField]
    GameObject cleaningWindPrefab;

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

    float projectileSpeed = 1;
    public float GetProjectileSpeed() { return projectileSpeed; }

    float projectileDamageScale = 5.4f;
    public float GetDamageScale() { return projectileDamageScale; }

    // "같은 피격체에 대한 최대 피해 적용 횟수"
    int maxHitPerSameObject = 2;
    public int GetMaxHitPerSameObject() { return maxHitPerSameObject; }

    // "회오리 생성 높이"
    float projectilePositionY = 0.5f;
    public float GetprojectilePositionY() { return projectilePositionY; }

    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
    }

    protected new void Start()
    {
        base.Start();
        UpdateSkillData();

        ResizeEffect();
    }

    void ResizeEffect()
    {
        effectController[0].Scale = gatherEnergySize;
        effectController[1].Scale = smashRange * 0.3333f;
    }

    public void UpdateSkillData()
    {
        base.UpdateSkillData(skillData);

        duration = skillData.GetDuration();

        gatherEnergySize = skillData.GetGatherEnergySize();
        smashDamageRate = skillData.GetSmashDamageRate();
        smashRange = skillData.GetSmashRange();
        projectilePositionY = skillData.GetProjectilePositionY();
        count = skillData.GetCount();
        projectileSpeed = skillData.GetSpeed();
        projectileDamageScale = skillData.GetProjectileDamageScale();
        projectileSize = skillData.GetProjectileSize();
        maxHitPerSameObject = skillData.GetMaxHitPerSameObject();

    }

    override public void Activate()
    {
        // 카메라 흔들기
        Camera.main.GetComponent<CinemachineVirtualCameraManager>().CameraShakeBegin();

        // 내려치기
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, smashRange);
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

            // CleaningWind cleaningWind = ObjectPool.SpawnFromPool<CleaningWind>(ObjectPool.enumPoolObject.CleaningWind, transform.position + Vector3.up * projectilePositionY, tempYAngle);
            GameObject cleaningWindObj = Instantiate(cleaningWindPrefab, transform.position + Vector3.up * projectilePositionY, tempYAngle);
            CleaningWind cleaningWind = cleaningWindObj.GetComponent<CleaningWind>();
            cleaningWind.SetUp(maxHitPerSameObject, projectileSpeed, duration, OwnerAbilityStatus, projectileDamageScale);
            cleaningWind.Resize(projectileSize);
        }
    }

    public override void ActivateSound(int index)
    {
        GameManager.Instance.playerSoundPlayer.PlaySound(PlayerSoundPlayer.TYPE.CleaningWind);
    }
}
