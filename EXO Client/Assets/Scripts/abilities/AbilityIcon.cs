using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityIcon : MonoBehaviour {

    private SpriteRenderer sr;
    private Sprite sprite;
    private ColorPixel cp;


    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sprite = sr.sprite;

        cp = GameObject.Find("Drawable").GetComponent<ColorPixel>();
    }

    private void OnMouseDown() //icon is clicked
    {
        Debug.Log("clicked the thing");
        cp.InitAbility(sprite.texture);
    }
}
