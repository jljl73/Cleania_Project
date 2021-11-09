using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : DamagingProperty
{
    SphereCollider triggerCollider;
    public float ColliderEnableTime = 1;

    [SerializeField]
    SkillEffectController bombObjectController;

    [SerializeField]
    SkillEffectController bombEffectController;

    private void Awake()
    {
        triggerCollider = GetComponent<SphereCollider>();
        if (triggerCollider == null)
            throw new System.Exception("ContactOnceDamage doesnt have collider");

        Invoke("EnableCollider", ColliderEnableTime);
    }

    private void Start()
    {
        bombEffectController.Scale = damageRange;
        triggerCollider.radius = damageRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isSetUp) return;

        if (other.CompareTag("Player"))
        {
            AbilityStatus playerAbil = other.gameObject.GetComponent<AbilityStatus>();
            if (playerAbil != null)
                playerAbil.AttackedBy(ownerAbility, damageScale);

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
