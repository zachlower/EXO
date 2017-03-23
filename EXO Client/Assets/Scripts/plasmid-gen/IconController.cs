using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconController : MonoBehaviour {

    public int ID = 0;

    private PlasmidController gc;


    private void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<PlasmidController>();
    }

    private void OnMouseDown()
    {
        if (gc.gameState == PlasmidController.GameState.Sending)
        {
            gc.SendPlasmids(ID);
        }
    }
}
