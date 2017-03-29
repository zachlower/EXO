using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CombatGlobals {

	/* This class includes all the enums for various combat variables
     * 
     */

    /* types that will characterize attacks as well as character vulnerabilities */
    //TODO: currently unused
    public enum AttackType
    {
        Burn,
        Cut,
        Mechanical,
        Biological
    };

    /* durational effects on characters */
    public enum EffectType
    {
        Damage,
        Heal,
        Block,
        Bleed,
        Poison
    };
        
}
