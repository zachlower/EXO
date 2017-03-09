using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconController : MonoBehaviour {

    public int ID = 0;

    private GameController gc;


    private void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void OnMouseDown()
    {
        if (gc.gameState == GameController.GameState.Sending)
        {
            gc.SendPlasmids(ID);
        }
    }
}
