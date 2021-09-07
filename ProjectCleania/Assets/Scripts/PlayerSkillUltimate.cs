using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillUltimate : Skill
{
    // public Status status;
    bool bSkill = false;
    public int duration = 5;
    public int cost = 5;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void Activate()
    {
        if (bSkill)
            OffSkill();
        else
            StartCoroutine(OnSkill());
    }

    IEnumerator OnSkill()
    {
        bSkill = true;

        for (int i = 0; i < 5; ++i)
        {
            //if(status.UseSP(cost) || bSkill == false)
            //{
            //    break;
            //}
            
            yield return new WaitForSeconds(1.0f);
        }

        OffSkill();
    }

    void OffSkill()
    {
        bSkill = false;
    }
}

