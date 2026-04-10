using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectInstance
{
    [Tooltip("Applicable to scaling effects (value of X)")]
    public int Value = 1;
    [Tooltip("Duration measured in rounds")]
    public int Duration = 1;
    [Tooltip("Used to account for variables originating from the Effect")]
    public Effect Context;

    public EffectInstance(Effect effect, int duration=1, int value=1)
    {
        Context = effect;
        Duration = duration;
        Value = value;
    }
}
