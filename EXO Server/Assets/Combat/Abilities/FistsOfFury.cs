using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistsOfFury : Ability {

	public FistsOfFury()
    {
        ID = 4;

        effects.Add(new Bludgeon(50));

        target = CombatGlobals.TargetType.Enemy;
    }
}
