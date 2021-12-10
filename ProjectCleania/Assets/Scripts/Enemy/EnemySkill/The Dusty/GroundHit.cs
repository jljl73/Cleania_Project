using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHit : MonoBehaviour
{
    void OnEnable()
    {
        // Invoke("DeactivateDelay", 3);
        Destroy(this.gameObject, 3);
    }

    void DeactivateDelay() => this.gameObject.SetActive(false);

    //private void OnDisable()
    //{
    //    ObjectPool.ReturnObject(ObjectPool.enumPoolObject.GroundHit, this.gameObject);
    //    CancelInvoke();
    //}
}
