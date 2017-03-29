using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : Effect {

	public Burn(float bp, int dur)
    {
        duration = dur;
        basePower = bp;
        effectType = CombatGlobals.EffectType.Bleed;
    }
}
