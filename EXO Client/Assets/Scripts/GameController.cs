using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public ClientBroadcast broadcast;
    public ControllerMap controllerMap;
    public Libraries libraries;
    public int clientID = 0;
    public Libraries.Character ch;

    public Dictionary<int, Libraries.Character> players;
    public Dictionary<int, Libraries.Character> enemies;
    public string characterName;

    void Start () {
        libraries = new Libraries();
        characterName = "";
        characterName += libraries.namePieces[Random.Range(0, libraries.namePieces.Length)];
        characterName += " ";
        characterName += libraries.namePieces[Random.Range(0, libraries.namePieces.Length)];
        DontDestroyOnLoad(gameObject);
    }
	

    public void Broadcast()
    {
        broadcast.Broadcast();
        SceneManager.LoadScene("character choosing", LoadSceneMode.Single);
    }


    private void DisableFolders()
    {
        controllerMap.navGO.SetActive(false);
        controllerMap.navCanvas.SetActive(false);
        controllerMap.plasmidGO.SetActive(false);
        controllerMap.plasmidCanvas.SetActive(false);
        controllerMap.abilityGO.SetActive(false);
        controllerMap.abilityCanvas.SetActive(false);
    }

    /* startup */
    public void SelectCharacter(int index)
    {
        ch = libraries.characters[index];
        broadcast.cl.sendUpdateToServer("character:" + index+":"+characterName);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("nav-map", LoadSceneMode.Single);
    }


    /* navigation */
    public void SwitchToNav()
    {
        DisableFolders();
        controllerMap.navGO.SetActive(true);
        controllerMap.navCanvas.SetActive(true);
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
        controllerMap.plasmidGO.SetActive(true);
        controllerMap.plasmidCanvas.SetActive(true);
    }
    public void SwitchToAbility()
    {
        DisableFolders();
        controllerMap.abilityGO.SetActive(true);
        controllerMap.abilityCanvas.SetActive(true);
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
            Debug.Log("client " + id + " is a " + character.name);
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
        broadcast.cl.sendUpdateToServer("plasmid:" + allyID + ":" + red + ":" + green + ":" + blue);
    }
    public void ReceivePlasmid(int red, int green, int blue)
    {
        controllerMap.abilityController.ReceivePlasmids(red, green, blue);
    }

    public void CastAbility(int targetID, int abilityID, float powerModifier)
    {
        broadcast.cl.sendUpdateToServer("ability:" + targetID + ":" + abilityID + ":" + powerModifier);
    }

    public void CharacterDead(int cID)
    {
        if (players.ContainsKey(cID))
        {
            //player died
            controllerMap.plasmidController.players.Remove(cID);
            Destroy(controllerMap.plasmidController.playerIcons[cID]);
            controllerMap.plasmidController.playerIcons.Remove(cID);
            //players.Remove(cID);
            if(cID == clientID)
            {
                //the dead player is me!
                Application.Quit();
            }
        }
        else
        {
            //enemy died
            controllerMap.abilityController.enemies.Remove(cID);
            Destroy(controllerMap.abilityController.enemyIcons[cID]);
            controllerMap.abilityController.enemyIcons.Remove(cID);
           // enemies.Remove(cID);
        }
    }


    public void EndGame(bool victory)
    {
        //TODO: this is gonna have to be wildly different
        Application.Quit();
    }
}
