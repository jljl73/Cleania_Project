using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillDusting : PlayerSkill
{
    [SerializeField]
    PlayerSkillDustingSO skillData;

    Collider attackArea;

    // "����ġ�� ������ ���� (ex. 2.0 = 200% ������ ����)"
    float damageRate = 5.4f;
    public float GetDamageRate() { return damageRate; }

    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    new void Awake()
    {
        base.Awake();

        UpdateSkillData();
    }

    new void Start()
    {
        base.Start();

        GameManager.Instance.player.OnLevelUp += UpdateSkillData;
        attackArea = GetComponent<Collider>();
        animator.SetFloat("Dusting multiplier", SpeedMultiplier);
    }

    public void UpdateSkillData()
    {
        base.UpdateSkillData(skillData);

        damageRate = skillData.GetDamageRate();
    }

    // Update is called once per frame

    public override bool AnimationActivate()
    {
        base.AnimationActivate();

        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSkillC", true);
        animator.SetTrigger("Dusting");
        // animator.SetInteger("Skill", 5);

        return true;
    }

    public override void Activate()
    {
        if (attackArea != null)
            attackArea.enabled = true;
    }

    public override void Deactivate()
    {
        animator.SetBool("OnSkillC", false);
        animator.SetBool("OnSkill", false);
        attackArea.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            AbilityStatus enemyAbil = other.GetComponent<Enemy>().abilityStatus;

            if (enemyAbil.HP != 0)
            {
                enemyAbil.AttackedBy(OwnerAbilityStatus, damageRate);
                OwnerAbilityStatus.ConsumeMP(-CreatedMP);
            }
        }
    }
}
