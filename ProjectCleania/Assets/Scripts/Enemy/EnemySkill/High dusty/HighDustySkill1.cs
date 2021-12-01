using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighDustySkill1 : EnemySkill
{
    [SerializeField]
    ThrowDustSO skillData;

    int skillCount = 0;
    
    float damageScale = 10;
    float projectileSize;
    float pondSize;

    AbilityStatus myAbility;
    public GameObject DustBall;

    SphereCollider triggerCollider;
    

    public override bool IsPassiveSkill { get { return skillData.GetIsPassiveSkill(); } }
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    protected new void Awake()
    {
        base.Awake();
        triggerCollider = GetComponent<SphereCollider>();
    }

    private new void Start()
    {
        base.Start();
        myAbility = GetComponentInParent<AbilityStatus>();
        UpdateSkillData();
        triggerCollider.center = triggerPosition;
        triggerCollider.radius = triggerRange;
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
            Invoke("ActivateRunAway", 1);
        }
    }

    public override void Deactivate()
    {
        base.Deactivate();
        animator.SetBool("OnSkill", false);
    }

    void ActivateRunAway()
    {
        enemyMove.RunAway(3);
    }
}
