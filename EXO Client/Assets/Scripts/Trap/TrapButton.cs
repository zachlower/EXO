using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapButton : Button {
    GameController game;
    public Text hintText;
    public bool isActive = true;
    public int currentPress = 0;
    public int pressNeeded = 15;
	// Use this for initialization
	new void Start () {
        game = GameObject.Find("GameController").GetComponent<GameController>();
        onClick.AddListener(TaskOnClick);
        hintText = GameObject.Find("HintText").GetComponent<Text>();
        hintText.text = "You encountered a trap" + '\n' + "Keep pressing the button to deactivate it";
        print("trap setup done");
    }

    // Update is called once per frame
    void Update () {
		
	}

    void TaskOnClick()
    {
        if (isActive)
        {
            currentPress++;
            if (currentPress >= pressNeeded)
            {
                isActive = false;
                currentPress = 0;
                // set the text update
                hintText.text = "You've done your part";
                game.broadcast.cl.sendUpdateToServer("trap solved");
            }
        }    
    }
}
