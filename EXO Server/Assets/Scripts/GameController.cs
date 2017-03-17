using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    // Map, rooms etc
    private MapInfo map;
    private int currentRoom;
        // votes index 0->3: up down left right
    private int [] directionVotes;
    private int directionVotesCount;
    private Direction direction;

    private GameObject text;
    private float textTimer = 3.0f;

    // communication shit
    public ServerListener serverListener;
    public Dictionary<int, Character> characters;
    private int nextCharacterID;
    public List<int> playerIDs;

    
    public CombatManager combatManager;

    private Sprite[] bg;
    private GameObject background;
    private GameObject icon;
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
        Right,
        None
    };

    // State Machine
    public enum State
    {
        WaitingConnection,
        Navigation,
        Combat,
    };

    // used to put the characters into their pos
    public struct Point
    {
        public float x, y;
        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }


    // Use this for initialization
    void Start () {
        nextCharacterID = 0;
        serverListener = GameObject.Find("ServerManager").GetComponent<ServerListener>();
        characters = new Dictionary<int, Character>();
        playerIDs = new List<int>();
        combatManager = GameObject.Find("CombatManager").GetComponent<CombatManager>();

        directionVotes = new int[4];
        directionVotesCount = 0;
        icon = GameObject.Find("YouIcon");
        text = GameObject.Find("Text");
        inCombat = false;
        // initialize character slots
        navPlayerSlots = new Point[6];
        combatPlayerSlots = new Point[6];
        combatEnemySlots = new Point[MapInfo.MAX_MONSTER];
        
        navPlayerSlots[0] = new Point(-7.5f, -2.0f);
        navPlayerSlots[1] = new Point(-5.0f, -2.0f);
        navPlayerSlots[2] = new Point(-1.5f, -1.0f);
        navPlayerSlots[3] = new Point(1.5f, -2.5f);
        navPlayerSlots[4] = new Point(4.5f, -1.5f);
        navPlayerSlots[5] = new Point(8.0f, -1.5f);

        combatPlayerSlots[0] = new Point(-13.5f, -1.5f);
        combatPlayerSlots[1] = new Point(-11.0f, -2.5f);
        combatPlayerSlots[2] = new Point(-8.5f, -1.0f);
        combatPlayerSlots[3] = new Point(-6.0f, -2.0f);
        combatPlayerSlots[4] = new Point(-3.5f, -2.5f);
        combatPlayerSlots[5] = new Point(-1.0f, -1.5f);

        /*combatEnemySlots[0] = new Point(6.0f, -1.0f);
        combatEnemySlots[1] = new Point(7.0f, -2.5f);
        combatEnemySlots[2] = new Point(10.0f, -1.5f);*/

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
        textTimer -= Time.deltaTime;
        if(textTimer <= 0.0f)
        {
            text.GetComponent<Text>().text = "";
        }
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
        // clear the votes
        directionVotesCount = 0;
        for(int i = 0; i < 4; i++)
        {
            directionVotes[i] = 0;
        }
        // switch rooms accordingly
        textTimer = 3.0f;
        Vector3 pos = icon.GetComponent<Transform>().position;
        switch (dir)
        {
            case Direction.None:
                // TODO
                // update the client (copy this to below)
                text.GetComponent<Text>().text = "You stayed in this room";
                break;
            case Direction.Up:
                text.GetComponent<Text>().text = "You moved forward";
                SwitchRoom(map.rooms[currentRoom].forward);
                currentRoom = map.rooms[currentRoom].forward;
                icon.GetComponent<Transform>().position = pos + new Vector3(0, 0.8f, 0);
                break;
            case Direction.Down:
                text.GetComponent<Text>().text = "You moved backward";
                SwitchRoom(map.rooms[currentRoom].backward);
                currentRoom = map.rooms[currentRoom].backward;
                icon.GetComponent<Transform>().position = pos + new Vector3(0, -0.8f, 0);
                break;
            case Direction.Left:
                text.GetComponent<Text>().text = "You moved left";
                SwitchRoom(map.rooms[currentRoom].left);
                currentRoom = map.rooms[currentRoom].left;
                icon.GetComponent<Transform>().position = pos + new Vector3(-0.8f, 0, 0);
                break;
            case Direction.Right:
                text.GetComponent<Text>().text = "You moved right";
                SwitchRoom(map.rooms[currentRoom].right);
                currentRoom = map.rooms[currentRoom].right;
                icon.GetComponent<Transform>().position = pos + new Vector3(0.8f, 0, 0);
                break;
        }
    }

    // TODO
    // need to be updated based on the latest design 
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
                Vector3 scale = new Vector3(2.35f, 1.73f, 1);
                background.GetComponent<Transform>().localScale = scale;
            }
            else if(current == 2)
            {
                background.GetComponent<Transform>().position.Set(0, 1.5f, 0);
                Vector3 scale = new Vector3(2.35f, 1.73f, 1);
                background.GetComponent<Transform>().localScale = scale;
            }
            else if(current == 3)
            {
                background.GetComponent<Transform>().position.Set(0, 1.5f, 0);
                Vector3 scale = new Vector3(4.65f, 3.46f, 1);
                background.GetComponent<Transform>().localScale = scale;
            }
            else
            {
                background.GetComponent<Transform>().position.Set(0, 1.5f, 0);
                Vector3 scale = new Vector3(1.56f, 1.5f, 1);
                background.GetComponent<Transform>().localScale = scale;
            }
        }

        /*// display monsters, if there are any
        if (map.rooms[index].enemyCount > 0)
        {
            inCombat = true;
            // display monsters
            for(int i = 0; i < map.rooms[index].monsters.Length; i++)
            {
                if (map.rooms[index].monsters[i].hp > 0)
                {
                    GameObject.Find("Monster" + i).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(map.rooms[index].monsters[i].sprite);
                    GameObject.Find("Monster" + i).GetComponent<SpriteRenderer>().sortingLayerName = visibleLayer;
                }
            }
            // move players to combat formation
            for(int i = 0; i < 6; i++)
            {
                GameObject.Find("Player" + i).GetComponent<Transform>().position = new Vector3(combatPlayerSlots[i].x, combatPlayerSlots[i].y, 0.0f);
            }
            // update the reminder
            //text.GetComponent<Text>().text += "\r\n";
            text.GetComponent<Text>().text += "\r\n"+"MONSTER!";

            // update the clients
        }
        // else, hide the monster objects
        else
        {
            inCombat = false;
            // hide monsters
            for(int i = 0; i < MapInfo.MAX_MONSTER; i++)
            {
                GameObject.Find("Monster" + i).GetComponent<SpriteRenderer>().sortingLayerName = invisibleLayer;
            }

            // move players to combat formation
            for (int i = 0; i < 6; i++)
            {
                GameObject.Find("Player" + i).GetComponent<Transform>().position = new Vector3(navPlayerSlots[i].x, navPlayerSlots[i].y, 0.0f);
            }

        }*/
    }

    // called when enemy takes damage (potentially for healing as well)
    void EnemyTakeDamage(int characterIndex, int dmg)
    {
        map.rooms[currentRoom].monsters[characterIndex].hp -= dmg;
        if(map.rooms[currentRoom].monsters[characterIndex].hp <= 0)
        {
            GameObject.Find("Monster" + map.rooms[currentRoom].monsters[characterIndex].hp).GetComponent<SpriteRenderer>().sortingLayerName = invisibleLayer;
            // TODO
            // tell the client this is dead

            // see how many left
            map.rooms[currentRoom].enemyCount -= 1;
            if(map.rooms[currentRoom].enemyCount <= 0)
            {
                // EnemyCleared(); obsolete
                CombatEnded();
                // TODO 
                // update clients?

            }
        }
    }

    // called by combat manager for healing and damage dealt to player
    void PlayerTakeDamage(int playerIndex, int dmg)
    {
        // TODO
        // update the player status on client
    }

    public void SendPlasmid(int playerIndex, string plasmids)
    {
        string str = "Plasmid:" + plasmids;
        // get the corresponding socket and send the plasmids
    }

    public void ActivateAbility(string ability, string target, List<int> indices)
    {
        // Probably of the wrong parameters
    }

    public void VoteDirection(Direction dir)
    {
        switch (dir)
        {
            case Direction.None:
                break;
            case Direction.Up:
                directionVotes[0] += 1;
                break;
            case Direction.Down:
                directionVotes[1] += 1;
                break;
            case Direction.Left:
                directionVotes[2] += 1;
                break;
            case Direction.Right:
                directionVotes[3] += 1;
                break;
        }
        // if everybody voted
        directionVotesCount += 1;
        if(directionVotesCount == playerIDs.Count)
        {
            // find the voted direction
            int max = 0;
            for(int i = 0; i < 4; i++)
            {
                if (max < directionVotes[i])
                    max = directionVotes[i];
            }
            // stay here
            if (max == 0)
                direction = Direction.None;
            else
            {
                direction = Direction.None;
                if (max == directionVotes[0])
                {
                    direction = Direction.Up;
                }
                if (max == directionVotes[1])
                {
                    if (direction == Direction.None)
                        direction = Direction.Down;
                    else
                        direction = Direction.None;
                }
                if (max == directionVotes[2])
                {
                    if (direction == Direction.None)
                        direction = Direction.Left;
                    else
                        direction = Direction.None;
                }
                if (max == directionVotes[3])
                {
                    if (direction == Direction.None)
                        direction = Direction.Right;
                    else
                        direction = Direction.None;
                }
            }
            MoveToDirection(direction);
        }
    }

    public void CombatStarted()
    {
        // TODO
        // create the enemy characters and add into the chracter map
    }

    public void CombatEnded()
    {
        // TODO
        // Show the loot etc

        // tell the clients

        // switch back to navigation formation
    }
}
