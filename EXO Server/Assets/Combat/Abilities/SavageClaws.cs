using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavageClaws : Ability {

	public SavageClaws()
    {
        ID = 5;

        effects.Add(new Slash(20));
        effects.Add(new Bleed(10, 5, 3));

        prepTime = 12;

        target = CombatGlobals.TargetType.Enemy;

    }
}
