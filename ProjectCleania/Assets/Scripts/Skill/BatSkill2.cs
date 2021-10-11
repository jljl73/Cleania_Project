using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSkill2 : Skill
{
    public AbilityStatus myAbility;

    private void Start()
    {
        myAbility = transform.parent.parent.GetComponent<Enemy>().abilityStatus;
    }

    public override void AnimationActivate()
    {
        animator.SetTrigger("Attack Soundwave");
    }

    override public void Activate()
    {
        if (Random.Range(0, 2) == 0) return;

        GameObject bat1 = Instantiate(transform.parent.parent.gameObject);
        GameObject bat2 = Instantiate(transform.parent.parent.gameObject);

        bat1.transform.Translate(bat1.transform.right * bat1.transform.localScale.x);
        bat2.transform.Translate(-bat1.transform.right * bat2.transform.localScale.x);

        bat1.transform.localScale *= 0.5f;
        bat2.transform.localScale *= 0.5f;

        //Status bat1stat = bat1.GetComponent<Enemy>().status;
        //Status bat2stat = bat2.GetComponent<Enemy>().status;

        //bat1stat.Atk = myStat.Atk / 2;
        //bat2stat.Atk = myStat.Atk / 2;

        //bat1stat.BasicHP = myStat.BasicHP / 2;
        //bat2stat.BasicHP = myStat.BasicHP / 2;

        //bat1stat.Def = myStat.Def / 2;
        //bat2stat.Def = myStat.Def / 2;

        //bat1stat.Strength = myStat.Strength / 2;
        //bat2stat.Strength = myStat.Strength / 2;

        //bat1stat.Vitality = myStat.Vitality / 2;
        //bat2stat.Vitality = myStat.Vitality / 2;

        //bat1stat.levelUpStrength = myStat.levelUpStrength / 2;
        //bat2stat.levelUpStrength = myStat.levelUpStrength / 2;

        //bat1stat.levelUpVitality = myStat.levelUpVitality / 2;
        //bat2stat.levelUpVitality = myStat.levelUpVitality / 2;

        bat1.GetComponent<Enemy>().abilityStatus.FullHP();
        bat2.GetComponent<Enemy>().abilityStatus.FullHP();
    }

    public override void Deactivate()
    {
    }
}
