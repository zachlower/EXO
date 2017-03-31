using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sclera : Player {

	public Sclera() : base()
    {
        ID = 3;

        maxHealth = 320;
        currentHealth = maxHealth;

        abilities.Add(new PiercingGaze());
        abilities.Add(new FistsOfFury());

        spriteName = "Sclera";
    }
}
