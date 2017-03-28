﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character {

    /* base class for characters 
    * 
    */


    // character health
    public float currentHealth { get; protected set; }
    public float maxHealth { get; protected set; }
    private Slider healthSlider;
    public GameObject sceneObj;
    public string spriteName;

    public bool Alive = true; //default value
    public bool alive {
        get
        {
            return Alive;
        }
        protected set
        {
            Alive = value;
        }
    } 

    // durational effects (have impact each turn until they expire)
    public List<Effect> currentEffects = new List<Effect>();
    // abilities that a character has
    public List<Ability> abilities = new List<Ability>();

    public int ID { get; protected set; } //library ID of character for clients



    public void Cast(Ability a, Character target, float powerModifier)
    {
        foreach (Effect e in a.effects) //apply each effect to target
        {
            //TODO: adjust powerModifier of effect? 
            target.ApplyEffect(e, powerModifier);
        }
    }

    // TODO: when applying effects, take into account defenses against various attack types and effect types
    // apply effect when character gets targeted by an ability
    public void ApplyEffect(Effect e, float powerModifier)
    {
        Debug.Log("effects are being applied: " + e.GetType());
        //effectText.ShowEffect(e.GetType().ToString());
        switch (e.effectType)
        {
            case CombatGlobals.EffectType.Damage:
                Damage(e.basePower * powerModifier);
                break;
            case CombatGlobals.EffectType.Heal:
                Damage(-e.basePower * powerModifier);
                break;
            case CombatGlobals.EffectType.Bleed:
                Effect bleed = new Effect(e); //durational effect, add to list
                currentEffects.Add(bleed);
                break;
            case CombatGlobals.EffectType.Poison:
                Effect poison = new Effect(e); //durational effect, add to list
                currentEffects.Add(poison);
                break;
        }
    }


    // apply durational effects
    public void TickDurationalEffects()
    {
        foreach(Effect e in currentEffects)
        {
            e.duration -= Time.deltaTime;
            if (e.duration <= 0.0f)
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
                e.ticks--;
                e.duration = e.tickDuration;
                if (e.ticks <= 0) {
                    currentEffects.Remove(e);
                }
            }
        }
    }



    // damage or heal character
    private void Damage(float amount)
    {
        currentHealth -= amount;
        Debug.Log(this.GetType() + " health: " + currentHealth);
        if (currentHealth > maxHealth) //character healed to full health, cap
            currentHealth = maxHealth;
        else if (currentHealth <= 0) //character killed
            Kill();
    }

    // when health drops below 0, character dies :(
    private void Kill() 
    {
        if (sceneObj != null) Object.Destroy(sceneObj);
        alive = false;
        Debug.Log(this.GetType() + " has perished.");
    }
    
}
