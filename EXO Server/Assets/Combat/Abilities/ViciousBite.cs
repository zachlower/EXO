using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViciousBite : Ability {

	public ViciousBite()
    {
        ID = 2;

        effects.Add(new Stab(30));
        effects.Add(new Bleed(10, 2));

        target = CombatGlobals.TargetType.Enemy;

    }
}
