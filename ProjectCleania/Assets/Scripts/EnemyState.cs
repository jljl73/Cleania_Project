using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public Animator animator;

    public void Damaged()
    {
        animator.SetBool("Dead", true);
    }

}
