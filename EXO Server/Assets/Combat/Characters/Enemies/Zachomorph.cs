using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zachomorph : Enemy {

    /* example of a enemy 
     */


    private void Awake()
    {
        maxHealth = 100;
        currentHealth = maxHealth;

        currentEffects = new List<Effect>();

        abilities = new List<Ability>();
        abilities.Add(new ZachAttack());

        ID = 1;
    }

}
