using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DustStorm", menuName = "Scriptable Object/Enemy/DustStorm")]
public class DustStormSO : EnemySkillSO
{
    [Header("Èí¼ö ÇÇÇØ ¹üÀ§")]
    [SerializeField]
    float damageRadius = 1;
    public float GetDamageRadius() { return damageRadius; }

    [Header("²ø·Á°¨ ¼Óµµ")]
    [SerializeField]
    float pulledSpeed = 5;
    public float GetPulledSpeed() { return pulledSpeed; }

    [Header("ÆøÇ³ Áö¼Ó ½Ã°£")]
    [SerializeField]
    float stormDuration = 5;
    public float GetstormDuration() { return stormDuration; }

    [Header("ÆøÇ³ ÇÇÇØÀ²")]
    [SerializeField]
    float stormDamageRate = 1;
    public float GetStormDamageRate() { return stormDamageRate; }

    [Header("ÆøÇ³ ÇÇÇØ ¹üÀ§")]
    [SerializeField]
    float stormDamageRadius = 1;
    public float GetStormDamageRadius() { return stormDamageRadius; }

    [Header("ÆøÇ³ ¹ÐÄ§ Èû ex) ÇÇ°ÝÃ¼ = 100kg & 10 drag ÀÏ¶§, 62,500ÈûÀº 1Ä­À» ¹Ð¾îº¸³¿")]
    [SerializeField]
    float stormForce = 100f;
    public float GetStormForce() { return stormForce; }

    [Header("¾ÏÈæ ½Ã¾ß ¹üÀ§")]
    [SerializeField]
    float sightHindRange = 0.3f;
    public float GetSightHindRange() { return sightHindRange; }

    [Header("¾ÏÈæ Áö¼Ó ½Ã°£")]
    [SerializeField]
    float sightHindDuration = 4f;
    public float GetsightHindDuration() { return sightHindDuration; }

    [Header("¹ßµ¿ È®·ü")]
    [SerializeField]
    float triggerProbability = 0.3f;
    public float GetTriggerProbability() { return triggerProbability; }
}
