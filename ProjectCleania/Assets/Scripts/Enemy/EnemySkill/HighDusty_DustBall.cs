using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighDusty_DustBall : MonoBehaviour
{
    public GameObject owner;
    public float DamageScale;

    public GameObject Ball;
    public GameObject Pond;
    public GameObject BallHitTargetEffect;
    public SkillEffectController BallHitGroundEffect;

    AbilityStatus playerAbility;
    AbilityStatus ownerAbility;
    bool isBall = true;

    void OnTriggerEnter(Collider other)
    {
        ownerAbility = owner.GetComponent<Enemy>().abilityStatus;
        if (other.CompareTag("Player"))
            playerAbility = other.GetComponent<Player>().abilityStatus;

        if (isBall)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                playerAbility.AttackedBy(ownerAbility, DamageScale);

                // BallHitTargetEffect.gameObject.transform.position = other.ClosestPoint(other.transform.position);
                // BallHitTargetEffect.PlaySkillEffect();
                Instantiate(BallHitTargetEffect, other.ClosestPoint(other.transform.position), other.transform.rotation);

                Destroy(gameObject);
            }
            else if (other.gameObject.CompareTag("Ground"))
            {
                Ball.SetActive(false);

                BallHitGroundEffect.PlaySkillEffect();

                Pond.SetActive(true);
                GetComponent<Rigidbody>().isKinematic = true;
                //GetComponent<Rigidbody>().useGravity = false;

                isBall = false;
                Destroy(gameObject, 5.0f);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!isBall)
            if (other.gameObject.CompareTag("Player"))
            {
                playerAbility.AttackedBy(ownerAbility, Time.deltaTime * DamageScale);
            }
    }
}
