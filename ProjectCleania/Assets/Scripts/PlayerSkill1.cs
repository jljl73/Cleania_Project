using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill1 : Skill
{
    public BuffManager buffManager;
    public Animator animator;
    public float speed = 10.0f;
    float duration = 0f;
    bool bSkill = false;
    int nDeadEnemy = 0;

    public override void AnimationActivate()
    {
        animator.SetInteger("Skill", 1);
    }

    override public void Activate()
    {
        animator.SetInteger("Skill", 0);
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

        buffManager.AddBuff(0.4f, AbilityOption.Buff.MoveSpeed_Buff, 5.0f);
        
        yield return new WaitForSeconds(duration);

        for (int i = 0; i < nDeadEnemy; ++i)
        {
            yield return new WaitForSeconds(1.0f);
        }

        //status.GetComponent<PlayerStatus>().SetSpeed(speed);
        bSkill = false;
    }

    public void addDeadEnemy()
    {
        if(bSkill)
            ++nDeadEnemy;
    }

}
