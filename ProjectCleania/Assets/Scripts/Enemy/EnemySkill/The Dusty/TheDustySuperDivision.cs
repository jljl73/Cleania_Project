using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDustySuperDivision : EnemySkill
{
    public EnemyEvent enemyEvent;
    public GameObject theDusty;
    public int nDivision = 3;

    //private new void Start()
    //{
    //    //enemyEvent.RegisterListener(Activate);
    //}

    public override void Activate()
    {
        for (int i = nDivision - 1; i >= 0; --i)
        {
            GameObject newObject = Instantiate(theDusty, transform.position, transform.rotation);
            newObject.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
        }

        gameObject.SetActive(false);
    }

    public override bool AnimationActivate()
    {
        animator.SetTrigger("Cast");

        return true;
    }

    public override void Deactivate()
    {

    }
}
