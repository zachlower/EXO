using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character {

    /* base class for all enemies
     * 
     */

    public GameObject tempTarget;


    // enemy attack loop - attacks in a loop
    public void BeginAttackLoop()
    {
        StartCoroutine(AttackLoop());
    }
    private IEnumerator AttackLoop()
    {
        while (alive)
        {
            Debug.Log("enemy starting to attack");
            int numAbilities = abilities.Count;
            int abilityIndex = Random.Range(0, numAbilities);
            Ability ability = abilities[abilityIndex];

            //wait for prep time then cast ability
            yield return new WaitForSeconds(ability.prepTime);
            //TODO: access list of players, choose one, cast at them
            // also a better system for power modifier
            float powerModifier = Random.Range(0, 1.0f);
            Cast(ability, tempTarget, powerModifier);
        }
    }
}
