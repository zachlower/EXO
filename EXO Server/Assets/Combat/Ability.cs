using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability {

    /* base class for all character abilities
     * 
     */

    public List<Effect> effects;



    public void Cast(GameObject target, float powerModifier)
    {
        //power modifier based on score when drawing ability
        Character targetCharacter = target.GetComponent<Character>();
        foreach(Effect e in effects) //apply each effect to target
        {
            //TODO: adjust powerModifier of effect? 
            targetCharacter.ApplyEffect(e, powerModifier); 
        }
    }
}
