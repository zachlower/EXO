using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
