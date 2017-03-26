using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavRoom : Room {

    public NavRoom() : base(){

    }

    protected override void GenerateBackground()
    {
        int bg = Random.Range(0, ImageStatics.navRoomBG.Count);
        background = ImageStatics.navRoomBG[bg];
    }
}
