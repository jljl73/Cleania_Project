using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : DamagingProperty
{
    //[SerializeField]
    //GameObject projectilePrefab;

    GameObject targetObj;
    float shotInterval;
    float shotRange;
    float projectileSpeed;

    bool isWaitingForShoot = false;

    float turretDuration;
    float projectileDuration;

    private void OnEnable()
    {
        if (!isSetUp) return;

        isWaitingForShoot = false;

        Start();
    }

    private void OnDisable()
    {
        CancelInvoke();
        ObjectPool.ReturnObject(ObjectPool.enumPoolObject.Turret, this.gameObject);
    }

    private void Start()
    {
        Invoke("DeactivateDelay", turretDuration);
    }

    void DeactivateDelay() => gameObject.SetActive(false);

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

    public void SetUp(float turretDuration, float projectileDuration, GameObject targetObj, float shotInterval, float shotRange, float projectileSpeed,  AbilityStatus abil, float damageScale)
    {
        this.turretDuration = turretDuration;
        this.projectileDuration = projectileDuration;
        this.targetObj = targetObj;
        this.shotInterval = shotInterval;
        this.shotRange = shotRange;
        this.projectileSpeed = projectileSpeed;

        base.SetUp(abil, damageScale);
    }

    IEnumerator ShotToTarget(float interval)
    {
        RotateToTarget();

        TurretProjectile proj = ObjectPool.SpawnFromPool<TurretProjectile>(ObjectPool.enumPoolObject.TurretProjectile, transform.position, transform.rotation);
        proj.gameObject.transform.Translate(proj.gameObject.transform.up * 1f);

        proj.SetUp(projectileDuration, projectileSpeed, ownerAbility, damageScale);

        isWaitingForShoot = true;
        yield return new WaitForSeconds(3f);
        isWaitingForShoot = false;
    }

    void RotateToTarget()
    {
        transform.LookAt(targetObj.transform);
    }
}
