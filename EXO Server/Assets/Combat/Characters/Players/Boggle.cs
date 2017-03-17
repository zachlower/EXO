using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boggle : Player {

	/* example of a player
     */

    private void Awake()
    {
        maxHealth = 100;
        currentHealth = maxHealth;

        currentEffects = new List<Effect>();

        abilities = new List<Ability>();
        abilities.Add(new BogSlog());

    }
}
