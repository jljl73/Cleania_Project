using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillDehydration : PlayerSkill
{
    [SerializeField]
    PlayerSkillDehydrationSO skillData;

    public float skillScale = 1.0f;
    float timePassed = 0;

    Collider attackArea;

    // "내려치기 데미지 비율 (ex. 2.0 = 200% 데미지 적용)"
    float damageRate = 3.0f;
    public float GetDamageRate() { return damageRate; }

    public float damageRange = 1.0f;
    public float GetDamageRange() { return damageRange; }

    public override int ID { get { return skillData.ID; } protected set { id = value; } }
    private new void Awake()
    {
        base.Awake();

        attackArea = GetComponent<Collider>();

        UpdateSkillData();
    }

    new void Start()
    {
        //GameManager.Instance.player.OnLevelUp.AddListener(UpdateSkillData);

        base.Start();

        effectController[0].Scale = damageRange * 0.5f;
        effectController[0].Scale = damageRange * 5 / 6f;
    }

    public void UpdateSkillData()
    {
        base.UpdateSkillData(skillData);

        damageRate = skillData.GetDamageRate();
        damageRange = skillData.GetDamageRange();
    }

    public override void Activate()
    {
        if (attackArea != null)
            attackArea.enabled = true;
    }

    public override void Deactivate()
    {
        if (attackArea != null)
            attackArea.enabled = false;
    }

    //public override void ActivateSound(int index)
    //{
    //    GameManager.Instance.playerSoundPlayer.PlaySound(PlayerSoundPlayer.TYPE.Dehydration,0 , true);
    //}

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            timePassed += Time.deltaTime;

            if (timePassed < 1f)
                return;
            else
                timePassed = 0f;

            other.GetComponent<Enemy>().abilityStatus.AttackedBy(OwnerAbilityStatus, skillScale);
        }
    }
}
