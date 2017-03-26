using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconController : MonoBehaviour {

    public int ID = 0;

    private PlasmidController pc;


    private void Start()
    {
        pc = GameObject.Find("GameController").GetComponent<PlasmidController>();
    }

    private void OnMouseDown()
    {
        if (pc.gameState == PlasmidController.GameState.Sending)
        {
            pc.SendPlasmids(ID);
        }
    }
}
