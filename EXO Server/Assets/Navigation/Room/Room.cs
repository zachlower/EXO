using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room {

    public Sprite background;


    public Room()
    {
        GenerateBackground();
    }


    protected virtual void GenerateBackground()
    {
        // select random background from list
        int bgIndex = Random.Range(0, NavigationGlobals.backgrounds.Count);
        string bgString = NavigationGlobals.backgrounds[bgIndex];
        Debug.Log("selecting index " + bgIndex + " from " + NavigationGlobals.backgrounds.Count + " backgrounds");

        // load background from resources
        background = Resources.Load<Sprite>("roomBackgrounds/" + bgString);
    }
}
