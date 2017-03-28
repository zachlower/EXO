using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zachomorph : Enemy {

    /* example of a enemy 
     */


    public Zachomorph()
    {
        maxHealth = 100;
        currentHealth = maxHealth;

        currentEffects = new List<Effect>();

        abilities = new List<Ability>();
        abilities.Add(new ZachAttack());

        spriteName = "Shell_Shock";
        ID = 1;
    }

}
