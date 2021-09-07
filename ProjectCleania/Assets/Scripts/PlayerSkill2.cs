using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill2 : Skill
{
    public int damage = 10;
    public int reduceArmor = 10;

    Collider col;

    private void Start()
    {
        col = GetComponent<Collider>();
    }

    public override void AnimationActivate()
    {
        col.enabled = true;
        Invoke("OffSkill", 1.0f);
    }

    override public void Activate()
    {
        col.enabled = true;
        Invoke("OffSkill", 1.0f);
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
    
    void OffSkill()
    {
        col.enabled = false;
    }

}
