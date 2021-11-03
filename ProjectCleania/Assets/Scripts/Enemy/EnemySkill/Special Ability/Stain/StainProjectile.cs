using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StainProjectile : DamagingProperty
{
    float stopTime;         // 총 stop 시간
    float stopStartTime;    // stop 시작 시간
    bool isSetUp = false;
    bool didStop = false;
    float spentTimeSum = 0f;

    Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        if (rigidbody == null)
            throw new System.Exception("StainProjectile doesnt have Rigidbody");
    }

    private void Update()
    {
        if (!isSetUp) return;

        spentTimeSum += Time.deltaTime;

        if (spentTimeSum > stopStartTime)
        {
            if (didStop) return;
            StartCoroutine("Stop", stopTime);
        }
    }

    IEnumerator Stop(float time)
    {
        Vector3 tempVel = rigidbody.velocity;
        rigidbody.velocity = Vector3.zero;
        rigidbody.useGravity = false;
        didStop = true;

        yield return new WaitForSeconds(time);

        rigidbody.useGravity = true;
        rigidbody.velocity = tempVel;
    }

    public void SetUp(float stopTime, float stopStartTime)
    {
        this.stopTime = stopTime;
        this.stopStartTime = stopStartTime;
        isSetUp = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("플레이어 stain 맞음!");
            Destroy(gameObject);
        }
    }
}
