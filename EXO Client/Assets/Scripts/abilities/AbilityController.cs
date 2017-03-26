using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityController : MonoBehaviour {

    public Text scoreText;



    private ColorPixel colorPixel;
    private GameController game;

    private int[] plasmids = new int[3] { 0, 0, 0 };


    private void Start()
    {
        colorPixel = GameObject.Find("Drawable").GetComponent<ColorPixel>();
        game = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public void ReceivePlasmids(int red, int green, int blue)
    {
        //receive plasmids from a fellow player (called from game controller)
        plasmids[0] += red;
        plasmids[1] += green;
        plasmids[2] += blue;
    }
}
