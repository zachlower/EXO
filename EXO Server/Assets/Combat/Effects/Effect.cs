using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect {

    /* base class for all effects
     * these can either be held by abilities or applied to characters
     * 
     */
    
    public CombatGlobals.EffectType effectType;
    public CombatGlobals.AttackType attackType;
    public float basePower; //applies in different ways to different effect types
    public int ticks;
    public float duration;
    //const duration for each tick
    //total effect time = ticks*tickduration
    public float tickDuration;

    public Effect() { }
    public Effect(Effect orig)
    {
        effectType = orig.effectType;
        attackType = orig.attackType;
        basePower = orig.basePower;
        duration = orig.duration;
    }
}
