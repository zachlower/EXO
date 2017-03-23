using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character {

    /* base class for all enemies
     * 
     */


    public void Attack()
    {
        Debug.Log("enemy starting to attack");
        int numAbilities = abilities.Count;
        int abilityIndex = Random.Range(0, numAbilities);
        Ability ability = abilities[abilityIndex];

        //TODO: access list of players, choose one, cast at them
        // also a better system for power modifier
        float powerModifier = Random.Range(0, 1.0f);
        Cast(ability, null, powerModifier); 
    }
}
