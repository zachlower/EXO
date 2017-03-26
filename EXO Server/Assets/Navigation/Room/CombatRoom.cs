using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatRoom : Room {


    /* the room where the fights happen
     * 
     */


    public List<Enemy> enemies;


	public CombatRoom() : base()
    {
        enemies = GenerateEnemies();
    }


    private List<Enemy> GenerateEnemies()
    {
        //TODO: create a function that procedurally generates a list of enemies based on factors and stuff
        List<Enemy> list = new List<Enemy>();
        list.Add(new Zachomorph());

        return list;
    }

    protected override void GenerateBackground()
    {
        int bg = Random.Range(0, ImageStatics.combatRoomBG.Count);
        background = ImageStatics.combatRoomBG[bg];
    }
}
