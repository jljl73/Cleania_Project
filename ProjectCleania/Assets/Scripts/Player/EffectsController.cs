using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour
{
    // ¿Ã∆Â∆Æ ª¿‘ ∏ÆΩ∫∆Æ
    [SerializeField]
    List<SkillEffectController> effectList = new List<SkillEffectController>();

    // ID->List µÒº≈≥ ∏Æ
    Dictionary<int, SkillEffectController> effectDict = new Dictionary<int, SkillEffectController>();

    void Start()
    {
        for (int i = 0; i < effectList.Count; i++)
        {
            if (!effectDict.ContainsKey(effectList[i].Id))
                effectDict.Add(effectList[i].Id, effectList[i]);
        }

        DoPortalEffect();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            DoPortalEffect();
            print("Pressed G!");
        }
    }

    public void DoPortalEffect()
    {
        if (effectDict.ContainsKey(1102))
            effectDict[1102].PlaySkillEffect();
        else
            print("no 1102 key!");
    }

    public void DoLevelUpEffect()
    {
        if (effectDict.ContainsKey(1123))
            effectList[1123].PlaySkillEffect();
    }
}
