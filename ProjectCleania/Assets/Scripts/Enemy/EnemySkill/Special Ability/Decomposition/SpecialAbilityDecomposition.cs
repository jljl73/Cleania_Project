using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbilityDecomposition : EnemySkill
{
    float damageScale = 10;
    float objectSize;
    float CreationRadius;           
    float existTime = 7f;
    float speed = 0.5f;
    float explodeWaitTime = 3f;
    float explodeDamageRange = 5f;
    float stunTime = 2f;
    GameObject target;

    public GameObject DecompositionPrefab;

    [SerializeField]
    SpecialAbilityDecompositionSO skillData;

    public override bool IsPassiveSkill { get { return skillData.IsPassiveSkill; } }
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
        if (DecompositionPrefab == null)
            throw new System.Exception("SpecialAbilityDecomposition doesnt have DecompositionPrefab");
    }

    private new void Start()
    {
        base.Start();

        UpdateSkillData();
    }

    public void UpdateSkillData()
    {
        if (skillData == null)
            throw new System.Exception("BatSkill1 no skillData");

        base.UpdateSkillData(skillData);

        damageScale = skillData.GetDamageRate();
        objectSize = skillData.GetObjectSize();
        CreationRadius = skillData.GetCreationRadius();
        existTime = skillData.GetExistTime();
        speed = skillData.GetSpeed();
        explodeWaitTime = skillData.GetExplodeWaitTime();
        explodeDamageRange = skillData.GetExplodeDamageRange();
        stunTime = skillData.GetStunTime();
    }

    public override bool AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSpecialSkill", true);
        animator.SetTrigger("Decomposition");

        return true;
    }

    override public void Activate()
    {
        MakeMines();
    }

    void MakeMines()
    {
        GameObject initiatedObj = Instantiate(DecompositionPrefab, transform.position, transform.rotation);
        initiatedObj.transform.position = GetRandomPointInCircle(transform.position, CreationRadius);
        Decomposition decomposition = initiatedObj.GetComponent<Decomposition>();
        if (decomposition != null)
        {
            decomposition.SetUp(existTime, speed, explodeWaitTime, explodeDamageRange, stunTime, enemyMove.TargetObject, OwnerAbilityStatus, damageScale);
            decomposition.Resize(objectSize);
        }
    }

    Vector3 GetRandomPointInCircle(Vector3 center, float distance)
    {
        Vector3 randomPos = Random.insideUnitSphere * distance + center;
        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(randomPos, out hit, distance, UnityEngine.AI.NavMesh.AllAreas))
            return hit.position;
        else
            return center;
    }

    public override void Deactivate()
    {
        animator.SetBool("OnSpecialSkill", false);
        animator.SetBool("OnSkill", false);
    }
}
