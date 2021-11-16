using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class PlayerSkill : Skill
{
    protected AudioSource audioSource;
    protected List<AudioClip> audioClips = new List<AudioClip>();

    // ¿€µø ≈∞
    protected KeyCode SkillSlotDependency = KeyCode.Alpha1;
    public KeyCode GetSkillSlotDependency() { return SkillSlotDependency; }

    protected void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            throw new System.Exception("PlayerSkill audioSource is null!");
    }

    protected new void Start()
    {
        base.Start();
    }

    public virtual void UpdateSkillData(PlayerSkillSO skillData)
    {
        ID = skillData.ID;
        SkillName = skillData.GetSkillName();
        SkillDetails = skillData.GetSkillDetails();
        CoolTime = skillData.GetCoolTime();
        CreatedMP = skillData.GetCreatedMP();
        ConsumMP = skillData.GetConsumMP();
        SpeedMultiplier = skillData.GetSpeedMultiplier();

        SkillSlotDependency = skillData.GetTriggerKey();

        for (int i = 0; i < skillData.GetSkillSoundCount(); i++)
        {
            audioClips.Add(skillData.GetSkillSound(i));
        }
    }

    public override void ActivateSound(int index)
    {
        if (audioClips.Count == 0)
            return;

        audioSource.clip = audioClips[index];
        audioSource.Play();
        print("activateSkill sound!");
    }

    public override void DeactivateSound(int index)
    {
        if (audioClips.Count == 0)
            return;

        audioSource.clip = audioClips[index];
        audioSource.Stop();
    }
}
