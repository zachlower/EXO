using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRoom : Room
{

    public Trap trap;
    public bool hasTriggered = false;

    public TrapRoom() : base()
    {
        trap = new Trap();
    }
}
