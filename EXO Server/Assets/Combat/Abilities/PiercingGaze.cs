using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingGaze : Ability {

	public PiercingGaze()
    {
        ID = 3;

        effects.Add(new Burn(25, 3, 4));

        target = CombatGlobals.TargetType.Enemy;

    }
}
