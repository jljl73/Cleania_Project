using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StormWindController : MonoBehaviour
{
    [SerializeField]
    GameObject stormWindPrefab;

    //List<GameObject> winds = new List<GameObject>();

    bool rotateCW = true;
    float rotateSpeed = 90f;
    float rotateRadius = 3f;
    int count;
    float duration;

    float stormDamageScale;
    float stormDamageRange;
    AbilityStatus ownerAbility;

    bool isSetUp = false;

    private void OnEnable()
    {
        if (!isSetUp) return;

        Start();
    }

    private void OnDisable()
    {
        CancelInvoke();
        ObjectPool.ReturnObject(ObjectPool.enumPoolObject.StormWindGroup, this.gameObject);
    }

    private void Start()
    {
        MakeWinds(rotateCW);
        Invoke("DeactivateDelay", duration);
        //Destroy(this.gameObject, duration);
    }

    void DeactivateDelay() => this.gameObject.SetActive(false);

    // Initiate 후 함수 호출 필수
    public void SetUp(bool isCW, float rotateRadius, float rotateSpeed, int count, float duration, AbilityStatus abil, float damageScale, float damageRange)
    {
        this.rotateCW = isCW;
        this.rotateRadius = rotateRadius;
        this.rotateSpeed = rotateSpeed;
        this.count = count;
        this.duration = duration;

        ownerAbility = abil;
        stormDamageScale = damageScale;
        stormDamageRange = damageRange;
        isSetUp = true;
    }

    void MakeWinds(bool isCW)
    {
        for (int i = 0; i < count; i++)
        {
            //GameObject obj = Instantiate(stormWindPrefab, GetRandomPosOnCircleEdge(), transform.rotation, transform);
            // winds.Add(obj);

            StormWind stormWind = ObjectPool.SpawnFromPool<StormWind>(ObjectPool.enumPoolObject.StormWind, GetRandomPosOnCircleEdge(), transform.rotation, transform);

            if (isCW)
                stormWind.SetUp(this.gameObject, rotateSpeed);
            else
                stormWind.SetUp(this.gameObject, -rotateSpeed);

            stormWind.SetUp(ownerAbility, stormDamageScale);
            stormWind.Resize(stormDamageRange);
        }
    }

    Vector3 GetRandomPosOnCircleEdge()
    {
        float rad = Random.Range(0f, 2 * Mathf.PI);
        if (!rotateCW)
            rad *= -1;

        return transform.position + new Vector3(rotateRadius * Mathf.Sin(rad), 0, rotateRadius * Mathf.Cos(rad));
    }
}
