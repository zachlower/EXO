using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public int collectLimit = 5;
    public Text redText;
    public Text greenText;
    public Text blueText;
    public GameObject playerIcon;

    private int redCollect = 0;
    private int greenCollect = 0;
    private int blueCollect = 0;
    List<GameObject> players = new List<GameObject>();
    private ClientListener clientListener;
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
        clientListener = GameObject.Find("Client Listener").GetComponent<ClientListener>();

        //spawn player icons
        int playerCount = 3;

        float topY = 2;
        float bottomY = -4;
        for(int i=0; i<playerCount; i++)
        {
            GameObject icon = Instantiate(playerIcon);
            icon.GetComponent<IconController>().ID = i;
            float yCoord = bottomY + i * (topY - bottomY) / (playerCount - 1);
            icon.transform.position = new Vector3(6.5f, yCoord, 0);
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
        // TODO: send plasmids over network to player
        clientListener.sendUpdateToServer("sending " + redCollect + ", " + greenCollect + ", " + blueCollect + " to " + playerID);

        redCollect = greenCollect = blueCollect = 0;
        redText.text = greenText.text = blueText.text = 0.ToString();
        gameState = GameState.Collecting;
    }
}
