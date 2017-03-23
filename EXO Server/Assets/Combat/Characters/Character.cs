﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour {

    /* base class for characters 
    * 
    */


    // character health
    public float currentHealth { get; protected set; }
    public float maxHealth { get; protected set; }
    private Slider healthSlider;

    private EffectText effectText;

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
    public List<Effect> currentEffects;
    // abilities that a character has
    public List<Ability> abilities;


    private void Start()
    {
        healthSlider = GetComponentInChildren<Slider>();
        effectText = GetComponentInChildren<EffectText>();
    }
    private void Update()
    {
        healthSlider.value = currentHealth / maxHealth;
    }

    public void Cast(Ability a, GameObject target, float powerModifier)
    {
        //power modifier based on score when drawing ability
        Character targetCharacter = target.GetComponent<Character>();
        foreach (Effect e in a.effects) //apply each effect to target
        {
            //TODO: adjust powerModifier of effect? 
            targetCharacter.ApplyEffect(e, powerModifier);
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


    // trigger durational effects every interval (TODO: interval?)
    public void BeginDurationalEffectTrigger(float interval)
    {
        StartCoroutine(DurationalEffectTrigger(interval));
    }
    private IEnumerator DurationalEffectTrigger(float interval)
    {
        while (alive)
        {
            yield return new WaitForSeconds(interval);
            if (currentEffects != null && currentEffects.Count != 0)
                DurationalEffects(currentEffects);
        }
    }
    // apply durational effects
    private void DurationalEffects(List<Effect> effects)
    {
        foreach(Effect e in effects)
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
        }
        currentEffects.RemoveAll(x => x.duration <= 0);
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
        //TODO: i dunno
        alive = false;
        Debug.Log(this.GetType() + " has perished.");
    }
    
}