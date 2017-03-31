using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Enemy : Character {

    /* base class for all enemies
     * 
     */
    public float warmUp;
    public Ability abilityToUse;
    public bool isRed = false;
    public Enemy()
    {
        abilitySoundString = "enemyAbility";
    }



    public void TickAttack()
    {
        if(abilityToUse == null) //first attack, select something to start with
        {
            selectAttack();
        }


        warmUp -= Time.deltaTime;
        // change color to red
        if(warmUp <= 1.0f && !isRed)
        {
            if (alive) sceneObj.GetComponent<SpriteRenderer>().color = Color.red;
            isRed = true;
        }

        if (warmUp <= 0.0f)
        {
            List<int> pIDs = new List<int>();
            foreach (var p in combatManager.players) {
                if (p.Value.alive) {
                    pIDs.Add(p.Key);
                }
            }
            if (pIDs.Count > 0)
            {
                int playerTarget = Random.Range(0, pIDs.Count);
                playerTarget = pIDs[playerTarget];

                float powerModifier = Random.Range(0, 1.0f);
                Cast(abilityToUse, combatManager.players[playerTarget], powerModifier);
                //TODO: use combat manager / game controller to target appropriate player
                sceneObj.GetComponent<SpriteRenderer>().color = Color.white;
                isRed = false;
                selectAttack();
            }
        }
    }

    

    public void selectAttack() {
        int abilityIndex = Random.Range(0, abilities.Count);
        abilityToUse = abilities[abilityIndex];
        warmUp = abilityToUse.prepTime;
    }
}
