using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour {
    GameController game;
    bool isEnabled;
    GameObject you;
    string invisible = "Default";
    string visible = "Obstacle";
    public Vector3 youPos;
	// Use this for initialization
	void Start () {
        isEnabled = true;
        print(gameObject.name);
        gameObject.GetComponentInChildren<Text>().text = "Select";
        gameObject.GetComponentInChildren<Text>().alignment = TextAnchor.MiddleCenter;
        you = GameObject.Find("You");
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
            you.GetComponent<SpriteRenderer>().sortingLayerName = visible;
            you.GetComponent<Transform>().position = youPos; 
            game.SelectCharacter(index);
        }
    }
}
