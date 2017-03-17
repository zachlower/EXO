using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect {

    /* base class for all effects
     * these can either be held by abilities or applied to characters
     * 
     */

    public CombatGlobals.EffectType effectType;
    public CombatGlobals.AttackType attackType;
    public float basePower; //applies in different ways to different effect types
    public int duration = 0;
}
