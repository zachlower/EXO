using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : Effect {

	public Bleed(float bp, int dur)
    {
        effectType = CombatGlobals.EffectType.Bleed;
        attackType = CombatGlobals.AttackType.Cut;
        basePower = bp;
        duration = dur;
    }
}
