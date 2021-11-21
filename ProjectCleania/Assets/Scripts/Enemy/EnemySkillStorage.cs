using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class EnemySkillStorage : SkillStorage
{
    public List<Skill> SpecialSkillCandidates;
    Dictionary<int, Skill> specialSkillDictionary = new Dictionary<int, Skill>();

    //public int SpecialSkillCount = 4;

    public Skill GetSpeacialSkill(int id)
    {
        return specialSkillDictionary[id];
    }

    public Skill GetSpecialSkillFromList(int id)
    {
        for (int i = 0; i < SpecialSkillCandidates.Count; i++)
        {
            if (id == SpecialSkillCandidates[i].ID)
                return SpecialSkillCandidates[i];
        }

        return null;
    }

    void Awake()
    {
        if (isSkillUploaded) return;
        UploadSkills();
        isSkillUploaded = true;
    }
  
    protected override void UploadSkills()
    {
        base.UploadSkills();
        UploadSpecialSkill();
    }
    void UploadSpecialSkill()
    {
        foreach (Skill skill in SpecialSkillCandidates)
        {
            specialSkillDictionary.Add(skill.ID, skill);
        }
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
