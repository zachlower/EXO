using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCloud : Ability {

	public PoisonCloud()
    {
        ID = 1;

        effects.Add(new Poison(25, 4));
    }
}
