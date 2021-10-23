using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighDustySkill1 : EnemySkill
{
    public float DamageScale = 10;

    //Collider col
    AbilityStatus myAbility;
    public GameObject DustBall;
    // public Enemy enemy;

    int skillCount = 0;

    private new void Start()
    {
        base.Start();
        myAbility = GetComponentInParent<AbilityStatus>();
    }

    public override void AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetTrigger("ThrowingDust");
    }

    override public void Activate()
    {
        GameObject ball = Instantiate(DustBall, transform.position, transform.rotation);
        ball.GetComponent<HighDusty_DustBall>().owner = enemy.gameObject;
        ball.GetComponent<HighDusty_DustBall>().DamageScale = DamageScale;
        ball.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up / 2) * 200.0f);

        if (++skillCount == 3)
        {
            skillCount = 0;
            enemy.enemyMove.RunAway();
        }
    }

    public override void Deactivate()
    {
        animator.SetBool("OnSkill", false);
    }
}
