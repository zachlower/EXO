using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatRoom : Room {


    /* the room where the fights happen
     * 
     */


    public List<Enemy> enemies;
    public bool hasFought = false;

	public CombatRoom() : base()
    {
        enemies = GenerateEnemies();
    }


    private List<Enemy> GenerateEnemies()
    {
        //TODO: create a function that procedurally generates a list of enemies based on factors and stuff
        List<Enemy> list = new List<Enemy>();
        list.Add(new Hamate());
        list.Add(new Testudine());

        return list;
    }

}
