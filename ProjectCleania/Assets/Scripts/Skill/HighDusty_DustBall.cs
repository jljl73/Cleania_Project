using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighDusty_DustBall : MonoBehaviour
{
    public GameObject owner;
    public float damage;

    public GameObject ball;
    public GameObject pond;

    AbilityStatus playerAbility;
    AbilityStatus ownerAbility;
    bool isBall = true;

    //private void Start()
    //{
    //    GetComponent<Rigidbody>().AddForce((transform.up + transform.forward)*100.0f);
    //}

    private void OnTriggerEnter(Collider other)
    {
        ownerAbility = owner.GetComponentInParent<AbilityStatus>();

        if (playerAbility == null && other.gameObject.CompareTag("Player"))
            playerAbility = other.gameObject.GetComponent<AbilityStatus>();

        if (isBall)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                playerAbility.AttackedBy(ownerAbility, damage);
                
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

    private void OnTriggerStay(Collider other)
    {
        if (!isBall)
            if (other.gameObject.CompareTag("Player"))
                playerAbility.AttackedBy(ownerAbility, Time.deltaTime * damage);
    }
}
