using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDustySkill_3 : Skill
{
    public GameObject theDusty;
    public int nDivision = 3;
    
    public override void Activate()
    {
        for (int i = nDivision - 1; i >= 0; --i)
        {
            GameObject newObject = Instantiate(theDusty, transform.position, transform.rotation);
            newObject.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
        }

        gameObject.SetActive(false);
    }

    public override void AnimationActivate()
    {
        animator.SetTrigger("Cast");
    }

    public override void AnimationDeactivate()
    {

    }
}
