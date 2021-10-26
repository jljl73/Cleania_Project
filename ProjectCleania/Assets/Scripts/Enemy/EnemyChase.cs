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
            myGroupManager.AddMember(enemy);

        }
    }
    GameObject enemy;
    EnemyGroupManager myGroupManager;
    AbilityStatus targetObjAbility;

    private void Awake()
    {
        // enemySpawner = transform.parent.GetComponent<Enemy>().EnemySpawner;
        // myGroupManager = enemySpawner.GetComponent<EnemyGroupManager>();
        enemy = transform.parent.gameObject;

    }

    private void Update()
    {
        if (targetObjAbility == null) return;
        if (targetObjAbility.HP == 0)
        {
            if (enemySpawner != null)
                enemySpawner.GetComponent<EnemyGroupManager>().ReleaseTarget();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (enemySpawner != null)
            {
                //enemySpawner.GetComponent<EnemyGroupManager>().SetTarget(other.gameObject);
                myGroupManager.SetTarget(other.gameObject);
                targetObjAbility = other.gameObject.GetComponent<AbilityStatus>();
            }
        }
    }

    void OnDestroy()
    {
        if(enemySpawner != null) enemySpawner.GetComponent<EnemyGroupManager>().DeleteMember(enemy);
    }
}
