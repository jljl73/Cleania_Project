using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public Animator animator;

    public string SkillName;
    [TextArea]
    public string SkillDetails;

    // public bool isAttacking;
    public float CoolTime;  // 추후 private 처리
    public float GetCoolTime { get { return CoolTime; } }
    public float CreatedMP = 0f;
    public float ConsumMP = 0f;

    public float speedMultiplier = 1.0f;

    // Start is called before the first frame update
    protected void Start()
    {
        // isAttacking = false;
    }

    public abstract void Activate();

    public abstract void AnimationActivate();

    public abstract void Deactivate();
}
