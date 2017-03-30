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
        Trap
    };
    public GameState state;
    
    public ServerListener serverListener;
    public Dictionary<int, Player> playerChars;
    private int nextCharacterID;

    public GameObject[] enemyTransforms;
    public GameObject[] playerCombatTransforms;

    public CombatManager combatManager;
    public AudioManager audioManager;

    // Map, rooms etc
    private MapInfo map;
    private Room currentRoom;
    public Image background;
    public Text endText;

    public Trap trap;

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
        string temp = "playernames:";
        foreach (var p in playerChars)
        {
            temp += (p.Key + ":" + p.Value.name+":");
        }
        serverListener.sendMessageToAllClients(temp);
        serverListener.sendMessageToAllClients("startgame");
        map = new MapInfo(MapInfo.MapSize.MAP_SMALL);
        currentRoom = map.startRoom;
        
        StartCoroutine(startGameCoroutine());
        // Instantiate the players here
        int positionIndex = 0;
        foreach (var p in playerChars)
        {
            p.Value.Instantiate(playerCombatTransforms[positionIndex].transform);
            positionIndex++;
        }
    }

    private IEnumerator startGameCoroutine()
    {
        Debug.Log("About to start game");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("starting game");

        

        enterRoom();
    }

    public void enterRoom() {
        background.sprite = currentRoom.background; //set background
        background.color = Color.white;

        //check room type
        if(currentRoom == map.endRoom)
        {
            //entered last room, end game
            EndGame(true);
        }
        else if (currentRoom is NavRoom)
        {
            startNav();
        }
        else if (currentRoom is CombatRoom)
        {
            if (!((CombatRoom)currentRoom).hasFought) CombatStarted();
            else startNav();
        }
        else if (currentRoom is TrapRoom)
        {
            if (!((TrapRoom)currentRoom).hasTriggered)
            {
                trap = ((TrapRoom)currentRoom).trap;
                TrapActivated();
            }
            else
            {
                trap.Deactivate();
                startNav();
            }
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

    public void TrapActivated()
    {
        state = GameState.Trap;
        ((TrapRoom)currentRoom).trap.Activate();
        serverListener.sendMessageToAllClients("trap");
    }

    public void CombatStarted() {
        state = GameState.Combat;

        audioManager.EnterCombat();

        //initialize combat stuff
        combatManager = gameObject.AddComponent<CombatManager>();
        combatManager.initCombat(playerChars,((CombatRoom)currentRoom).enemies,this);

        /* send lists of players and enemies to clients 
         * "players:clientID:characterID:..."
         */
        string playerString = "players:";
        foreach (int key in playerChars.Keys)
        {
            playerString = playerString + key + ":" + playerChars[key].ID + ":"; //TODO: currently sending client ID, will also need to send character ID
        }
        serverListener.sendMessageToAllClients(playerString);
        string enemiesString = "enemies:";
        foreach(int key in combatManager.enemies.Keys) //key is character ID of enemy
        {
            enemiesString = enemiesString + key + ":" + combatManager.enemies[key].ID + ":";
        }
        serverListener.sendMessageToAllClients(enemiesString);

        //inform clients of combat
        serverListener.sendMessageToAllClients("combat");
    }

    public void CombatEnded()
    {
        audioManager.ExitCombat();

        Destroy(combatManager);
        ((CombatRoom)currentRoom).hasFought = true;
        serverListener.sendMessageToAllClients("no more combat");
        enterRoom();
    }

    public void SendPlasmids(int allyID, int red, int green, int blue)
    {
        //instigated by client message, send plasmids to ally
        string mes = "plasmid:" + red + ":" + green + ":" + blue;
        audioManager.PlasmidSent();
        serverListener.sendMessageToClient(mes, allyID);
    }
    public void CastAbility(int casterID, int targetID, int abilityID, float powerModifier) //receive call from client to cast an ability
    {
        //instigated by player, cast ability (can target either player or enemy)
        combatManager.PlayerCast(casterID, targetID, abilityID, powerModifier);
    }

    public void CharacterDead(int clientID)
    {
        if (playerChars.ContainsKey(clientID))
        {
            //player died
            //  playerChars.Remove(clientID);
            playerChars[clientID].alive = false;
        }
        

        //TODO: inform clients of dead character
        serverListener.sendMessageToAllClients("dead:" + clientID);
    }



    public void EndGame(bool victory)
    {
        string endMes = "end:";
        endMes += victory ? "True" : "False";
        serverListener.sendMessageToAllClients(endMes);

        StartCoroutine(EndText(victory));
    }
    private IEnumerator EndText(bool victory)
    {
        string endString = victory ? "VICTORY\nYou have completed the level." : "FAILURE\nAll players have died.";
        endText.text = endString;

        yield return new WaitForSeconds(5);
        Application.Quit();
    }
}
