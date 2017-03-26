using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Libraries {

	public struct Character
    {
        public string name;
        public string description;
        public Sprite sprite;

        public int ability1ID;
        public int ability2ID;
        public int ability3ID;
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
        //TODO: add all characters to character dictionary with appropriate IDs
        //zachomorph
        Character zachomorph = new Character();
        zachomorph.name = "Zachomorph";
        zachomorph.description = "He shoots fire his eyes and death from his fingertips. Wise men fear him.";
        characters.Add(1, zachomorph);

        Character boggle = new Character();
        boggle.name = "Boggle";
        boggle.description = "Truly the golden boy of EXO, Boggle was birthed of the sun and fears no mortal.";
        characters.Add(2, boggle);
    }
    private void initAbilities()
    {
        //TODO: add all abilities to ability dictionary with appropriate IDs
        Ability zachattack = new Ability();
        zachattack.name = "Zach-Attack";
        zachattack.description = "Delivers a powerful slash to opponents, causing them to bleed for several turns.";
        abilities.Add(1, zachattack);

        Ability bogslog = new Ability();
        bogslog.name = "Bog Slog";
        bogslog.description = "A powerful penetrative jab.";
        abilities.Add(2, bogslog);
    }
}
