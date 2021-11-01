using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySkill : Skill
{
    public Enemy enemy;
    protected EnemyMove enemyMove;
    protected EnemyChase enemyChase;

    protected void Awake()
    {
        if (enemy == null)
            throw new System.Exception("EnemySkill doesnt have enemy");
        enemyMove = enemy.enemyMove;
        enemyChase = enemy.GetComponentInChildren<EnemyChase>();
    }

    //public override void AnimationActivate()
    //{
    //    throw new System.NotImplementedException();
    //}

    //public override void Deactivate()
    //{
    //    throw new System.NotImplementedException();
    //}
}
