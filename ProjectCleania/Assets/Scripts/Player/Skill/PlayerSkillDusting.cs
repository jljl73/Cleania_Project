using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillDusting : PlayerSkill
{
    [SerializeField]
    PlayerSkillDustingSO skillData;

    Collider attackArea;

    // "내려치기 데미지 비율 (ex. 2.0 = 200% 데미지 적용)"
    float damageRate = 5.4f;
    public float GetDamageRate() { return damageRate; }

    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    new void Awake()
    {
        base.Awake();

    }

    new void Start()
    {
        base.Start();
        UpdateSkillData();

        //GameManager.Instance.player.OnLevelUp.AddListener(UpdateSkillData);
        attackArea = GetComponent<Collider>();
    }

    public void UpdateSkillData()
    {
        base.UpdateSkillData(skillData);

        damageRate = skillData.GetDamageRate();
    }

    // Update is called once per frame


    public override void Activate()
    {
        if (attackArea != null)
            attackArea.enabled = true;
    }

    public override void Deactivate()
    {
        //animator.SetBool("OnSkillC", false);
        //animator.SetBool("OnSkill", false);
        if (attackArea != null)
            attackArea.enabled = false;
    }

    public override void ActivateSound(int index)
    {
        GameManager.Instance.playerSoundPlayer.PlaySound(PlayerSoundPlayer.TYPE.Dusting);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            AbilityStatus enemyAbil = other.GetComponent<AbilityStatus>();

            if (enemyAbil.HP != 0)
            {
                enemyAbil.AttackedBy(OwnerAbilityStatus, damageRate);
                OwnerAbilityStatus.ConsumeMP(-CreatedMP);
            }
        }
    }
}
