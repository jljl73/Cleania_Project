using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public bool isAttacking;
    public float CoolTime;  // 추후 private 처리
    public float GetCoolTime { get { return CoolTime; } }

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
