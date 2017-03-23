﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConnectionManager : MonoBehaviour {

    public GameController game;
    public ServerBroadcast sb;
    int playersJoined = 0;
    int playersSelectedChars = 0;
    public Dictionary<int,Character> characters;
    bool listDirty = true;

    public Text butText;
    public Slider lengthSlider;
    public Button startButton;
    bool textCounting = false;
    int conTextCounter = 0;
    public GameObject[] playerIcons = new GameObject[6];

    void Start() {
        foreach (GameObject i in playerIcons) { i.SetActive(false); }
    }

    // Update is called once per frame
    void Update() {
        if (!textCounting) {
            textCounting = true;
            Invoke("updateConText", .5f);
        }
        if (listDirty) {
            if (playersJoined >= 3)
            {
                if (playersSelectedChars == playersJoined)
                {
                    startButton.enabled = true;
                }
            }
            else startButton.enabled = false;

            //update the visual bit
            int charSlot = 0;
            foreach (var c in characters) {
                string charText = "Player " + c.Key + ": ";
                if (c.Value != null) charText += "Some Zany Alien";
                else charText += "No Wacky Aliens";
                playerIcons[charSlot].SetActive(true);
                playerIcons[charSlot].GetComponentInChildren<Text>().text = charText;
                charSlot++;
            }
            for (int i = charSlot; i < 6; i++) {
                playerIcons[i].SetActive(false);
            }
            
        }
    }

    void updateConText() {
        string ConText = "Listening";
        for (int i = 0; i < conTextCounter; i++)
        {
            ConText += ".";
        }
        conTextCounter++;
        if (conTextCounter > 5) conTextCounter = 0;
        butText.text = ConText;
        textCounting = false;
    }

    public void addPlayer(int cID) {
        if (!characters.ContainsKey(cID))
        {
            characters.Add(cID, null);
            playersJoined = characters.Count;
        }
        else Debug.Log("Error adding player ID: Already in Dictionary");
    }

    public void updateCharacter(int cID, Character ch) {
        if (characters.ContainsKey(cID))
        {
            if (characters[cID] == null) playersSelectedChars++;
            characters[cID] = ch;
        }
        else Debug.Log("Error assigning Character: No such player");
    }

    public void startGame() {
        game.characters = characters;
    }
}
