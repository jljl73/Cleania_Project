using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestNavAgent : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Vector3 targetPose;
    public Vector3 TargetPose { get => targetPose; private set { targetPose = value; } }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        TargetPose = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveToPosition(Input.mousePosition);
        }

        navMeshAgent.SetDestination(TargetPose);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(navMeshAgent.destination, new Vector3(1.5f, 1.5f, 1.5f));
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(TargetPose, 1.6f);
    }

    public void MoveToPosition(Vector3 position)
    {
        int layerMask = 0;
        //layerMask = 1 << 5 | 1 << 7;
        layerMask = 1 << 7;

        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 500.0f, layerMask))
        {
            if (hit.collider.tag == "Ground")
            {
                TargetPose = hit.point;
                //print("Ground Hit");
            }
            //else if (hit.collider.CompareTag("Enemy"))
            //    TargetPose = hit.collider.transform.position;
        }
    }
}
