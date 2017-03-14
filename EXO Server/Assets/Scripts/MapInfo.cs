using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour {
    // Room Info
    public static int MAX_MONSTER = 3;
    public Room[] rooms;
    public class Room
    {
        public int forward = -1, backward = -1, left = -1, right = -1, bgIndex = 0;
        public Monster[] monsters;
        public int enemyCount = 0;
        public struct Monster
        {
            public int hp;
            public string name;
            public string sprite;
            public Monster(int hp, string name, string sprite)
            {
                this.hp = hp;
                this.name = name;
                this.sprite = sprite;
            }
        }
    }

    public MapInfo()
    {
        rooms = new Room[5];
        for (int i = 0; i < 5; ++i)
        {
            rooms[i] = new Room();
            rooms[i].monsters = new Room.Monster[MAX_MONSTER];
        }
        // directions
        rooms[0].forward = 1;
        rooms[1].forward = 3;
        rooms[1].backward = 0;
        rooms[1].right = 2;
        rooms[2].left = 1;
        rooms[3].forward = 4;
        rooms[3].backward = 1;
        rooms[4].backward = 3;
        // background
        rooms[0].bgIndex = 0;
        rooms[1].bgIndex = 1;
        rooms[2].bgIndex = 2;
        rooms[3].bgIndex = 3;
        rooms[4].bgIndex = 2;
        // TODO
        // put in the monsters
        rooms[2].enemyCount = 2;
        rooms[4].enemyCount = 3;
        rooms[2].monsters[0] = new Room.Monster(10, "Shell Shock", "Sprites/Nav Combat/Shell_Shock");
        rooms[2].monsters[1] = new Room.Monster(10, "Shell Shock", "Sprites/Nav Combat/Shell_Shock");
        rooms[4].monsters[2] = new Room.Monster(10, "Shell Shock", "Sprites/Nav Combat/Shell_Shock");
        rooms[4].monsters[0] = new Room.Monster(10, "Shell Shock", "Sprites/Nav Combat/Shell_Shock");
        rooms[4].monsters[1] = new Room.Monster(20, "Chtlig", "Sprites/Nav Combat/Chtlig");
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
