using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private MapInfo map;
    private int currentRoom;
    private GameObject icon;

    private Sprite[] bg;
    private GameObject background;

    string visibleLayer = "Obstacle";
    string invisibleLayer = "Default";

    private bool inCombat;

    public Point[] navPlayerSlots;
    public Point[] combatPlayerSlots;
    public Point[] combatEnemySlots;

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    };

    // used to put the characters into their pos
    public struct Point
    {
        int x, y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    // Use this for initialization
    void Start () {
        icon = GameObject.Find("YouIcon");
        inCombat = false;
        // initialize character slots
        navPlayerSlots = new Point[6];
        combatPlayerSlots = new Point[6];
        combatEnemySlots = new Point[MapInfo.MAX_MONSTER];

        // load backgrounds
        bg = new Sprite[4];
        background = GameObject.Find("Background");
        bg[0] = Resources.Load<Sprite>("Sprites/Nav Combat/Interior1");
        bg[1] = Resources.Load<Sprite>("Sprites/Nav Combat/Interior2");
        bg[2] = Resources.Load<Sprite>("Sprites/Nav Combat/Interior3");
        bg[3] = Resources.Load<Sprite>("Sprites/Nav Combat/Interior4");

        // load room information
        currentRoom = 0;
        //map = GameObject.Find("GameController").GetComponent<MapInfo>();
        map = new MapInfo();
        SwitchRoom(currentRoom);
    }
	
	// Update is called once per frame
	void Update () {
        if (inCombat) return;

        // for test purpose, use arrow to move around
		if(Input.GetKeyDown(KeyCode.UpArrow) && map.rooms[currentRoom].forward != -1)
        {
            MoveToDirection(Direction.Up);
            return;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow) && map.rooms[currentRoom].backward != -1)
        {
            MoveToDirection(Direction.Down);
            return;
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow) && map.rooms[currentRoom].left != -1)
        {
            MoveToDirection(Direction.Left);
            return;
        }
        if(Input.GetKeyDown(KeyCode.RightArrow) && map.rooms[currentRoom].right != -1)
        {
            MoveToDirection(Direction.Right);
            return;
        }
	}

    public void MoveToDirection(Direction dir)
    {
        // switch rooms accordingly
        Vector3 pos = icon.GetComponent<Transform>().position;
        switch (dir)
        {
            case Direction.Up:
                SwitchRoom(map.rooms[currentRoom].forward);
                currentRoom = map.rooms[currentRoom].forward;
                icon.GetComponent<Transform>().position = pos + new Vector3(0, 0.8f, 0);
                break;
            case Direction.Down:
                SwitchRoom(map.rooms[currentRoom].backward);
                currentRoom = map.rooms[currentRoom].backward;
                icon.GetComponent<Transform>().position = pos + new Vector3(0, -0.8f, 0);
                break;
            case Direction.Left:
                SwitchRoom(map.rooms[currentRoom].left);
                currentRoom = map.rooms[currentRoom].left;
                icon.GetComponent<Transform>().position = pos + new Vector3(-0.8f, 0, 0);
                break;
            case Direction.Right:
                SwitchRoom(map.rooms[currentRoom].right);
                currentRoom = map.rooms[currentRoom].right;
                icon.GetComponent<Transform>().position = pos + new Vector3(0.8f, 0, 0);
                break;
        }
    }

    void SwitchRoom(int index)
    {
        //change background
        int current = map.rooms[index].bgIndex;
        int prev = map.rooms[currentRoom].bgIndex;
        if (current != prev)
        {
            background.GetComponent<SpriteRenderer>().sprite = bg[current];
            
            if (current == 1)
            {
                background.GetComponent<Transform>().position.Set(0, 0, 0);
                Vector3 scale = new Vector3(2.29f, 1.73f, 1);
                background.GetComponent<Transform>().localScale = scale;
            }
            else if(current == 2)
            {
                background.GetComponent<Transform>().position.Set(0, 1.5f, 0);
                Vector3 scale = new Vector3(2.29f, 1.73f, 1);
                background.GetComponent<Transform>().localScale = scale;
            }
            else if(current == 3)
            {
                background.GetComponent<Transform>().position.Set(0, 1.5f, 0);
                Vector3 scale = new Vector3(4.58f, 3.46f, 1);
                background.GetComponent<Transform>().localScale = scale;
            }
            else
            {
                background.GetComponent<Transform>().position.Set(0, 0, 0);
                Vector3 scale = new Vector3(1.53f, 1.43f, 1);
                background.GetComponent<Transform>().localScale = scale;
            }
        }

        // display monsters, if there are any
        if (map.rooms[index].hasMonsters)
        {
            inCombat = true;
            for(int i = 0; i < map.rooms[index].monsters.Length; i++)
            {
                if (map.rooms[index].monsters[i].hp > 0)
                {
                    GameObject.Find("Monster" + i).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(map.rooms[index].monsters[i].sprite);
                    GameObject.Find("Monster" + i).GetComponent<SpriteRenderer>().sortingLayerName = visibleLayer;
                }
            }
        }
        // else, hide the monster objects
        else
        {
            inCombat = false;
            for(int i = 0; i < MapInfo.MAX_MONSTER; i++)
            {
                GameObject.Find("Monster" + i).GetComponent<SpriteRenderer>().sortingLayerName = invisibleLayer;
            }
        }
    }

    // called when room is cleared
    void EnemyCleared()
    {
        inCombat = false;
        map.rooms[currentRoom].hasMonsters = false;
        // hide the cleared monsters
        for (int i = 0; i < MapInfo.MAX_MONSTER; i++)
        {
            GameObject.Find("Monster" + i).GetComponent<SpriteRenderer>().sortingLayerName = invisibleLayer;
        }
    }

    // called when enemy takes damage (potentially for healing as well)
    void EnemyTakeDamage(int index, int dmg)
    {
        map.rooms[currentRoom].monsters[index].hp -= dmg;
        if(map.rooms[currentRoom].monsters[index].hp <= 0)
        {
            GameObject.Find("Monster" + map.rooms[currentRoom].monsters[index].hp).GetComponent<SpriteRenderer>().sortingLayerName = invisibleLayer;
            // TODO
            // tell the client this is dead
        }
    }

    void PlayerTakeDamage(int index, int dmg)
    {
        // TODO
        // update the player status on client
    }
}
