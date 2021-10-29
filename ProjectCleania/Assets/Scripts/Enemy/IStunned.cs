using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IStunned
{
    void Stunned(bool isStunned, float stunnedTime);

    IEnumerator StunnedFor(float time);
}
