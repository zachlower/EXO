using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BogSlog : Ability {

	public BogSlog()
    {
        ID = 2;

        effects = new List<Effect>();
        effects.Add(new Slice(30));

        prepTime = 10.0f;


        redCost = 0;
        greenCost = 3;
        blueCost = 3;
    }
}
