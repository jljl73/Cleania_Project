using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill4 : Skill
{
    Ray ray;
    RaycastHit hit;
    Collider attackArea;

    void Start()
    {   
        attackArea = GetComponent<Collider>();
    }

    public override void AnimationActivate()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            transform.position = new Vector3(
                hit.point.x,
                0.0f,
                hit.point.z);
        }

        attackArea.enabled = true;
        Invoke("OffSkill", 3.0f);
    }

    override public void Activate()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
        {
            transform.position = new Vector3(
                hit.point.x,
                0.0f,
                hit.point.z);
        }

        attackArea.enabled = true;
        Invoke("OffSkill", 3.0f);
    }

    void OffSkill()
    {
        attackArea.enabled = false;
    }

}
