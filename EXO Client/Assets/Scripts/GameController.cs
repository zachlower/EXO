using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public ClientBroadcast broadcast;
    public ControllerMap controllerMap;
    public Libraries libraries;
    public int clientID = 0;

    public Dictionary<int, Libraries.Character> players;
    public Dictionary<int, Libraries.Character> enemies;
    public Libraries.Character myCharacter;


    void Start () {
        libraries = new Libraries();
        DontDestroyOnLoad(gameObject);
    }
	

    public void Broadcast()
    {
        broadcast.Broadcast();
        SceneManager.LoadScene("character choosing", LoadSceneMode.Single);
    }


    private void DisableFolders()
    {
        controllerMap.navFolder.SetActive(false);
        controllerMap.plasmidFolder.SetActive(false);
        controllerMap.abilityFolder.SetActive(false);
    }

    /* startup */
    public void SelectCharacter(int index)
    {
        myCharacter = libraries.characters[index];
        broadcast.cl.sendUpdateToServer("character:" + index);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("nav-map", LoadSceneMode.Single);
    }


    /* navigation */
    public void SwitchToNav()
    {
        DisableFolders();
        controllerMap.navFolder.SetActive(true);
    }
    public void EnterRoom(char adjacent)
    {
        //pass down byte containing adjacent room information
        if (controllerMap == null)
            Debug.Log("controller map is still null");
        controllerMap.navController.SwitchRoom(adjacent); 
    }


    /* combat */
    public void BeginCombat()
    {
        SwitchToPlasmid();
        //SwitchToAbility();
        controllerMap.plasmidController.BeginCombat(players);
        controllerMap.abilityController.BeginCombat(enemies);

    }
    public void SwitchToPlasmid()
    {
        DisableFolders();
        controllerMap.plasmidFolder.SetActive(true);
    }
    public void SwitchToAbility()
    {
        DisableFolders();
        controllerMap.abilityFolder.SetActive(true);
    }

    public void SetPlayers(Dictionary<int, int> playerCharacters)
    {
        Debug.Log("GAME CONTROLLER: received " + playerCharacters.Count + " players");
        //receive mapping of player IDs to character IDs
        players = new Dictionary<int, Libraries.Character>(); //reset player list
        foreach(int id in playerCharacters.Keys)
        {
            //map client id to character with correct character id
            Libraries.Character character = libraries.characters[playerCharacters[id]];
            players.Add(id, character);
        }
    }
    public void SetEnemies(Dictionary<int, int> enemyCharacters)
    {
        //receive mapping of enemy IDs to character IDs
        enemies = new Dictionary<int, Libraries.Character>(); //reset enemy list
        foreach(int id in enemyCharacters.Keys)
        {
            //map client id to character with correct character id
            Libraries.Character character = libraries.characters[enemyCharacters[id]];
            enemies.Add(id, character);
        }
        
    }
    public void SendPlasmid(int allyID, int red, int green, int blue)
    {
        //TODO: plasmids to ally of proper ID
        broadcast.cl.sendUpdateToServer("plasmid:" + allyID + ":" + red + ":" + green + ":" + blue);
    }
    public void ReceivePlasmid(int red, int green, int blue)
    {
        controllerMap.abilityController.ReceivePlasmids(red, green, blue);
    }

    public void CastAbility(int targetID, int abilityID, float powerModifier)
    {
        //TODO: cast ability on target of appropriate ID with powerModifier
        broadcast.cl.sendUpdateToServer("ability:" + targetID + ":" + abilityID + ":" + powerModifier);
    }
}
