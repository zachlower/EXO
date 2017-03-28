using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boggle : Player {

	/* example of a player
     */

    public Boggle()
    {
        maxHealth = 100;
        currentHealth = maxHealth;

        currentEffects = new List<Effect>();

        abilities = new List<Ability>();
        abilities.Add(new BogSlog());

        spriteName = "Turtle1";

        ID = 2;
    }
}
