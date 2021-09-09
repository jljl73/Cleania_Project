using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRay : MonoBehaviour
{
    public GameObject rayTarget;
    private RaycastHit currentHit;
    TranparentWall currentTransparentWall;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 dir = Vector3.Normalize(rayTarget.transform.position - transform.position);
        float dist = Vector3.Distance(transform.position, rayTarget.transform.position);

        Debug.DrawRay(transform.position, dir * dist, Color.red);

        if (Physics.Raycast(this.transform.position, dir, out currentHit,  dist, LayerMask.GetMask("Wall")))
        {
            // ������ ray ���� ������Ʈ�� ������ȭ

            // ���� ray ���� ������Ʈ�� ����ȭ
            currentTransparentWall = currentHit.transform.GetComponent<TranparentWall>();
            // currentTransparentWall.MakeTransparent(true);
        }
    }
}
