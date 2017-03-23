using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour {
    GameController game;
    bool isEnabled;
	// Use this for initialization
	void Start () {
        isEnabled = true;
        print(gameObject.name);
        gameObject.GetComponentInChildren<Text>().text = "Select";
        gameObject.GetComponentInChildren<Text>().alignment = TextAnchor.MiddleCenter;
        
        game = GameObject.Find("GameController").GetComponent<GameController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Disable()
    {
        isEnabled = false;
    }

    public void TaskOnClick()
    {
        if (isEnabled)
        {
            int index = int.Parse(gameObject.name.Substring(6));
            game.SelectCharacter(index);
        }
    }
}
