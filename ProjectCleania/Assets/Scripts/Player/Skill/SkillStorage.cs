using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStorage : MonoBehaviour
{
    public List<PlayerSkill> Skill1List;
    public List<PlayerSkill> Skill2List;
    public List<PlayerSkill> Skill3List;
    public List<PlayerSkill> Skill4List;
    public List<PlayerSkill> SkillCList;
    public List<PlayerSkill> SkillRList;

    Dictionary<PlayerSkill.SkillID, PlayerSkill> skillDictionary;
    public PlayerSkill GetSkill(PlayerSkill.SkillID skillNameEnum)
    {
        return skillDictionary[skillNameEnum];
    }

    void Awake()
    {
        skillDictionary = new Dictionary<PlayerSkill.SkillID, PlayerSkill>();
        uploadSkills();
    }


    void uploadSkills()
    {
        foreach (PlayerSkill skill in Skill1List)
        {
            skillDictionary.Add(skill.ID, skill);
        }

        foreach (PlayerSkill skill in Skill2List)
        {
            skillDictionary.Add(skill.ID, skill);
        }

        foreach (PlayerSkill skill in Skill3List)
        {
            skillDictionary.Add(skill.ID, skill);
        }

        foreach (PlayerSkill skill in Skill4List)
        {
            skillDictionary.Add(skill.ID, skill);
        }

        foreach (PlayerSkill skill in SkillCList)
        {
            skillDictionary.Add(skill.ID, skill);
        }

        foreach (PlayerSkill skill in SkillRList)
        {
            skillDictionary.Add(skill.ID, skill);
        }
    }
}
