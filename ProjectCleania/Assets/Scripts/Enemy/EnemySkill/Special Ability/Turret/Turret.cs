using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    GameObject projectilePrefab;

    GameObject targetObj;
    float shotInterval;
    float shotRange;
    float projectileSpeed;

    bool isWaitingForShoot = false;

    public void SetUp(GameObject targetObj, float shotInterval, float shotRange, float projectileSpeed)
    {
        this.targetObj = targetObj;
        this.shotInterval = shotInterval;
        this.shotRange = shotRange;
        this.projectileSpeed = projectileSpeed;
    }

    private void Update()
    {
        if (targetObj == null)
            return;

        if (Vector3.Distance(targetObj.transform.position, this.transform.position) > shotRange)
            return;

        if (isWaitingForShoot)
            return;
        StartCoroutine("ShotToTarget", shotInterval);
    }

    IEnumerator ShotToTarget(float interval)
    {
        RotateToTarget();
        GameObject obj = Instantiate(projectilePrefab, transform.position, transform.rotation);
        obj.transform.Translate(obj.transform.up * 1f);

        TurretProjectile proj = obj.GetComponent<TurretProjectile>();
        if (proj != null)
            proj.SetUp(projectileSpeed);

        Destroy(obj, 8);

        isWaitingForShoot = true;
        yield return new WaitForSeconds(3f);
        isWaitingForShoot = false;
    }

    void RotateToTarget()
    {
        transform.LookAt(targetObj.transform);
    }
}
