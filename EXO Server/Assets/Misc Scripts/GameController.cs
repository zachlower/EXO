using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    // State Machine
    public enum GameState
    {
        WaitingConnection,
        Navigation,
        Combat,
    };
    public GameState state;
    // good  shit
    public ServerListener serverListener;
    public Dictionary<int, Player> playerChars;
    private int nextCharacterID;

    public GameObject[] enemyTransforms;
    public GameObject[] playerCombatTransforms;

    public CombatManager combatManager;

    // Map, rooms etc
    private MapInfo map;
    private Room currentRoom;
    public Sprite background;

    /******DUNGEON GENERATION STUFF**********/
    private GameObject text;
    private float textTimer = 3.0f; 

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        None,
        DIRECTION_SIZE
    };
    public int[] directionVotes;
    int votesCast;

    void Start () {
        nextCharacterID = 0;
        playerChars = new Dictionary<int, Player>();
    }

    public void startGame() {
        serverListener.sb.StopBroadcast();
        serverListener.sendMessageToAllClients("startgame");
        map = new MapInfo(MapInfo.MapSize.MAP_SMALL);
        currentRoom = map.startRoom;
        StartCoroutine(startGameCoroutine());
    }

    private IEnumerator startGameCoroutine()
    {
        Debug.Log("About to start game");
        yield return new WaitForSeconds(4.0f);
        Debug.Log("starting game");
        enterRoom();
    }

    public void enterRoom() {
        // background = r.background;
        if (currentRoom is NavRoom)
        {
            startNav();
        }
        else if (currentRoom is CombatRoom)
        {
            if (!((CombatRoom)currentRoom).hasFought) CombatStarted();
            else startNav();
        }
    }

    public void startNav() {
        print("starting nav again");
        state = GameState.Navigation;
        votesCast = 0;
        directionVotes = new int[(int)Direction.DIRECTION_SIZE];
        string navStr = "nav:";
        byte b = map.getAdjacentRooms();
        navStr += b;
        serverListener.sendMessageToAllClients(navStr);
    }

    public void VoteDirection(Direction dir) {
        directionVotes[(int)dir]++;
        votesCast++;
        Debug.Log(votesCast + " / " + playerChars.Count + " players have voted.");
        //if all votes are in..
        if (votesCast == playerChars.Count) {
            print("all votes are in");
            Direction d = Direction.None;
            int count = 0;
            for (int i = 0; i < directionVotes.Length; i++) {
                if (directionVotes[i] > count) {
                    count = directionVotes[i];
                    d = (Direction)i;
                }
            }
            if (d != Direction.None)
            {
                //we have moved
                print("We are moving " + d.ToString());
                currentRoom = map.moveInDir(d);
                serverListener.sendMessageToAllClients("we have moved");
                enterRoom();
            }
            else {
                //we have not moved
                startNav();
                serverListener.sendMessageToAllClients("we have not moved");
            }
        }
    }

    public void CombatStarted() {
        state = GameState.Combat;

        //initialize combat stuff
        combatManager = gameObject.AddComponent<CombatManager>();
        combatManager.initCombat(playerChars,((CombatRoom)currentRoom).enemies,this);

        /* send lists of players and enemies to clients 
         * "players:clientID:characterID:..."
         */
        string playerString = "players:";
        foreach (int key in playerChars.Keys)
        {
            playerString = playerString + key + ":" + playerChars[key].ID + ":";
        }
        serverListener.sendMessageToAllClients(playerString);
        string enemiesString = "enemies:";
        foreach(int key in combatManager.enemies.Keys)
        {
            enemiesString = enemiesString + key + ":" + combatManager.enemies[key].ID;
        }
        serverListener.sendMessageToAllClients(enemiesString);

        //inform clients of combat
        serverListener.sendMessageToAllClients("combat");
    }

    public void CombatEnded()
    {
        Destroy(combatManager);
        ((CombatRoom)currentRoom).hasFought = true;
        serverListener.sendMessageToAllClients("no more combat");
        enterRoom();
    }

    public void SendPlasmids(int allyID, int red, int green, int blue)
    {
        //instigated by client message, send plasmids to ally
        string mes = "plasmid:" + red + ":" + green + ":" + blue;
        serverListener.sendMessageToClient(mes, allyID);
    }
    public void CastAbility(int casterID, int targetID, int abilityID, float powerModifier) //receive call from client to cast an ability
    {
        //instigated by player, cast ability (can target either player or enemy)
        combatManager.PlayerCast(casterID, targetID, abilityID, powerModifier);
    }
}
