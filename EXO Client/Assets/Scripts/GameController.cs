﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public ClientBroadcast broadcast;
    public int clientID = 0;




    void Start () {
        DontDestroyOnLoad(gameObject);
	}
	

    public void Broadcast()
    {
        broadcast.Broadcast();
        SceneManager.LoadScene("character choosing", LoadSceneMode.Single);
    }

    public void SelectCharacter(int index)
    {
        broadcast.cl.sendUpdateToServer("character:" + index);
    }

    public void SwitchToAbilityScreen()
    {
        SceneManager.LoadScene("abilities", LoadSceneMode.Single);
    }

    public void SwitchToPlasmidScreen()
    {
        SceneManager.LoadScene("plasmid-gen", LoadSceneMode.Single);
    }

    public void SwitchToNav()
    {
        SceneManager.LoadScene("nav-map", LoadSceneMode.Single);
    }
}
