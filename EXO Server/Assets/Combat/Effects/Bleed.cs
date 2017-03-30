using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : Effect {

	public Bleed(float bp, int t, float td)
    {
        basePower = bp;
        ticks = t;
        tickDuration = td;
        effectType = CombatGlobals.EffectType.Bleed;
    }
}
