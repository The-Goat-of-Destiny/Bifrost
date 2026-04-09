using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectInstance
{
    public int Counter = 1;
    public int Duration = 1;
    [Tooltip("Used to account for variables originating from the Effect")]
    public Effect Context;
}
