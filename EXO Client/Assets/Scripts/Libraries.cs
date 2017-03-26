using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Libraries {

	public struct Character
    {
        public string name;
        public string description;
        public Sprite sprite;

        public Ability ability1;
        public Ability ability2;
        public Ability ability3;
    }

    public struct Ability
    {
        public string name;
        public string description;
        public Sprite symbol;
    }


    public Dictionary<int, Character> characters = new Dictionary<int, Character>();
    public Dictionary<int, Ability> abilities = new Dictionary<int, Ability>();

    


    public Libraries()
    {
        initCharacters();
        initAbilities();
    }

    private void initCharacters()
    {
        
    }
    private void initAbilities()
    {

    }
}
