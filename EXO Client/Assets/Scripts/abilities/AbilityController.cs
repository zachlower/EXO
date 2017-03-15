using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityController : MonoBehaviour {

    public Text scoreText;

    private ColorPixel colorPixel;


    private void Start()
    {
        colorPixel = GameObject.Find("Drawable").GetComponent<ColorPixel>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float percent = colorPixel.Difference();
            scoreText.text = (int)(percent * 100) + "!";
        }
    }
}
