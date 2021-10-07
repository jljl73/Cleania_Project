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
        enemySpawner.GetComponent<EnemyGroupManager>().AddMember(transform.parent.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            enemySpawner.GetComponent<EnemyGroupManager>().SetTarget(other.gameObject);
        }
    }

}
