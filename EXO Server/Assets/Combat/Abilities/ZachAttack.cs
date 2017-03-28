using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZachAttack : Ability {

	public ZachAttack()
    {
        ID = 1;

        effects = new List<Effect>();
        effects.Add(new Slice(20));
        effects.Add(new Bleed(5, 2));

        prepTime = 5.0f;


        redCost = 4;
        greenCost = 1;
        blueCost = 2;
    }
}
