using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stab : Effect {

	public Stab(float bp)
    {
        basePower = bp;

        effectType = CombatGlobals.EffectType.Damage;
    }
}
