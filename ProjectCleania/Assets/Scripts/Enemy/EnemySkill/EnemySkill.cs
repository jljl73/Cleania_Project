using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill : Skill
{
    public enum SkillID
    {

    }

    public SkillID ID;

    public Enemy enemy;
    protected EnemyMove enemyMove;
    protected EnemyChase enemyChase;

    protected void Awake()
    {
        enemy = transform.parent.parent.GetComponent<Enemy>();
        enemyMove = enemy.enemyMove;
        enemyChase = enemy.GetComponentInChildren<EnemyChase>();
    }

    protected new void Start()
    {
        base.Start();
    }

    public override void AnimationActivate()
    {
        throw new System.NotImplementedException();
    }

    public override void Deactivate()
    {
        throw new System.NotImplementedException();
    }
}
