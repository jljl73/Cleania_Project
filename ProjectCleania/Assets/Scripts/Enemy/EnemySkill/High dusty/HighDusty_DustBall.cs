using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighDusty_DustBall : MonoBehaviour
{
    //public GameObject owner;
    public GameObject Owner { get; set; }
    public float DamageScale;

    public GameObject Ball;
    public GameObject Pond;
    public SkillEffectController BallHitTargetEffect;
    public SkillEffectController BallHitGroundEffect;

    AbilityStatus playerAbility;
    AbilityStatus ownerAbility;
    bool isBall = true;

    bool isGoingToBeDestroyed = false;

    void OnTriggerEnter(Collider other)
    {
        if (isGoingToBeDestroyed) return;

        ownerAbility = Owner.GetComponent<Enemy>().abilityStatus;
        if (Owner == null)
            throw new System.Exception("HighDusty_DustBall Owner is null");
        if (other.CompareTag("Player"))
            playerAbility = other.GetComponent<Player>().abilityStatus;

        if (isBall)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                playerAbility.AttackedBy(ownerAbility, DamageScale);

                // BallHitTargetEffect.gameObject.transform.position = other.ClosestPoint(other.transform.position);
                // BallHitTargetEffect.PlaySkillEffect();

                Ball.SetActive(false);
                Pond.SetActive(false);

                BallHitTargetEffect.PlaySkillEffect();

                Destroy(gameObject, 1);
                isGoingToBeDestroyed = true;
            }
            else if (other.gameObject.CompareTag("Ground"))
            {
                Ball.SetActive(false);

                BallHitGroundEffect.PlaySkillEffect();

                Pond.SetActive(true);
                Pond.GetComponent<SkillEffectController>().PlaySkillEffect();
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
