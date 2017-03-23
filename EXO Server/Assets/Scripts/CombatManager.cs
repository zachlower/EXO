using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {

    //public GameController game;
    public Dictionary<int, Player> players;
    public Dictionary<int, Enemy> enemies;


    private void Start()
    {
        initCombat(null, null);
    }
    public void initCombat(Dictionary<int, Player> pl, Dictionary<int, Enemy> enemies) {

        //create the combat picture (background, character locations, etc)
        //send versions of these dictionaries with pertinent information to the clients 


    }

    

}
