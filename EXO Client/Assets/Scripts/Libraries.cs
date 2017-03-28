using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Libraries {

	public struct Character
    {
        public Character(string n, string d, bool p, string s, int a1, int a2, int a3)
        {
            name = n;
            description = d;
            isPlayer = p;
            sprite = Resources.Load<Sprite>("CharacterSprites/" + s);
            ability1ID = a1;
            ability2ID = a2;
            ability3ID = a3;
        }

        public string name;
        public string description;
        public bool isPlayer;
        public Sprite sprite;

        public int ability1ID;
        public int ability2ID;
        public int ability3ID;
    }

    public struct Ability
    {
        public Ability(string n, string d, string s, int r, int g, int b)
        {
            name = n;
            description = d;
            symbol = Resources.Load<Sprite>("AbilitySprites/" + s);

            redCost = r;
            greenCost = g;
            blueCost = b;
        }
        public string name;
        public string description;
        public Sprite symbol;

        public int redCost;
        public int greenCost;
        public int blueCost;
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
            false,
            "Shell_Shock",
            1, 0, 0));

        characters.Add(2, new Character("Boggle",
            "Truly the golden boy of EXO, Boggle was birthed of the sun and fears no mortal.",
            true,
            "Turtle1",
            2, 0, 0));
    }
    private void initAbilities()
    {
        //TODO: add all abilities to ability dictionary with appropriate IDs
        abilities.Add(1, new Ability("Zach Attack",
            "Delivers a powerful slash to opponents, causing them to bleed for several turns.",
            "test",
            4,1,2));

        abilities.Add(2, new Ability("bog Slog",
            "A powerful penetrative jab.",
            "test2",
            0,3,3));
    }
}
