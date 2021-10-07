using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public enum ENEMYSTATE { Idle, Chasing, Attack, Dead };
    public Animator animator;

    ENEMYSTATE _state;
    public ENEMYSTATE State
    {
        get
        {
            return _state;
        }
    }

    public void Transtion(ENEMYSTATE destState)
    {
        _state = destState;
    }

    public void Damaged()
    {
        animator.SetBool("Dead", true);
    }
}
