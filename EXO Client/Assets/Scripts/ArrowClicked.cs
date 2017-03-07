using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowClicked : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        // TODO
        // this object was clicked - signal the client the chosen direction
        string str = gameObject.name.Substring(6);
        Debug.Log("Voting ");
    }
}
