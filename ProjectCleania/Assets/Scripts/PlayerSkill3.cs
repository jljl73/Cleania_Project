using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill3 : Skill
{
    public Animator animator;
    public GameObject hurricanePrefabs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void AnimationActivate()
    {
        animator.SetInteger("Skill", 3);
    }

    override public void Activate()
    {
        Quaternion left = transform.rotation;
        Quaternion right = transform.rotation;

        left *= Quaternion.Euler(0, 30.0f, 0.0f);
        right *= Quaternion.Euler(0, -30.0f, 0.0f);

        Instantiate(hurricanePrefabs, transform.position, left);
        Instantiate(hurricanePrefabs, transform.position, transform.rotation);
        Instantiate(hurricanePrefabs, transform.position, right);
    }
}
