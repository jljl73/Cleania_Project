using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill1 : PlayerSkill
{
    public BuffManager buffManager;
    // public float speed = 10.0f;
    float duration = 0f;
    bool bSkill = false;
    int nDeadEnemy = 0;


    protected new void Start()
    {
        base.Start();
        animator.SetFloat("FairysWings multiplier", speedMultiplier);
    }

    public override void AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSkill1", true);
        //animator.SetInteger("Skill", 1);
        animator.SetTrigger("FairysWings");
    }

    override public void Activate()
    {
        if (!bSkill)
        {
            StartCoroutine(SpeedUp());
        }
    }

    IEnumerator SpeedUp()
    {
        bSkill = true;
        duration = 5.0f;
        nDeadEnemy = 0;

        buffManager.AddBuff(0.4f, Ability.Buff.MoveSpeed_Buff, 5.0f);
        
        yield return new WaitForSeconds(duration);

        for (int i = 0; i < nDeadEnemy; ++i)
        {
            yield return new WaitForSeconds(1.0f);
        }

        bSkill = false;
    }

    public void AddDeadEnemy()
    {
        if(bSkill)
            ++nDeadEnemy;
    }

    public override void Deactivate()
    {
        animator.SetBool("OnSkill1", false);
        animator.SetBool("OnSkill", false);
    }
}
