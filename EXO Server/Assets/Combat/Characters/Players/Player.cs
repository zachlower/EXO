using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {


	public static Player CreatePlayerClass(int charID)
    {
        switch (charID)
        {
            case 2:
                return new Boggle();
            default:
                return null;
        }
    }
}
