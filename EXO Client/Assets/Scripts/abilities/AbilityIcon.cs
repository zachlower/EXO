﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityIcon : MonoBehaviour {

    public int ID;
    public Sprite sprite;


    private AbilityController ac;
    private ColorPixel cp;


    private void Start()
    {
        ac = GameObject.Find("AbilityController").GetComponent<AbilityController>();
        cp = GameObject.Find("Drawable").GetComponent<ColorPixel>();
    }

    private void OnMouseDown() //icon is clicked
    {
        Debug.Log("clicked ability " + ID);
        ac.SelectAbility(ID);
        cp.InitAbility(sprite.texture);
    }
}
