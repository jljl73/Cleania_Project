using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public Animator animator;
    public EnemyAnimationEvent animationEvent;
    public Skill skill;


    public int HP = 100;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            HP -= 10;
            if (HP <= 30)
            {
                animator.SetTrigger("Cast");
                animationEvent.SetSkill(skill);
            }
        }
    }

}
