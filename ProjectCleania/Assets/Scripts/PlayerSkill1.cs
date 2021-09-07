using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill1 : Skill
{
    //public GameObject status;
    public float speed = 10.0f;
    float duration = 0f;
    bool bSkill = false;
    int nDeadEnemy = 0;

    override public void Activate()
    {
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
        //status.GetComponent<PlayerStatus>().SetSpeed(speed);
        
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
