using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacterController : MonoBehaviour
{
    public abstract void SetStatusAilment(StatusAilment.BehaviorRestrictionType option, bool value);
    public abstract void SetStatusAilment(StatusAilment.ContinuousDamageType option);
}
