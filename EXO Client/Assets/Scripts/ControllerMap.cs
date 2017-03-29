using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMap : MonoBehaviour {

    public GameController gameController;


    public NavGameController navController;
    public GameObject navGO;
    public GameObject navCanvas;

    public PlasmidController plasmidController;
    public GameObject plasmidGO;
    public GameObject plasmidCanvas;

    public AbilityController abilityController;
    public GameObject abilityGO;
    public GameObject abilityCanvas;


    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        gameController.controllerMap = this;
    }
}
