using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    /* base class for characters 
    * 
    */


    // character health
    public int currentHealth { get; private set; }
    public int maxHealth { get; private set; }

    // durational effects (have impact each turn until they expire)
    public List<Effect> currentEffects;


    
    // TODO: when applying effects, take into account defenses against various attack types and effect types
    // apply effect when character gets targeted by an ability
    public void ApplyEffect(Effect e, float powerModifier)
    {
        switch (e.effectType)
        {
            case CombatGlobals.EffectType.Damage:
                Damage((int)(e.basePower * powerModifier));
                break;
            case CombatGlobals.EffectType.Heal:
                Damage((int)(-e.basePower * powerModifier));
                break;
            case CombatGlobals.EffectType.Bleed:
                currentEffects.Add(e); //durational effect, add to list
                break;
            case CombatGlobals.EffectType.Poison:
                currentEffects.Add(e); //durational effect, add to list
                break;
        }
    }

    // apply durational effects
    private void DurationalEffects()
    {
        foreach(Effect e in currentEffects)
        {
            switch (e.effectType) // determine what kind of durational effect
            {
                case CombatGlobals.EffectType.Bleed:
                    Damage((int)e.basePower);
                    break;
                case CombatGlobals.EffectType.Poison:
                    Damage((int)e.basePower);
                    break;
            }

            e.duration--;
            if (e.duration <= 0) // effect duration has run out
                currentEffects.Remove(e);
        }
    }



    // damage or heal character
    private void Damage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth > maxHealth) //character healed to full health, cap
            currentHealth = maxHealth;
        else if (currentHealth <= 0) //character killed
            Kill();
    }

    // when health drops below 0, character dies :(
    private void Kill() 
    {

    }
    
}
