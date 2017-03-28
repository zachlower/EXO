using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability {

    /* base class for all character abilities
     * 
     */

    public int ID;
    public List<Effect> effects = new List<Effect>();
    public float prepTime = 10.0f;

    public int redCost, greenCost, blueCost;

    
}
