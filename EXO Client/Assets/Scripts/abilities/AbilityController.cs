using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityController : MonoBehaviour {

    public Text scoreText;
    public GameObject enemyIcon;
    public GameObject iconParent;
    public GameObject abilityIcon;

    public Text redText;
    public Text greenText;
    public Text blueText;
    public Text abilityInfoText;

    public int currentAbility;


    private ColorPixel colorPixel;
    private GameController game;
    private Dictionary<int, Libraries.Character> enemies = new Dictionary<int, Libraries.Character>();
    private Dictionary<int, Libraries.Ability> abilities = new Dictionary<int, Libraries.Ability>();

    //plasmids in inventory - red, green, blue
    private int[] plasmids = new int[3] { 0, 0, 0 };


    private void Awake()
    {
        colorPixel = GameObject.Find("Drawable").GetComponent<ColorPixel>();
        game = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public void BeginCombat(Dictionary<int, Libraries.Character> e)
    {
        for (int i = 0; i < plasmids.Length; i++) plasmids[i] = 0; //clear current plasmids

        enemies = e;

        //populate list of abilities
        int abilityID = game.myCharacter.ability1ID;
        if (abilityID != 0) abilities.Add(abilityID, game.libraries.abilities[abilityID]);
        abilityID = game.myCharacter.ability2ID;
        if (abilityID != 0) abilities.Add(abilityID, game.libraries.abilities[abilityID]);
        abilityID = game.myCharacter.ability3ID;
        if (abilityID != 0) abilities.Add(abilityID, game.libraries.abilities[abilityID]);

        //spawn character icons (just enemies for now)
        float topY = 2;
        float bottomY = -4;

        int enemyCount = enemies.Count;
        int placementIndex = 1;
        foreach (int id in enemies.Keys)
        {
            GameObject icon = Instantiate(enemyIcon, iconParent.transform);
            icon.GetComponent<SpriteRenderer>().sprite = enemies[id].sprite;
            icon.GetComponent<EnemyIcon>().ID = id;
            float yCoord = bottomY + placementIndex * (topY - bottomY) / (enemyCount + 1);
            icon.transform.position = new Vector3(6.5f, yCoord, -5);
            placementIndex++;
        }

        //spawn ability icons
        int abilityCount = abilities.Count;
        placementIndex = 1;
        foreach (int id in abilities.Keys)
        {
            GameObject icon = Instantiate(abilityIcon, iconParent.transform);
            icon.GetComponent<SpriteRenderer>().sprite = abilities[id].symbol;
            icon.GetComponent<AbilityIcon>().ID = id;
            icon.GetComponent<AbilityIcon>().sprite = abilities[id].symbol;
            float yCoord = bottomY + placementIndex * (topY - bottomY) / (abilityCount + 1);
            icon.transform.position = new Vector3(-6.5f, yCoord, -5);
            placementIndex++;
        }
    }


    public void SwitchToPlasmid()
    {
        game.SwitchToPlasmid();
    }

    public void SelectAbility(int abilityID)
    {
        currentAbility = abilityID;

        //set info text
        Libraries.Ability ability = game.libraries.abilities[abilityID];
        string infoString = ability.name + ": " + ability.description + '\n';
        infoString += ability.redCost + " red, " + ability.greenCost + " green, " + ability.blueCost + " blue";
        abilityInfoText.text = infoString;
    }
    public void CastAbility(int targetID, float powerModifier)
    {
        if(currentAbility != 0) //make sure an ability is selected
        {
            Libraries.Ability ability = abilities[currentAbility];
            if (plasmids[0] >= ability.redCost && plasmids[1] >= ability.greenCost && plasmids[2] >= ability.blueCost) //check costs
            {
                //subtract costs
                plasmids[0] -= ability.redCost;
                plasmids[1] -= ability.greenCost;
                plasmids[2] -= ability.blueCost;

                game.CastAbility(targetID, currentAbility, powerModifier); //cast ability

                UpdatePlasmidText();
            }
        }
    }

    public void ReceivePlasmids(int red, int green, int blue)
    {
        //receive plasmids from a fellow player (called from game controller)
        plasmids[0] += red;
        plasmids[1] += green;
        plasmids[2] += blue;

        UpdatePlasmidText();
    }

    private void UpdatePlasmidText()
    {
        redText.text = plasmids[0].ToString();
        greenText.text = plasmids[1].ToString();
        blueText.text = plasmids[2].ToString();
    }
}