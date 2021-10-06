using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public StateMachine playerStateMachine;
    public Animator animator;

    public bool isAttacking;
    public float CoolTime;  // 추후 private 처리
    public float GetCoolTime { get { return CoolTime; } }
    public float ConsumMP = 0f;

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
    }

    public abstract void Activate();

    public abstract void AnimationActivate();

    public abstract void AnimationDeactivate();
}
