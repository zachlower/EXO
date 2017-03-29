using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : Effect {

	public Poison(float bp, int dur)
    {
        duration = dur;
        basePower = bp;
        effectType = CombatGlobals.EffectType.Poison;
    }
}
