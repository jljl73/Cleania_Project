using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    GameObject enemySpawner;
    public GameObject EnemySpawner 
    {
        get { return enemySpawner; }
        set 
        { 
            enemySpawner = value;
            myGroupManager = enemySpawner.GetComponent<EnemyGroupManager>();
            myGroupManager.AddMember(enemy.gameObject);
        }
    }

    [SerializeField]
    Enemy enemy;
    EnemyGroupManager myGroupManager;
    AbilityStatus targetObjAbility;

    public float CognitiveRange { get; set; }

    SphereCollider cognitiveCollider;

    public void ActivateChase() => cognitiveCollider.enabled = true;
    public void DeactivateChase() => cognitiveCollider.enabled = false;

    void Awake()
    {
        if (enemy == null)
            throw new System.Exception("EnemyChase doesnt have Enemy");

        cognitiveCollider = GetComponent<SphereCollider>();
    }

    void Start()
    {
        CognitiveRange = 7;
        // CognitiveRange = 3;
        cognitiveCollider.radius = CognitiveRange;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (enemySpawner != null)
            {
                //if (targetObjAbility != null)
                //    return;
                
                myGroupManager.Target = other.gameObject;
                myGroupManager?.SetTarget();
                //if (enemySpawner == null)
                //    enemy.SetTarget(other.gameObject);
                //else
                //    myGroupManager.Target = other.gameObject;

                targetObjAbility = other.gameObject.GetComponent<AbilityStatus>();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            myGroupManager?.ReleaseTarget();
    }

    void OnDisable()
    {
        // if(enemySpawner != null) enemySpawner.GetComponent<EnemyGroupManager>().DeleteMember(gameObject);
        if (enemySpawner != null)
        {
            myGroupManager?.ReleaseTarget();
            myGroupManager?.DeleteMember(enemy.gameObject);
            enemySpawner = null;
        }
    }

    void OnDestroy()
    {
        // if(enemySpawner != null) enemySpawner.GetComponent<EnemyGroupManager>().DeleteMember(gameObject);
        if (enemySpawner != null)
        {
            myGroupManager?.ReleaseTarget();
            myGroupManager?.DeleteMember(enemy.gameObject);
            enemySpawner = null;
        }
    }
}
