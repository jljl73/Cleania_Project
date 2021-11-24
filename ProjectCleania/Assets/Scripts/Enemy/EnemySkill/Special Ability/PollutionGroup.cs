using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollutionGroup : DamagingProperty
{
    List<Pollution> pollutionList = new List<Pollution>();

    [SerializeField]
    int abilityCount = 0;
    [SerializeField]
    int prevAbilityCount = 0;

    AbilityStatus abilityStatus;

    float pollutionDuration = 3;
    float damageRate;
    bool abilityActivate = false;

    public void AddPollution(Pollution pollution)
    {
        pollutionList.Add(pollution);
    }

    public void PopPollution(Pollution pollution)
    {
        pollutionList.Remove(pollution);
    }

    public void AddAbility(AbilityStatus abilityStatus)
    {
        abilityCount += 1;
        this.abilityStatus = abilityStatus;
    }

    public void PopAbility()
    {
        abilityCount -= 1;
        if (abilityCount == 0)
        {
            abilityStatus = null;
        }
    }

    float timePassed = 0f;

    void GiveDamage()
    {
        if (abilityCount < 0)
            throw new System.Exception("ability count is under 0");
        else if (abilityCount == 0)
        {
            timePassed = 0f;
            return;
        }
        else if (abilityCount > 0 && prevAbilityCount != 0)
        {
            timePassed += Time.deltaTime;

            if (timePassed < 1f)
                return;
            else
                timePassed = 0f;
        }
        //else
        //    print("First Hit!");

        //if (abilityCount == 0)
        //{
        //    timePassed = 0f;
        //    return;
        //}
        //else if (abilityCount > 0 && prevAbilityCount == 0)
        //{
        //    abilityStatus.AttackedBy(ownerAbility, damageScale);
        //    return;
        //}
        //else if (abilityCount < 0)
        //    throw new System.Exception("ability count is under 0");
        //else
        //{
        //    timePassed += Time.deltaTime;

        //    if (timePassed < 1f)
        //        return;
        //    else
        //        timePassed = 0f;
        //}
        abilityStatus.AttackedBy(ownerAbility, damageScale);
    }

    public void Activate(bool value)
    {
        abilityActivate = value;
    }

    public void SetUp(float duration, AbilityStatus ownerabil, float damageRate)
    {
        this.pollutionDuration = duration;
        base.SetUp(ownerabil, damageRate);
    }

    private void FixedUpdate()
    {
        if (!abilityActivate) return;
        Pollution pollution = ObjectPool.SpawnFromPool<Pollution>(ObjectPool.enumPoolObject.Pollution, this.transform.position, this.transform.rotation);
        pollution.SetUp(this, pollutionDuration, ownerAbility, damageRate);
        pollution.Resize(damageRange);

        GiveDamage();
        prevAbilityCount = abilityCount;
    }
}
