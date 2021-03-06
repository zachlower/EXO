﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{

    public GameController game;
    public Dictionary<int, Player> players;
    public Dictionary<int, Enemy> enemies;
    //true for player victory, false for monster
    bool combatResult;

    public void Update()
    {
        tickPlayers();
        tickEnemies();


        //check if all players are dead
        if (players.Where(x => x.Value.alive).ToList().Count <= 0)
        {
            //all players are dead - failure
            game.EndGame(false);
            gameObject.SetActive(false);
        }

        //check if all enemies are dead
        if (enemies.Where(x => x.Value.alive).ToList().Count <= 0)
        {
            //all enemies are dead - victory
            endCombat();
        }

    }

    public void initCombat(Dictionary<int, Player> pl, List<Enemy> en, GameController g)
    {

        //create the combat picture (background, character locations, etc)
        //send versions of these dictionaries with pertinent information to the clients 
        players = pl;
        game = g;
        enemies = new Dictionary<int, Enemy>();
        int tentativeID = players.Count;
        foreach (Enemy e in en)
        {
            while (pl.ContainsKey(tentativeID)) tentativeID++;
            enemies.Add(tentativeID, e);
            tentativeID++;
        }

        //create each character on screen
        int positionIndex = 0;
        foreach (var e in enemies)
        {
            e.Value.Instantiate(game.enemyTransforms[positionIndex].transform);
            e.Value.combatManager = this;
            positionIndex++;
        }

        foreach (var p in players)
        {
            p.Value.combatManager = this;
        }
    }

    public void endCombat()
    {
        foreach (var e in enemies)
        {
            if (e.Value.sceneObj != null) Destroy(e.Value.sceneObj);
        }
        game.CombatEnded();
    }


    public void tickEnemies()
    {
        foreach (var e in enemies)
        {
            if (e.Value.alive) {
                e.Value.TickAttack();
                e.Value.TickDurationalEffects();
            }
        }
    }
    public void tickPlayers()
    {
        foreach (var p in players)
        {
            if (p.Value.alive)
            {
                p.Value.TickDurationalEffects();
            }
        }
    }

    public void PlayerCast(int casterID, int targetID, int abilityID, float powerModifier)
    {
        //instigated by client

        Player caster = players[casterID];
        //use abilityID to determine which ability has been selected from player's list of abilities
        Ability ability = caster.abilities.Where(x => x.ID == abilityID).FirstOrDefault();

        //figure out target
        Character target;
        if (players.ContainsKey(targetID)) //target is player
        {
            target = players[targetID];
        }
        else //target is enemy
        {
            target = enemies[targetID];
        }

        caster.Cast(ability, target, powerModifier); //cast is sent through caster Character?
        
    }

    public void CharacterDead(Character c)
    {
        int cID;
        if (c is Enemy)
        {
            cID = enemies.FirstOrDefault(x => x.Value == (Enemy)c).Key;
            enemies[cID].alive = false;
        }
        else
        {
            cID = players.FirstOrDefault(x => x.Value == (Player)c).Key;
            players[cID].alive = false;
        }
        game.CharacterDead(cID);
    }

    
}
