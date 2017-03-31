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
    public Text text;
    public GameController game;
	// Use this for initialization
	void Start () {
        text = gameObject.GetComponent<Text>();
        text.text = "QUICK!" + '\n' + "DEACTIVATE THE TRAP!";
        targetDefuse = game.playerChars.Count;
	}

    public void Activate()
    {
        time = 5.0f;
        endTime = 2.0f;
        isActive = true;
        isEnded = false;
        currentDefuse = 0;
        gameObject.SetActive(true);
        ((TrapRoom)game.currentRoom).hasTriggered = true;
        
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
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
                Deactivate();
                game.enterRoom();
            }
        }
        // if not ended yet, check if defused
        else
        {
            if (currentDefuse >= targetDefuse)
            {
                isEnded = true;
                isActive = false;
                text.text = "Congrats! You deactivated the trap!";
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
