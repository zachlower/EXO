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


    private void Start()
    {
        game = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public void BeginCombat(Dictionary<int, Libraries.Character> p)
    {
        Debug.Log("PLASMID CONTROLLER: received " + p.Count + " players");
        players = p;

        //create an icon for each player on the side bar
        float topY = 2;
        float bottomY = -4;

        int playerCount = players.Count;
        int placementIndex = 1;
        foreach(int id in players.Keys)
        {
            //TODO: do not spawn your own icon
            GameObject icon = Instantiate(playerIcon, iconParent.transform);
            icon.GetComponent<SpriteRenderer>().sprite = players[id].sprite;
            icon.GetComponent<IconController>().ID = id;
            float yCoord = bottomY + placementIndex * (topY - bottomY) / (playerCount + 1);
            icon.transform.position = new Vector3(6.5f, yCoord, -5);
            placementIndex++;
        }
    }

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

            Destroy(plasmid);
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
    }
}
