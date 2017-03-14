using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowClicked : MonoBehaviour {
    public bool isEnabled;
    private GameController nav;

	// Use this for initialization
	void Start () {
        nav = GameObject.Find("Nav Controller").GetComponent<GameController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        // this button was clicked
        if (isEnabled)
        {
            string str = gameObject.name.Substring(6);
            if (str.Equals("Up"))
                nav.VoteForDirection(GameController.Direction.Up);
            else if (str.Equals("Down"))
                nav.VoteForDirection(GameController.Direction.Down);
            else if (str.Equals("Left"))
                nav.VoteForDirection(GameController.Direction.Left);
            else if (str.Equals("Right"))
                nav.VoteForDirection(GameController.Direction.Right);
        }
    }
}
