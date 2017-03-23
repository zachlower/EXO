using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room {

    public NavigationGlobals.RoomType roomType;
    public Texture2D background;


    public Room()
    {
        background = GenerateBackground();
    }


    private Texture2D GenerateBackground()
    {
        //TODO: create a function that selects a background
        return null;
    }
}
