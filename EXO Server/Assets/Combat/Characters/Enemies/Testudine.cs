using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testudine : Enemy {

	public Testudine()
    {
        ID = 4;

        maxHealth = 60;
        currentHealth = maxHealth;

        abilities.Add(new Boot());

        spriteName = "Testudine";
    }
}
