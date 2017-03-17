using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZachAttack : Ability {

	public ZachAttack()
    {
        effects = new List<Effect>();
        effects.Add(new Slice(20));
        effects.Add(new Bleed(5, 5));
    }
}
