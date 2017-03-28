using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SelectController : MonoBehaviour {

    public GameObject selectIcon;


    private GameController game;
    private Dictionary<int, GameObject> icons = new Dictionary<int, GameObject>();


    private void Start()
    {
        game = GameObject.Find("GameController").GetComponent<GameController>();

        //create select icons
        Dictionary<int, Libraries.Character> playerChars = game.libraries.characters.Where(x => x.Value.isPlayer == true).ToDictionary(x => x.Key, x => x.Value);
        int numChars = playerChars.Count;
        float leftX = -7.0f;
        float rightX = 7.0f;
        int placementIndex = 1;
        foreach(int cID in playerChars.Keys)
        {
            GameObject icon = Instantiate(selectIcon);
            icon.GetComponent<SpriteRenderer>().sprite = playerChars[cID].sprite;
            //set position
            float xPos = leftX + placementIndex * (rightX - leftX) / (numChars + 1);
            icon.transform.position = new Vector3(xPos, -1, 0);
            icon.transform.Find("Canvas/NameText").GetComponent<Text>().text = playerChars[cID].name;
            icon.transform.Find("Canvas/SelectButton").GetComponent<SelectButton>().ID = cID;
            icons.Add(cID, icon);
            placementIndex++;
        }
    }

    public void Select(int id)
    {
        game.SelectCharacter(id);
    }
}
