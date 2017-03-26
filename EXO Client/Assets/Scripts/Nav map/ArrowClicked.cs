using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowClicked : MonoBehaviour {
    public bool isEnabled;
    private NavGameController nav;

    public NavGameController.Direction direction;


	void Start () {
        nav = GameObject.Find("Nav Controller").GetComponent<NavGameController>();
    }
	

    void OnMouseDown()
    {
        // this button was clicked
        if (isEnabled)
        {
            Debug.Log("Button pressed: " + direction.ToString());
            nav.VoteForDirection(direction);
        }
    }
}
