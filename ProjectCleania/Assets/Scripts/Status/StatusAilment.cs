using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusAilment : MonoBehaviour
{
    [SerializeField]
    BaseCharacterController baseCharacterController;
    AbilityStatus ownerAbilityStatus;

    void Awake()
    {
        baseCharacterController = GetComponent<BaseCharacterController>();
        ownerAbilityStatus = GetComponent<AbilityStatus>();
    }

    void Update()
    {
        //foreach (AbilityStatus abil in continuousDamagetempTimeCalculation.Keys)
        //{
        //    continuousDamagetempTimeCalculation[abil] += Time.deltaTime;
        //    if (continuousDamagetempTimeCalculation[abil] > 1)
        //    {
        //        continuousDamagetempTimeCalculation[abil] = 0;
        //        continuousDamageDuration[abil] -= 1;
        //        if (continuousDamageDuration[abil] != 0)
        //            ownerAbilityStatus.AttackedBy(abil, 0.1f);
        //    }
        //}
    }

    // �ൿ ������
    float[] _behaviorRestrictionOptions = { 0, 0, 0, 0 };
    int[] _behaviorRestrictionOptionsOvelapped = { 0, 0, 0, 0 };
    public enum BehaviorRestrictionType
    {
        Restraint,
        Stun,
        Silence,
        Dark
    }

    public float this[BehaviorRestrictionType index]
    {
        get => _behaviorRestrictionOptions[(int)index];
    }

    public void RestrictBehavior(BehaviorRestrictionType option, float duration)
    {
        if (CheckRestrictBehaviorOvelapped(option))
            return;

        baseCharacterController.SetStatusAilment(option, true);
        _behaviorRestrictionOptions[(int)option] += duration;
        _behaviorRestrictionOptionsOvelapped[(int)option] += 1;
        StartCoroutine(OffRestrictBehavior(option, duration));
        Debug.Log("behaviorRestriction On : " + option.ToString() + " : " + _behaviorRestrictionOptions[(int)option]);
    }
    IEnumerator OffRestrictBehavior(BehaviorRestrictionType option, float duration)
    {
        yield return new WaitForSeconds(duration);
        baseCharacterController.SetStatusAilment(option, false);
        _behaviorRestrictionOptions[(int)option] -= duration;
        _behaviorRestrictionOptionsOvelapped[(int)option] -= 1;
        Debug.Log("behaviorRestriction Off : " + option.ToString() + " : " + _behaviorRestrictionOptions[(int)option]);
    }

    bool CheckRestrictBehaviorOvelapped(BehaviorRestrictionType option)
    {
        bool result = true;
        switch (option)
        {
            case BehaviorRestrictionType.Restraint:
            case BehaviorRestrictionType.Stun:
            case BehaviorRestrictionType.Silence:
                if (_behaviorRestrictionOptionsOvelapped[(int)option] >= 1)
                    result = false;
                break;
            case BehaviorRestrictionType.Dark:
                if (_behaviorRestrictionOptionsOvelapped[(int)option] >= 5)
                    result = false;
                break;
            default:
                break;
        }
        return result;
    }







    Dictionary<AbilityStatus, float> continuousDamageDuration = new Dictionary<AbilityStatus, float>();
    Dictionary<AbilityStatus, float> continuousDamagetempTimeCalculation = new Dictionary<AbilityStatus, float>();
    int[] _continuousDamageOptionsOvelapped = { 0, 0, 0, 0 };
    public enum ContinuousDamageType
    {
        Addiction
    }
    public void DamageContinuously(ContinuousDamageType option, float duration, AbilityStatus attackerAbil)
    {
        if (CheckContinuousDamageOvelapped(option))
            return;

        AddAbilityStatus(option, duration, attackerAbil);
        ownerAbilityStatus.AttackedBy(attackerAbil, 0.1f);
        _continuousDamageOptionsOvelapped[(int)option] += 1;
        StartCoroutine(OffContinuousDamage(option, duration, attackerAbil));
        Debug.Log("DamageContinuously On : " + option.ToString() + " : " + _continuousDamageOptionsOvelapped[(int)option]);
    }

    IEnumerator OffContinuousDamage(ContinuousDamageType option, float duration, AbilityStatus abil)
    {
        yield return new WaitForSeconds(duration);
        RemoveAbilityStatus(option, abil);
        _continuousDamageOptionsOvelapped[(int)option] -= 1;
        Debug.Log("DamageContinuously Off : " + option.ToString() + " : " + _continuousDamageOptionsOvelapped[(int)option]);
    }

    void AddAbilityStatus(ContinuousDamageType option, float duration, AbilityStatus attackerAbil)
    {
        switch (option)
        {
            case ContinuousDamageType.Addiction:
                continuousDamageDuration.Add(attackerAbil, duration);
                continuousDamagetempTimeCalculation.Add(attackerAbil, 0);
                break;
            default:
                break;
        }
    }

    void RemoveAbilityStatus(ContinuousDamageType option, AbilityStatus attackerAbil)
    {
        switch (option)
        {
            case ContinuousDamageType.Addiction:
                continuousDamageDuration.Remove(attackerAbil);
                continuousDamagetempTimeCalculation.Remove(attackerAbil);
                break;
            default:
                break;
        }
    }

    bool CheckContinuousDamageOvelapped(ContinuousDamageType option)
    {
        bool result = true;
        switch (option)
        {
            case ContinuousDamageType.Addiction:
                if (_continuousDamageOptionsOvelapped[(int)option] >= 10)
                    result = false;
                break;
            default:
                break;
        }
        return result;
    }
}
