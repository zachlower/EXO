using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlasmidController : MonoBehaviour {

    public int collectLimit = 5;
    public Text redText;
    public Text greenText;
    public Text blueText;
    public GameObject playerIcon;
    public GameObject iconParent;

    private int redCollect = 0;
    private int greenCollect = 0;
    private int blueCollect = 0;
    PickupController[] pickups;
    Dictionary<int, Libraries.Character> players = new Dictionary<int, Libraries.Character>();
    private GameController game;
    public enum GameState
    {
        Collecting,
        Sending
    };
    public GameState gameState = GameState.Collecting;


    public enum PlasmidType
    {
        Red,
        Green,
        Blue
    };


    private void Awake()
    {
        game = GameObject.Find("GameController").GetComponent<GameController>();
        pickups = FindObjectsOfType<PickupController>();
    }

    public void BeginCombat(Dictionary<int, Libraries.Character> p)
    {
        players = p;

        //create an icon for each player on the side bar
        float topY = 2;
        float bottomY = -4;

        int playerCount = players.Count;
        int placementIndex = 1;
        foreach(int id in players.Keys)
        {
            //TODO: do not spawn your own icon (keep this way to debug)
            GameObject icon = Instantiate(playerIcon, iconParent.transform);
            icon.GetComponent<SpriteRenderer>().sprite = players[id].sprite;
            icon.GetComponent<IconController>().ID = id;
            icon.GetComponent<Text>().text = players[id].name;
            float yCoord = bottomY + placementIndex * (topY - bottomY) / (playerCount + 1);
            icon.transform.position = new Vector3(6.5f, yCoord, -5);
            placementIndex++;
        }
    }


    public void SwitchToAbility()
    {
        game.SwitchToAbility();
    }

    public void Collect(GameObject plasmid)
    {
        if(redCollect + greenCollect + blueCollect < collectLimit) //limit not yet reached
        {
            PlasmidType type = plasmid.GetComponent<PickupController>().type;
            switch (type)
            {
                case PlasmidType.Red:
                    redCollect++;
                    redText.text = redCollect.ToString();
                    break;
                case PlasmidType.Green:
                    greenCollect++;
                    greenText.text = greenCollect.ToString();
                    break;
                case PlasmidType.Blue:
                    blueCollect++;
                    blueText.text = blueCollect.ToString();
                    break;
            }

            plasmid.SetActive(false);
        }
    }
    
    public void TriggerSend()
    {
        gameState = GameState.Sending;
    }
    public void SendPlasmids(int playerID)
    {
        game.SendPlasmid(playerID, redCollect, greenCollect, blueCollect); //send plasmids through game controller

        redCollect = greenCollect = blueCollect = 0;
        redText.text = greenText.text = blueText.text = 0.ToString();

        gameState = GameState.Collecting;
        //regenerate all plasmid pickups
        foreach(PickupController pickup in pickups)
        {
            pickup.gameObject.SetActive(true);
            pickup.Regenerate();
        }
    }
}
