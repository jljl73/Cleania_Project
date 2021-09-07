using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill2 : Skill
{
    public int damage = 10;
    public int reduceArmor = 10;

    private void Start()
    {
        
    }

    override public void Activate()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            //other.GetComponent<Status>().Damaged(damage);
            // n√ ∞£
            //other.GetComponent<Status>().ReduceArmor(damage, n);
        }
    }
    

}
