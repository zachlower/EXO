using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slice : Effect {

	public Slice(float bp)
    {
        effectType = CombatGlobals.EffectType.Damage;
        attackType = CombatGlobals.AttackType.Cut;
        basePower = bp;
    }
}
