using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character {

    /* base class for all enemies
     * 
     */

    public void Start()
    {
        Debug.Log("Enemy start");
        base.Start();
    }


    // enemy attack loop - attacks in a loop
    public void BeginAttackLoop()
    {
        StartCoroutine(AttackLoop());
    }
    private IEnumerator AttackLoop()
    {
        while (alive)
        {
            int numAbilities = abilities.Count;
            int abilityIndex = Random.Range(0, numAbilities);
            Ability ability = abilities[abilityIndex];

            //wait for prep time then cast ability
            yield return new WaitForSeconds(ability.prepTime);
            //TODO: access list of players, choose one, cast at them
            //ability.Cast()
        }
    }
}
