﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public ClientBroadcast broadcast;
    public ControllerMap controllerMap;
    public Libraries libraries;
    public int clientID = 0;

    public Dictionary<int, int> players;
    public Dictionary<int, int> enemies;


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
    public void SwitchToPlasmid()
    {
        DisableFolders();
        controllerMap.plasmidFolder.SetActive(true);
    }
    public void SwitchToAbility()
    {
        controllerMap.abilityFolder.SetActive(true);
        DisableFolders();
    }

    public void SetPlayers(Dictionary<int, int> playerCharacters)
    {
        //receive mapping of player IDs to character IDs
        players = playerCharacters;
    }
    public void SetEnemies(Dictionary<int, int> enemyCharacters)
    {
        //receive mapping of enemy IDs to character IDs
        enemies = enemyCharacters;
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
