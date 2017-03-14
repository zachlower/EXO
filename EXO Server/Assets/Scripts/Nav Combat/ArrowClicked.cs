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
                nav.MoveToDirection(GameController.Direction.Up);
            else if (str.Equals("Down"))
                nav.MoveToDirection(GameController.Direction.Down);
            else if (str.Equals("Left"))
                nav.MoveToDirection(GameController.Direction.Left);
            else if (str.Equals("Right"))
                nav.MoveToDirection(GameController.Direction.Right);
        }
    }
}
