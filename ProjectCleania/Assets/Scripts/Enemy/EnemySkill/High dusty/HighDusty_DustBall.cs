using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighDusty_DustBall : DamagingProperty
{
    public GameObject Ball;
    public GameObject Pond;

    [SerializeField]
    SkillEffectController ballProjectile;
    [SerializeField]
    SkillEffectController ballHitTargetEffect;
    [SerializeField]
    SkillEffectController ballHitGroundEffect;
    [SerializeField]
    SkillEffectController pondEffect;

    AbilityStatus playerAbility;

    bool isBall = true;

    bool isGoingToBeDestroyed = false;

    float projectileSize;
    float pondSize;

    private void Start()
    {
        ballProjectile.Scale = projectileSize * 2f;
        ballHitTargetEffect.Scale = projectileSize;
        ballHitGroundEffect.Scale = pondSize * 0.3333f;
        pondEffect.Scale = pondSize * 0.3333f;
    }

    public void SetUp(float projectileSize, float pondSize, AbilityStatus abil, float damageScale)
    {
        this.projectileSize = projectileSize;
        this.pondSize = pondSize;

        base.SetUp(abil, damageScale);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isSetUp) return;
        if (isGoingToBeDestroyed) return;

        if (other.CompareTag("Player"))
            playerAbility = other.GetComponent<Player>().abilityStatus;

        if (isBall)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                playerAbility.AttackedBy(ownerAbility, damageScale);

                // BallHitTargetEffect.gameObject.transform.position = other.ClosestPoint(other.transform.position);
                // BallHitTargetEffect.PlaySkillEffect();

                Ball.SetActive(false);
                Pond.SetActive(false);

                ballHitTargetEffect.PlaySkillEffect();

                Destroy(gameObject, 1);
                isGoingToBeDestroyed = true;
            }
            else if (other.gameObject.CompareTag("Ground"))
            {
                Ball.SetActive(false);

                ballHitGroundEffect.PlaySkillEffect();

                Pond.SetActive(true);
                pondEffect.PlaySkillEffect();
                GetComponent<Rigidbody>().isKinematic = true;
                //GetComponent<Rigidbody>().useGravity = false;

                isBall = false;
                Destroy(gameObject, 5.0f);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!isSetUp) return;
        if (!isBall)
            if (other.gameObject.CompareTag("Player"))
            {
                playerAbility.AttackedBy(ownerAbility, Time.deltaTime * damageScale);
            }
    }
}
