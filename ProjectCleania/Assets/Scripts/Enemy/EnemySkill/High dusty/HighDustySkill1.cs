using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighDustySkill1 : EnemySkill
{
    float damageScale = 10;
    float projectileSize;
    float pondSize;


    //Collider col
    AbilityStatus myAbility;
    public GameObject DustBall;

    [SerializeField]
    ThrowDustSO skillData;
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

        damageScale = skillData.GetDamageRate();
        projectileSize = skillData.GetProjectileSize();
        pondSize = skillData.GetPondSize();
    }

    public override bool AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetTrigger("ThrowingDust");

        return true;
    }

    override public void Activate()
    {
        GameObject ball = Instantiate(DustBall, transform.position, transform.rotation);
        ball.GetComponent<HighDusty_DustBall>().SetUp(projectileSize, pondSize, OwnerAbilityStatus, damageScale);
        ball.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up / 2) * 200.0f);

        if (++skillCount == 3)
        {
            skillCount = 0;
            enemyMove.RunAway();
            //base.AnimationActivate();
        }
    }

    public override void Deactivate()
    {
        animator.SetBool("OnSkill", false);
    }
}
