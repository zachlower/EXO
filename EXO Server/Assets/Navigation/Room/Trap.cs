using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trap : MonoBehaviour {
    public float time = 5.0f;
    public float damage = 15.0f;
    public bool isActive = false;
    public bool isEnded = false;
    public float endTime = 2.0f;
    public int currentDefuse = 0;
    public int targetDefuse;
    public GameObject TrapText;
    public Text text;
    public GameController game;
	// Use this for initialization
	void Start () {
        game = GameObject.Find("GameController").GetComponent<GameController>();
        TrapText = GameObject.Find("TrapText");
        text = TrapText.GetComponent<Text>();
        text.text = "QUICK!" + '\n' + "DEACTIVATE THE TRAP!";
        targetDefuse = game.playerChars.Count;
	}

    public void Activate()
    {
        TrapText.SetActive(true);
        isActive = true;
    }

    public void Deactivate()
    {
        TrapText.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        // timer for trap effect trigger
        if (isActive) {
            time -= Time.deltaTime;
            if (time <= 0.0f)
            {
                effectTriggered();
            }
        }
        // timer for update game into nav phase
        if (isEnded)
        {
            endTime -= Time.deltaTime;
            if (endTime <= 0.0f)
            {
                isEnded = false;
                game.enterRoom();
            }
        }
        // if not ended yet, check if defused
        else
        {
            if (currentDefuse >= targetDefuse)
            {
                isEnded = true;
                text.text = "Congrats! You deactivated the trap!";
                Deactivate();
            }
        }
	}

    void effectTriggered()
    {
        isActive = false;
        text.text = "BOOM!";
        foreach (Player p in game.playerChars.Values)
        {
            p.Damage(damage);
        }
        isEnded = true;
    }
}
