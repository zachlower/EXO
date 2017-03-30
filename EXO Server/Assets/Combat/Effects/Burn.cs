using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : Effect {

	public Burn(float bp, int t, float td)
    {
        basePower = bp;
        ticks = t;
        tickDuration = td;
        effectType = CombatGlobals.EffectType.Bleed;
    }
}
