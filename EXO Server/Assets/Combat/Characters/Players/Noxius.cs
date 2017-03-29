﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noxius : Player {

	public Noxius()
    {
        ID = 1;

        maxHealth = 120;
        currentHealth = maxHealth;

        abilities.Add(new PoisonCloud());
        abilities.Add(new ViciousBite());

        spriteName = "Noxius";
    }
}
