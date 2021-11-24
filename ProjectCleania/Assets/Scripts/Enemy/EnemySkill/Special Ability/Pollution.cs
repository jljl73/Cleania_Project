using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollution : ContactStayDamage
{
    [SerializeField]
    SkillEffectController effectController;

    PollutionGroup pollutionGroup;

    bool enrolledAbility = false;
    float duration;

    private void OnEnable()
    {
        if (!isSetUp)
            return;

        pollutionGroup.AddPollution(this);
        Invoke("DeactivateDelay", duration);
    }

    void Start()
    {
        effectController.Scale = damageRange * 0.3333f;
    }

    void DeactivateDelay() => this.gameObject.SetActive(false);

    public void SetUp(PollutionGroup pollutionGroup, float duration, AbilityStatus abil, float damageScale)
    {
        this.pollutionGroup = pollutionGroup;
        this.duration = duration;
        base.SetUp(abil, damageScale);
    }

    private void OnDisable()
    {
        if (isSetUp)
        {
            pollutionGroup.PopPollution(this);
            if (enrolledAbility)
            {
                pollutionGroup.PopAbility();
                enrolledAbility = false;
            }
            SetUp(null, 0, null, 0);
            isSetUp = false;
        }
        ObjectPool.ReturnObject(ObjectPool.enumPoolObject.Pollution, this.gameObject);
        CancelInvoke();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AbilityStatus abil = other.gameObject.GetComponent<AbilityStatus>();
            if (abil != null)
            {
                abil.AttackedBy(ownerAbility, damageScale);
                pollutionGroup.AddAbility(abil);
                enrolledAbility = true;
            }
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AbilityStatus abil = other.gameObject.GetComponent<AbilityStatus>();
            if (abil != null)
            {
                pollutionGroup.PopAbility();
                enrolledAbility = false;
            }
        }
    }
}
