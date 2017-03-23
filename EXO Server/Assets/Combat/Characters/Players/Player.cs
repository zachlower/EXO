using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    public enum PlayerClass
    {
        Boggle
    };


	public static Player CreatePlayerClass(PlayerClass pc)
    {
        switch (pc)
        {
            case PlayerClass.Boggle:
                return new Boggle();
            default:
                return null;
        }
    }
}
