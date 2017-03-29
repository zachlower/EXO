using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : Effect {

	public Slash(float bp)
    {
        basePower = bp;
        effectType = CombatGlobals.EffectType.Damage;
    }
}
