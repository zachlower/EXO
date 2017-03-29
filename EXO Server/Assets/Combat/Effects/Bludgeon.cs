using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bludgeon : Effect {

	public Bludgeon(float bp)
    {
        basePower = bp;
        effectType = CombatGlobals.EffectType.Damage;
    }
}
