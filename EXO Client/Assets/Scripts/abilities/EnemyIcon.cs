using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIcon : MonoBehaviour {

    public int ID;


    private AbilityController ac;
    private ColorPixel cp;


    private void Start()
    {
        ac = GameObject.Find("AbilityController").GetComponent<AbilityController>();
        cp = GameObject.Find("Drawable").GetComponent<ColorPixel>();
    }

    private void OnMouseDown() //icon is clicked
    {
        if (ac.currentAbility != 0)
        {
            Debug.Log("clicked enemy " + ID);

            float powerModifier = cp.Difference(); //get power modifier from color pixel
            ac.CastAbility(ID, powerModifier); //cast

            cp.InitAbility(null);
        }
    }
}
