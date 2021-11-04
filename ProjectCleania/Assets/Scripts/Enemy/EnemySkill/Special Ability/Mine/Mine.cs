using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : DamagingProperty
{
    Collider triggerCollider;
    public float ColliderEnableTime = 1;

    [SerializeField]
    SkillEffectController bombObjectController;

    [SerializeField]
    SkillEffectController bombEffectController;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider>();
        if (triggerCollider == null)
            throw new System.Exception("ContactOnceDamage doesnt have collider");

        Invoke("EnableCollider", ColliderEnableTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("���� ����!!");
            //AbilityStatus playerAbil = other.gameObject.GetComponent<AbilityStatus>();
            //if (playerAbil != null)
            //    playerAbil.AttackedBy(OwnerAbility, DamageScale);
            bombEffectController.PlaySkillEffect();
            bombObjectController.StopSKillEffect();

            EnableCollider(false);

            Destroy(this.gameObject, 2f);
        }
    }

    void EnableCollider(bool value)
    {
        triggerCollider.enabled = value;
    }
}
