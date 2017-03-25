using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {

    public GameController game;
    public Dictionary<int, Player> players;
    public Dictionary<int, Enemy> enemies;


    private void Start()
    {
        
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

    }

    

}
