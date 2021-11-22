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
            myGroupManager.AddMember(gameObject);

        }
    }
    Enemy enemy;
    EnemyGroupManager myGroupManager;
    AbilityStatus targetObjAbility;

    public float CognitiveRange = 7;

    Collider[] overlappedColiders;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        overlappedColiders = Physics.OverlapSphere(transform.position, CognitiveRange);
        foreach (Collider collider in overlappedColiders)
        {
            if (collider.CompareTag("Player"))
            {
                if (targetObjAbility != null)
                    break;

                if (enemySpawner == null)
                    enemy.SetTarget(collider.gameObject);
                else
                    myGroupManager.Target = collider.gameObject;

                targetObjAbility = collider.gameObject.GetComponent<AbilityStatus>();
                break;
            }
        }

        //if (targetObjAbility == null) return;
        //if (targetObjAbility.HP == 0)
        //{
        //    if (enemySpawner != null)
        //        enemySpawner.GetComponent<EnemyGroupManager>().ReleaseTarget();
        //}
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        if (enemySpawner != null)
    //        {
    //            //enemySpawner.GetComponent<EnemyGroupManager>().SetTarget(other.gameObject);
    //            myGroupManager.Target = other.gameObject;
    //            targetObjAbility = other.gameObject.GetComponent<AbilityStatus>();
    //        }
    //    }
    //}

    void OnDestroy()
    {
        if(enemySpawner != null) enemySpawner.GetComponent<EnemyGroupManager>().DeleteMember(gameObject);
    }

    
}
