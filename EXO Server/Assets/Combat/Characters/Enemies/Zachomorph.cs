using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zachomorph : Enemy {

    /* example of a character 
     */

    private void Awake()
    {
        maxHealth = 100;
        currentHealth = maxHealth; //TODO: carry this over from previous encounters

        currentEffects = new List<Effect>();

        abilities = new List<Ability>();
        abilities.Add(new ZachAttack());


    }

    private void Start()
    {
        Debug.Log("Zachomorph start");
        base.Start();
    }
}
