using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConnectionManager : MonoBehaviour {

    public GameController game;
    public ServerBroadcast sb;
    int playersJoined = 0;
    int playersSelectedChars = 0;
    public Dictionary<int,Player> playerChars = new Dictionary<int, Player>();
    bool listDirty = false;

    public Text butText;
    public Slider lengthSlider;
    public Button startButton;
    bool textCounting = false;
    int conTextCounter = 0;
    public GameObject[] playerIcons = new GameObject[6];
    public GameObject[] playerImages = new GameObject[6];

    public Sprite Sclera;
    public Sprite Noxius;

    void Start() {
        Sclera = Resources.Load<Sprite>("CharacterSprites/Sclera");
        Noxius = Resources.Load<Sprite>("CharacterSprites/Noxius");
        foreach (GameObject i in playerIcons) { i.SetActive(false); }
        foreach (GameObject i in playerImages) { i.SetActive(false); }
    }

    // Update is called once per frame
    void Update() {
        if (!textCounting) {
            textCounting = true;
            Invoke("updateConText", .5f);
        }
        // update game texts
        if (listDirty) {
            if (playersJoined >= 1 && playersSelectedChars == playersJoined)
            {
                startButton.enabled = true;
                startButton.GetComponentInChildren<Text>().text = "Start Game!";
            }
            else startButton.enabled = false;

            //update the visual bit
            int charSlot = 0;
            foreach (var c in playerChars) {
                string charText = "Player " + c.Key + ": ";
                if (c.Value != null)
                {
                    charText = c.Value.name;
                    string type = c.Value.GetType().ToString();
                    if (type.Equals("Sclera"))
                    {
                        playerImages[charSlot].SetActive(true);
                        playerImages[charSlot].GetComponentInChildren<Image>().sprite = Sclera;
                    }
                    else if (type.Equals("Noxius"))
                    {
                        playerImages[charSlot].SetActive(true);
                        playerImages[charSlot].GetComponentInChildren<Image>().sprite = Noxius;
                    }
                }
                else
                    charText += "Pending";

                playerIcons[charSlot].SetActive(true);
                playerIcons[charSlot].GetComponentInChildren<Text>().text = charText;
                charSlot++;
            }
            for (int i = charSlot; i < 6; i++) {
                playerIcons[i].SetActive(false);
            }
            listDirty = false;
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
        if (!playerChars.ContainsKey(cID))
        {
            playerChars.Add(cID, null);
            playersJoined = playerChars.Count;
            listDirty = true;
        }
        else Debug.Log("Error adding player ID: Already in Dictionary");
    }

    public void updateCharacter(int cID, Player ch) {
        if (playerChars.ContainsKey(cID))
        {
            if (playerChars[cID] == null) playersSelectedChars++;
            playerChars[cID] = ch;
            listDirty = true;
        }
        else Debug.Log("Error assigning Character: No such player");
    }

    public void startGame() {
        game.playerChars = playerChars;
        game.startGame();
        gameObject.SetActive(false);
    }
}
