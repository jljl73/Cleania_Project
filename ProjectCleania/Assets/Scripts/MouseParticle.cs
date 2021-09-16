using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseParticle : ParticleBase
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // CanBeTrigger가 true가 아닐 시, 쓰레기 값 반환
            RaycastHit rayhitInfo;
            if (!CanBeTriggerd("Ground" ,out rayhitInfo)) return;

            ChangePoseWith(rayhitInfo);

            base.ChangeScale(Scale);

            ParticleObject.Play();
        }
    }

    bool CanBeTriggerd(string collideTag, out RaycastHit rayhitInfo)
    {
        bool result = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.collider.CompareTag(collideTag))
                result = true;
        }
        rayhitInfo = raycastHit;

        return result;
    }

    void ChangePoseWith(RaycastHit raycastHit)
    {
        this.transform.position = raycastHit.point + raycastHit.normal * 0.5f;
        this.transform.LookAt(raycastHit.point + raycastHit.normal * 1.5f);
    }
}