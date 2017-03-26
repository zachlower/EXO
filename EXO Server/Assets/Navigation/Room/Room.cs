using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room {

    public Sprite background;


    public Room()
    {
      //  GenerateBackground();
    }


    protected virtual void GenerateBackground()
    {
        //TODO: create a function that selects a background
    }
}
