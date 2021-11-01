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
    // GameObject enemy;
    EnemyGroupManager myGroupManager;
    AbilityStatus targetObjAbility;

    public float CognitiveRange = 7;

    Collider[] overlappedColiders;

    private void Awake()
    {
        // enemySpawner = transform.parent.GetComponent<Enemy>().EnemySpawner;
        // myGroupManager = enemySpawner.GetComponent<EnemyGroupManager>();
        // enemy = transform.parent.gameObject;

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

                enemySpawner.GetComponent<EnemyGroupManager>().SetTarget(collider.gameObject);
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
    //    if(other.CompareTag("Player"))
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
