using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRelease : MonoBehaviour
{
    Enemy enemy;
    EnemyChase enemyChase;

    void Start()
    {
        enemy = transform.parent.GetComponent<Enemy>();
        enemyChase = enemy.GetComponentInChildren<EnemyChase>();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            enemyChase.EnemySpawner.GetComponent<EnemyGroupManager>().ReleaseTarget();
    }
}
