using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Libraries {

	public struct Character
    {
        public Character(string n, string d, string s, int a1, int a2, int a3)
        {
            name = n;
            description = d;
            sprite = Resources.Load<Sprite>("CharacterSprites/" + s);
            ability1ID = a1;
            ability2ID = a2;
            ability3ID = a3;
        }

        public string name;
        public string description;
        public Sprite sprite;

        public int ability1ID;
        public int ability2ID;
        public int ability3ID;
    }

    public struct Ability
    {
        public Ability(string n, string d, string s)
        {
            name = n;
            description = d;
            symbol = Resources.Load<Sprite>("AbilitySprites/" + s);
        }
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
        //TODO: add all characters to character dictionary with appropriate IDs
        characters.Add(1, new Character("Zachomorph",
            "He shoots fire his eyes and death from his fingertips. Wise men fear him.",
            "Shell_Shock",
            1, 0, 0));

        characters.Add(2, new Character("Boggle",
            "Truly the golden boy of EXO, Boggle was birthed of the sun and fears no mortal.",
            "Turtle1",
            2, 0, 0));
    }
    private void initAbilities()
    {
        //TODO: add all abilities to ability dictionary with appropriate IDs
        abilities.Add(1, new Ability("Zach Attack",
            "Delivers a powerful slash to opponents, causing them to bleed for several turns.",
            "test"));

        abilities.Add(2, new Ability("bog Slog",
            "A powerful penetrative jab.",
            "test2"));
    }
}
