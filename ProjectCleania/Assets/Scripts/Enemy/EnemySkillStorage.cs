using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class EnemySkillStorage : MonoBehaviour
{
    public List<EnemySkill> InherentSkillList;
    public List<EnemySkill> usableSpecialSkillList;

    List<GameObject> SpecialSkillObjectList;

    Enemy myEnemy;
    public int SpecialSkillCount = 4;

    public EnemySkill GetInherentSkill(int idx)
    {
        return InherentSkillList[idx];
    }

    //Dictionary<EnemySkill.SkillID, EnemySkill> skillDictionary;
    //public EnemySkill GetSkill(EnemySkill.SkillID skillNameEnum)
    //{
    //    return skillDictionary[skillNameEnum];
    //}

    void Awake()
    {
        myEnemy = transform.parent.GetComponent<Enemy>();
        // skillDictionary = new Dictionary<EnemySkill.SkillID, EnemySkill>();
    }

    private void Start()
    {
        // uploadSpecialSkills();
    }


    //void uploadSpecialSkills()
    //{
    //    SpecialSkillObjectList = SpecialSkillObjectList.OrderBy(i => Guid.NewGuid()).ToList();

    //    if (myEnemy.enemyStateMachine.Rank == EnemyStateMachine.enumRank.Rare)
    //    {
    //        if (SpecialSkillObjectList.Count == 0)
    //        {
    //            print("SpecialSkillObjectList.Count == 0");
    //            return;
    //        }

    //        for (int i = 0; i < SpecialSkillCount; i++)
    //        {
    //            // 특수 스킬 부여
    //            GameObject specialSkill = Instantiate(SpecialSkillObjectList[i], transform.parent, false);

    //            EnemySkill enemySkill = SpecialSkillObjectList[i].GetComponent<EnemySkill>();
    //            if (enemySkill != null)
    //                usableSpecialSkillList.Add(enemySkill);
    //            else
    //                print("enemySkill is null");
    //        }
    //    }
    //}
}
