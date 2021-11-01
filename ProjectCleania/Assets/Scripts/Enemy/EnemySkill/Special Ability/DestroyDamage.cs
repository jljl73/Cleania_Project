using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDamage : DamagingProperty
{
    private void OnDestroy()
    {
        print("플레이어에게 데미지 & 중독 상태 부여");
    }
}
