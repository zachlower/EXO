using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMap : MonoBehaviour {

    public GameController gameController;

    public GameObject navFolder;
    public NavGameController navController;

    public GameObject plasmidFolder;
    public PlasmidController plasmidController;

    public GameObject abilityFolder;
    public AbilityController abilityController;


    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        gameController.controllerMap = this;
    }
}
