using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public GameObject enemySpawner;
    GameObject enemy;

    void Start()
    {
        enemy = transform.parent.gameObject;
        enemySpawner = transform.parent.GetComponent<Enemy>().enemySpawner;
        enemySpawner.GetComponent<EnemyGroupManager>().AddMember(enemy);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            enemySpawner.GetComponent<EnemyGroupManager>().SetTarget(other.gameObject);
        }
    }

    void OnDestroy()
    {
        if(enemySpawner != null) enemySpawner.GetComponent<EnemyGroupManager>().DeleteMember(enemy);
    }
}
