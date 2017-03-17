using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConnectionManager : MonoBehaviour {
    public Slider lengthSlider;
    public GameObject[] playerIcons = new GameObject[6];
    public ServerBroadcast sb;
    public Text butText;
    int pCounter = 0;
    int conTextCounter = 0;
    public List<string> playernames;
    //public Dictionary<int,/*character class*/>;
    bool textCounting = false;
    void Start() {
        foreach (GameObject i in playerIcons) { i.SetActive(false); }
    }

    // Update is called once per frame
    void Update() {
        if (!textCounting) {
            textCounting = true;
            Invoke("updateConText", .5f);
        }
        if (playernames.Count != pCounter) {
            for (int i = 0; i < 6; i++) {
                if (i > pCounter) playerIcons[i].SetActive(false);
                else {
                    playerIcons[i].SetActive(true);
                    playerIcons[i].GetComponentInChildren<Text>().text = playernames[i];
                }
            }
            pCounter = playernames.Count;
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
}
