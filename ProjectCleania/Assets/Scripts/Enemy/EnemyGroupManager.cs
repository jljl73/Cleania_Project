using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupManager : MonoBehaviour
{
    List<GameObject> enemies = new List<GameObject>();
    GameObject target;
    public GameObject Target
    {
        get { return target; }
        set
        {
            foreach (var e in enemies)
            {
                e.GetComponent<Enemy>().SetTarget(value);
            }
            this.target = value;
        }
    }
    const float chasingDistance = 10.0f;

    private void OnDisable()
    {
        target = null;
    }

    public void AddMember(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void DeleteMember(GameObject enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count == 0)
            Target = null;
    }

    //public void SetTarget(GameObject target)
    //{
    //    foreach (var e in enemies)
    //    {
    //        e.GetComponent<Enemy>().SetTarget(target);
    //    }
    //    this.target = target;
    //}

    public void ReleaseTarget()
    {
        if (CheckCollidedObject() > 0) return;

        foreach (var e in enemies)
            e.GetComponent<Enemy>().ReleaseTarget();
    }

    int CheckCollidedObject()
    {
        int sum = 0;
        if (target == null) return 1;

        foreach (var e in enemies)
        {
            if (Vector3.Distance(target.transform.position, e.transform.position) < chasingDistance)
                ++sum;
        }

        return sum;
    }
}
