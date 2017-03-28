using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconController : MonoBehaviour {

    public int ID = 0;

    private PlasmidController pc;


    private void Start()
    {
        Debug.Log("icon start");
        pc = GameObject.Find("PlasmidController").GetComponent<PlasmidController>();
    }

    private void OnMouseDown()
    {
        if (pc.gameState == PlasmidController.GameState.Sending)
        {
            pc.SendPlasmids(ID);
        }
    }
}
