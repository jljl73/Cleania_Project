using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
    }



    virtual public void Activate()
    {
    }

    virtual public void AnimationActivate() { }

    virtual public void AnimationDeactivate() { }

}
