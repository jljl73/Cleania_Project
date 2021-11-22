using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStorage : MonoBehaviour
{
    [SerializeField]
    List<Skill> SkillList;

    Dictionary<int, Skill> skillDictionary = new Dictionary<int, Skill>();

    protected bool isSkillUploaded = false;

    public Skill GetNormalSkill(int id)
    {
        return skillDictionary[id];
    }

    public void CopyNormalSkillDictTo(Dictionary<int, Skill> dict)
    {
        foreach (KeyValuePair<int, Skill> pair in skillDictionary)
        {
            dict.Add(pair.Key, pair.Value);
        }
    }

    void Awake()
    {
        if (isSkillUploaded) return;
        UploadSkills();
        isSkillUploaded = true;
    }

    protected virtual void UploadSkills()
    {
        foreach (Skill skill in SkillList)
        {
            skillDictionary.Add(skill.ID, skill);
        }
    }
}
