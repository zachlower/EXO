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
        characters.Add(1, new Character("Noxius",
            "",
            true,
            "Noxius",
            1, 2, 0));

        characters.Add(2, new Character("Hamate",
            "",
            false,
            "Hamate",
            5, 0, 0));

        characters.Add(3, new Character("Sclera",
            "",
            true,
            "Sclera",
            3, 4, 0));

        characters.Add(4, new Character("Testudine",
            "",
            false,
            "Testudine",
            6, 0, 0));
    }
    private void initAbilities()
    {
        //TODO: add all abilities to ability dictionary with appropriate IDs
        abilities.Add(1, new Ability("Poison Cloud",
            "A thick cloud of poisonous gas which infects an enemy and does great damage over time",
            "PoisonCloud",
            1, 4, 0));
        abilities.Add(2, new Ability("Vicious Bite",
            "Brutal gnashing teeth cut through enemy skin and cause bleeding",
            "ViciousBite",
            1, 0, 1));
        abilities.Add(3, new Ability("Piercing Gaze",
            "Demonic eyes pierce through the tough exterior of your enemy, burning their very soul",
            "PiercingGaze",
            3, 0, 0));
        abilities.Add(4, new Ability("Fists of Fury",
            "Brutal pummeling deals immense damage to any mortal foe",
            "FistsOfFury",
            0, 1, 2));
    }
}
