using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighDusty_DustBall : MonoBehaviour
{
    public GameObject owner;
    public float DamageScale;

    public GameObject ball;
    public GameObject pond;

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
                
                Destroy(gameObject);
            }
            else if (other.gameObject.CompareTag("Ground"))
            {
                ball.SetActive(false);
                pond.SetActive(true);
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
