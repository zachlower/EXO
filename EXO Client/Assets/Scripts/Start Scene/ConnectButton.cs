using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectButton : Button {
    ClientBroadcast broadcaster;
    GameController game;
    // Use this for initialization
    new void Start  () {
        game = GameObject.Find("GameController").GetComponent<GameController>();
        gameObject.GetComponentInChildren<Text>().text = "Connect to Server";
        gameObject.GetComponentInChildren<Text>().alignment = TextAnchor.MiddleCenter;
        broadcaster = GameObject.Find("ClientListener").GetComponent<ClientBroadcast>();
        onClick.AddListener(TaskOnClick);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void TaskOnClick()
    {
        Debug.Log("button clicked");
        game.Broadcast();
    }
}
