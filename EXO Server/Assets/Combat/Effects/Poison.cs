using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : Effect {

	public Poison(float bp, int t, float td)
    {
        basePower = bp;
        ticks = t;
        tickDuration = td;
        effectType = CombatGlobals.EffectType.Poison;
    }
}
