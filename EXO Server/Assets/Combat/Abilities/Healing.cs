using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : Ability {

    public Healing()
    {
        ID = 7;

        effects.Add(new Stab(-50.0f));

        target = CombatGlobals.TargetType.Ally;
    }

}
