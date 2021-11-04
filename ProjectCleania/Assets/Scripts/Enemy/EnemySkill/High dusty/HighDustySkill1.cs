using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighDustySkill1 : EnemySkill
{
    float DamageScale = 10;

    //Collider col
    AbilityStatus myAbility;
    public GameObject DustBall;

    [SerializeField]
    EnemySkillSO skillData;
    // public Enemy enemy;

    int skillCount = 0;

    public override bool IsPassiveSkill { get { return skillData.IsPassiveSkill; } }
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Start()
    {
        base.Start();
        myAbility = GetComponentInParent<AbilityStatus>();
        UpdateSkillData();
    }

    public void UpdateSkillData()
    {
        base.UpdateSkillData(skillData);

        DamageScale = skillData.GetDamageRate();
    }

    public override void AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetTrigger("ThrowingDust");
    }

    override public void Activate()
    {
        GameObject ball = Instantiate(DustBall, transform.position, transform.rotation);
        ball.GetComponent<HighDusty_DustBall>().Owner = enemy.gameObject;
        ball.GetComponent<HighDusty_DustBall>().DamageScale = DamageScale;
        ball.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up / 2) * 200.0f);

        if (++skillCount == 3)
        {
            skillCount = 0;
            base.AnimationActivate();
        }
    }

    public override void Deactivate()
    {
        animator.SetBool("OnSkill", false);
    }
}
