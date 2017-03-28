using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character {

    /* base class for all enemies
     * 
     */
    public float warmUp;
    public Ability abilityToUse;
    public Dictionary<int, Player> players = new Dictionary<int, Player>();
    public List<int> pIDs = new List<int>();
    public void constructpIDs(Dictionary<int, Player> p) {
        players = p;
        foreach (var pl in players) {
            pIDs.Add(pl.Key);
        }
    }

    public void TickAttack()
    {
        if(abilityToUse == null) //first attack, select something to start with
        {
            selectAttack();
        }

        warmUp -= Time.deltaTime;
        if (warmUp <= 0.0f)
        {
            int playerTarget = Random.Range(0, pIDs.Count);
            playerTarget = pIDs[playerTarget];

            float powerModifier = Random.Range(0, 1.0f);
            Cast(abilityToUse, players[playerTarget], powerModifier);
            //TODO: use combat manager / game controller to target appropriate player

            selectAttack();
        }
    }



    public void selectAttack() {
        int abilityIndex = Random.Range(0, abilities.Count);
        abilityToUse = abilities[abilityIndex];
        warmUp = abilityToUse.prepTime;
    }
}
