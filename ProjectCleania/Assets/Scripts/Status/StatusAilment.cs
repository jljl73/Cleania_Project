using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusAilment : MonoBehaviour
{
    AbilityStatus ownerAbilityStatus;

    void Awake()
    {
        ownerAbilityStatus = GetComponent<AbilityStatus>();
    }

    // 행동 제한형
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
        if (!CheckRestrictBehaviorOvelapped(option))
            return;
        _behaviorRestrictionOptions[(int)option] += duration;
        _behaviorRestrictionOptionsOvelapped[(int)option] += 1;
        StartCoroutine(OffRestrictBehavior(option, duration));
        Debug.Log("behaviorRestriction On : " + option.ToString() + " : " + _behaviorRestrictionOptions[(int)option]);
    }
    IEnumerator OffRestrictBehavior(BehaviorRestrictionType option, float duration)
    {
        yield return new WaitForSeconds(duration);
        _behaviorRestrictionOptions[(int)option] -= duration;
        if (_behaviorRestrictionOptions[(int)option] <= 0)
            _behaviorRestrictionOptions[(int)option] = 0;
        _behaviorRestrictionOptionsOvelapped[(int)option] -= 1;
        if (_behaviorRestrictionOptionsOvelapped[(int)option] <= 0)
            _behaviorRestrictionOptionsOvelapped[(int)option] = 0;
        Debug.Log("behaviorRestriction Off : " + option.ToString() + " : " + _behaviorRestrictionOptions[(int)option]);
    }

    public void ForceOffRestrictBehavior(BehaviorRestrictionType option)
    {
        if (!CheckRestrictBehaviorOvelapped(option))
            return;
        _behaviorRestrictionOptions[(int)option] = 0;
        _behaviorRestrictionOptionsOvelapped[(int)option] = 0;
        Debug.Log("behaviorRestriction On : " + option.ToString() + " : " + _behaviorRestrictionOptions[(int)option]);
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



    void Update()
    {
        // 시간 지남 계산
        CalculateTimePassed();

        // Ability status에 1초마다 데미지 주기
        ContinuousDamage();
    }


    // 지속 피해형
    [SerializeField]
    List<float> continuousDamageDurationLeftList = new List<float>();
    [SerializeField]
    List<float> oneSecPassCheckList = new List<float>();
    [SerializeField]
    List<float> continuousDamageDurationTotalList = new List<float>();
    [SerializeField]
    List<float> continuousDamageRateList = new List<float>();
    int continuousDamageOptionsOverlapped = 0;

    const float CONTINUOUS_DAMAGE_DURATION = 8f;

    public void DamageContinuously(AbilityStatus attackerAbil)
    {
        if (CheckContinuousDamageOvelapped())
            return;

        AddDamageRate(CONTINUOUS_DAMAGE_DURATION, attackerAbil.GetStat(Ability.Stat.Attack));
        continuousDamageOptionsOverlapped += 1;
        // Debug.Log("DamageContinuously On current Overlapped: " + continuousDamageOptionsOverlapped);
    }

    public void DamageContinuously(float damageRate)
    {
        if (CheckContinuousDamageOvelapped())
            return;

        AddDamageRate(CONTINUOUS_DAMAGE_DURATION, damageRate);
        continuousDamageOptionsOverlapped += 1;
        // Debug.Log("DamageContinuously On current Overlapped: " + continuousDamageOptionsOverlapped);
    }

    void AddDamageRate(float duration, float damageRate)
    {
        continuousDamageDurationLeftList.Add(duration);
        continuousDamageDurationTotalList.Add(duration);
        continuousDamageRateList.Add(damageRate);

        // 시작하자마자 데미지 주기 위해 1 설정
        oneSecPassCheckList.Add(1);
    }

    void RemoveDamageRate(int idx)
    {
        continuousDamageDurationLeftList.RemoveAt(idx);
        continuousDamageDurationTotalList.RemoveAt(idx);
        continuousDamageRateList.RemoveAt(idx);
        oneSecPassCheckList.RemoveAt(idx);
        continuousDamageOptionsOverlapped -= 1;
        // Debug.Log("DamageContinuously Off current Overlapped: " + continuousDamageOptionsOverlapped);
    }

    bool CheckContinuousDamageOvelapped()
    {
        bool result = false;

        if (continuousDamageOptionsOverlapped >= 10)
            result = true;

        return result;
    }

    void CalculateTimePassed()
    {
        // 시간 지남 계산
        for (int i = 0; i < continuousDamageDurationLeftList.Count; i++)
        {
            continuousDamageDurationLeftList[i] -= Time.deltaTime;
            oneSecPassCheckList[i] += Time.deltaTime;

            // 시간 다 지나면 삭제
            if (continuousDamageDurationLeftList[i] <= 0f)
            {
                RemoveDamageRate(i);
            }
        }
    }

    void ContinuousDamage()
    {
        // Ability status에 1초마다 데미지 주기
        for (int i = 0; i < continuousDamageRateList.Count; i++)
        {
            if (oneSecPassCheckList[i] > 1f)
            {
                oneSecPassCheckList[i] = 0;
                // ownerAbilityStatus.AttackedBy()
            }
        }
    }
}
