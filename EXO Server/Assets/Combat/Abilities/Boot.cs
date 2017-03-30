using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boot : Ability {

	public Boot()
    {
        ID = 6;

        effects.Add(new Bludgeon(40));

        prepTime = 10;

        target = CombatGlobals.TargetType.Enemy;

    }
}
