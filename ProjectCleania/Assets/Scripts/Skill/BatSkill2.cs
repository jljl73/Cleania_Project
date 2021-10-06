using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSkill2 : Skill
{    
    AbilityStatus myAbility;
    Status myStat;

    private void Start()
    {
        myAbility = transform.parent.parent.GetComponent<AbilityStatus>();
        myStat = transform.parent.parent.GetComponent<Status>();
    }

    public override void AnimationActivate()
    {
        animator.SetBool("Attack Soundwave", true);
        //animator.SetInteger("Skill", 1);
        stateMachine.Transition(StateMachine.enumState.Attacking);

        Activate();

        Invoke("AnimationDeactivate", 1.0f);
    }

    override public void Activate()
    {
        GameObject bat1 = Instantiate(transform.parent.parent.gameObject);
        GameObject bat2 = Instantiate(transform.parent.parent.gameObject);

        bat1.transform.localScale *= 0.5f;
        bat2.transform.localScale *= 0.5f;

        Status bat1stat = bat1.GetComponent<Status>();
        Status bat2stat = bat2.GetComponent<Status>();

        bat1stat.Atk = myStat.Atk / 2;
        bat2stat.Atk = myStat.Atk / 2;

        bat1stat.BasicHP = myStat.BasicHP / 2;
        bat2stat.BasicHP = myStat.BasicHP / 2;

        bat1stat.Def = myStat.Def / 2;
        bat2stat.Def = myStat.Def / 2;

        bat1stat.Strength = myStat.Strength / 2;
        bat2stat.Strength = myStat.Strength / 2;

        bat1stat.Vitality = myStat.Vitality / 2;
        bat2stat.Vitality = myStat.Vitality / 2;

        bat1stat.levelUpStrength = myStat.levelUpStrength / 2;
        bat2stat.levelUpStrength = myStat.levelUpStrength / 2;

        bat1stat.levelUpVitality = myStat.levelUpVitality / 2;
        bat2stat.levelUpVitality = myStat.levelUpVitality / 2;

        bat1.GetComponent<AbilityStatus>().FullHP();
        bat2.GetComponent<AbilityStatus>().FullHP();

        Invoke("OffSkill", 1.0f);
    }

    void OffSkill()
    {
    }

    public override void AnimationDeactivate()
    {
        stateMachine.Transition(StateMachine.enumState.Idle);
        animator.SetBool("Attack Soundwave", false);
        OffSkill();
    }
}
