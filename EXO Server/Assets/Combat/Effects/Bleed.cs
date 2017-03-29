using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : Effect {

	public Bleed(float bp, int dur)
    {
        duration = dur;
        basePower = bp;
        effectType = CombatGlobals.EffectType.Bleed;
    }
}
