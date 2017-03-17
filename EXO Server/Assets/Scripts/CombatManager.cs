using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {
    //public GameController game;
    public Dictionary<int, Player> players;
    public Dictionary<int, Enemy> enemies;

    /* temporary test code */
    public GameObject tempPlayer;
    public GameObject tempEnemy;

    private void Start()
    {
        initCombat(null, null);
    }
    public void initCombat(Dictionary<int, Player> pl, Dictionary<int, Enemy> enemies) {

        //create the combat picture (background, character locations, etc)
        //send versions of these dictionaries with pertinent information to the clients 

        /* temporary test code */
        players = new Dictionary<int, Player>();
        players.Add(0, tempPlayer.GetComponent<Player>());
        enemies = new Dictionary<int, Enemy>();
        enemies.Add(0, tempEnemy.GetComponent<Enemy>());

        enemies[0].BeginAttackLoop();
        players[0].BeginDurationalEffectTrigger(1.0f);
    }

    

}
