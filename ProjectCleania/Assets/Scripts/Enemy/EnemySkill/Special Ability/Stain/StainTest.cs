using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StainTest : MonoBehaviour
{
    [SerializeField]
    GameObject projectilePrefab;
    GameObject targetObj;

    float projArriveTime = 3;

    public void SetUp(GameObject target)
    {
        targetObj = target;
    }

    private void Start()
    {
        SetUp(GameManager.Instance.player.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (targetObj == null) return;

            GameObject obj = Instantiate(projectilePrefab, this.transform.position, Quaternion.identity);
            if (obj == null) return;

            StainProjectile stainProj = obj.GetComponent<StainProjectile>();
            if (stainProj == null) return;

            Rigidbody rigidbody = obj.GetComponent<Rigidbody>(); ;
            if (rigidbody != null)
            {
                if (targetObj == null) return;
                rigidbody.velocity = CaculateVelocity(targetObj.transform.position, transform.position, projArriveTime);
                stainProj.SetUp(2, projArriveTime * 0.5f);
            }
        }
    }

    Vector3 CaculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }
}
