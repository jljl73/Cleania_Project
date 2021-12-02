using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillSweeping : PlayerSkill
{
    [SerializeField]
    PlayerSkillSweepingSO skillData;
    float skillScale = 0.0f;

    CapsuleCollider col;

    // "경직 시간"
    float stunTime = 2;
    public float GetStunTime() { return stunTime; }

    // "쓸어담기 범위"
    float sweepRange = 2f;
    public float GetSweepRange() { return sweepRange; }

    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
        col = GetComponent<CapsuleCollider>();
        UpdateSkillData();
    }

    protected new void Start()
    {
        base.Start();
        GameManager.Instance.player.OnLevelUp.AddListener(UpdateSkillData);
        animator.SetFloat("Sweeping multiplier", SpeedMultiplier);

        effectController[0].Scale = sweepRange * 0.3333f;
    }
    
    public void UpdateSkillData()
    {
        base.UpdateSkillData(skillData);

        stunTime = skillData.GetStunTime();
        sweepRange = skillData.GetSweepRange();
    }

    public override bool AnimationActivate()
    {
        base.AnimationActivate();

        //animator.SetInteger("Skill", 2);
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSkill2", true);
        animator.SetTrigger("Sweeping");

        return true;
    }

    override public void Activate()
    {
        col.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            // 기절
            other.GetComponent<Enemy>().OnStunned(true, stunTime);

            //if (other.GetComponent<Enemy>().abilityStatus.AttackedBy(OwnerAbilityStatus, skillScale) == 0)
            //    other.GetComponent<Enemy>().Die();
            //else
            //    other.GetComponent<Enemy>().enemyMove.WarpToPosition(transform.position + transform.forward);
        }
    }

    void OffSkill()
    {
        if (col != null)
            col.enabled = false;
    }

    public override void Deactivate()
    {
        animator.SetBool("OnSkill2", false);
        animator.SetBool("OnSkill", false);
        OffSkill();
    }

    public override void ActivateSound(int index)
    {
        GameManager.Instance.playerSoundPlayer.PlaySound(PlayerSoundPlayer.TYPE.Sweeping);
    }
}
