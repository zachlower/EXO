using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hamate : Enemy {

	public Hamate()
    {
        ID = 2;

        maxHealth = 90;
        currentHealth = maxHealth;

        abilities.Add(new SavageClaws());

        spriteName = "Hamate";
    }
}
