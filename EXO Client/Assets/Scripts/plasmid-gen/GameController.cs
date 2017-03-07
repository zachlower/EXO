using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int collectLimit = 5;

    private int redCollect = 0;
    private int greenCollect = 0;
    private int blueCollect = 0;

    
	public enum PlasmidType
    {
        Red,
        Green,
        Blue
    };

    public void Collect(GameObject plasmid)
    {
        if(redCollect + greenCollect + blueCollect < collectLimit) //limit not yet reached
        {
            PlasmidType type = plasmid.GetComponent<PickupController>().type;
            Debug.Log("COLLECTED " + type.ToString());
            switch (type)
            {
                case PlasmidType.Red:
                    redCollect++;
                    break;
                case PlasmidType.Green:
                    greenCollect++;
                    break;
                case PlasmidType.Blue:
                    blueCollect++;
                    break;
            }

            Destroy(plasmid);
        }
    }
}
