using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {
    public GameController game;
    public Dictionary<int,/*player character class*/> players;
    public Dictionary<int,/*enemy class*/> enemies;

    public void initCombat(Dictionary<int,/*player class*/> pl, Dictionary<int,/*enemy class*/> enemies) {

        //create the combat picture (background, character locations, etc)
        //send versions of these dictionaries with pertinent information to the clients 
        //
    }

}
