using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRelease : MonoBehaviour
{
    Enemy enemy;

    void Start()
    {
        enemy = transform.parent.GetComponent<Enemy>();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            enemy.enemySpawner.GetComponent<EnemyGroupManager>().ReleaseTarget();
    }
}
