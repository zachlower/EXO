using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {

    public GameController game;
    public Dictionary<int, Player> players;
    public Dictionary<int, Enemy> enemies;
    //true for player victory, false for monster
    bool combatResult;

    public void Update() {
        tickPlayers();
        tickEnemies();
        //check for eoc (end of combat) ;)
        bool allDead = true;
        foreach (var e in enemies) {
            if (e.Value.alive) allDead = false;
        }
        if (allDead) {
            combatResult = true;
            endCombat();
        }
        else {
            allDead = true;
            foreach (var p in players) {
                if (p.Value.alive) allDead = false;
            }
            if (allDead) {
                combatResult = false;
                endCombat();
            }
        }
        //nice work! We have now checked for end of combat :-)
    }

    public void initCombat(Dictionary<int, Player> pl, List<Enemy> en, GameController g) {

        //create the combat picture (background, character locations, etc)
        //send versions of these dictionaries with pertinent information to the clients 
        players = pl;
        game = g;
        enemies = new Dictionary<int, Enemy>();
        int tentativeID = players.Count;
        foreach (Enemy e in en) {
            while (pl.ContainsKey(tentativeID)) tentativeID++;
            e.constructpIDs(pl);
            enemies.Add(tentativeID, e);
            tentativeID++; 
        }

        int positionIndex = 0;
        foreach (var e in enemies) {    
            Instantiate(e.Value.sceneObj, game.enemyTransforms[positionIndex].transform);
            positionIndex++;
        }
        positionIndex = 0;
        foreach (var p in players)
        {
            Instantiate(p.Value.sceneObj, game.playerCombatTransforms[positionIndex].transform);
            positionIndex++;
        }
    }

    public void endCombat() {
        foreach (var e in enemies) {
            if (e.Value.sceneObj != null) Destroy(e.Value.sceneObj);
        }
        foreach (var p in players) {
            if (p.Value.sceneObj != null) Destroy(p.Value.sceneObj);
        }
        game.CombatEnded();
    }


    public void tickEnemies() {
        foreach (var e in enemies) {
            e.Value.TickAttack();
            e.Value.TickDurationalEffects();
        }
    }
    public void tickPlayers() {
        foreach (var p in players) {
            p.Value.TickDurationalEffects();
        }
    }

}
