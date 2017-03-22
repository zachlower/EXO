using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavGameController : MonoBehaviour {
    // direction arrows
    public GameObject ArrowUp;
    public GameObject ArrowDown;
    public GameObject ArrowLeft;
    public GameObject ArrowRight;

    private Sprite[] bg;
    private GameObject background;

    public ClientListener clientListener;

    // maybe the general controller should hold the map
    public MapInfo map;

    string visibleLayer = "Obstacle";
    string invisibleLayer = "Default";

    public Text timer;
    public Text reminder;
    private float time;
    private static float VoteTime = 15.99f;
    private bool voted;

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        None
    };

    private class Room
    {
        public int forward = -1, backward = -1, left = -1, right = -1, bgIndex = 0;
    }

    // Use this for initialization
    void Start () {
        //clientListener = GameObject.Find("ClientListener").GetComponent<ClientListener>();
        timer = GameObject.Find("Timer").GetComponent<Text>();
        reminder = GameObject.Find("Reminder").GetComponent<Text>();
        time = VoteTime;
        voted = false;
        // load background
        bg = new Sprite[4];
        background = GameObject.Find("Background");
        bg[0] = Resources.Load<Sprite>("Sprites/Nav map/Interior1");
        bg[1] = Resources.Load<Sprite>("Sprites/Nav map/Interior2");
        bg[2] = Resources.Load<Sprite>("Sprites/Nav map/Interior3");
        bg[3] = Resources.Load<Sprite>("Sprites/Nav map/Interior4");
    }
	
	// Update is called once per frame
	void Update () {
        if (time >= 1.0f)
        {
            time -= Time.deltaTime;
            int t = (int)time;
            timer.text = "" + t;
        }
        else
        {
            if (!voted)
            {
                VoteForDirection(Direction.None);
                voted = true;
            }
        }
	}

    public void VoteForDirection(Direction dir)
    {
        DisableArrows();
        string str = "direction: ";
        string s = "You voted ";
        voted = true;
        time = 0.0f;
        switch (dir)
        {
            case Direction.None:
                str += "none";
                s += "to stay";
                break;
            case Direction.Up:
                str += "up";
                s += "forward";
                ArrowUp.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case Direction.Down:
                str += "down";
                s += "backward";
                ArrowDown.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case Direction.Left:
                str += "left";
                s += "left";
                ArrowLeft.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case Direction.Right:
                str += "right";
                s += "right";
                ArrowRight.GetComponent<SpriteRenderer>().color = Color.red;
                break;
        }
        reminder.text = s;
        timer.text = "";
        //clientListener.SendMessage(str);
    }

    // called when room is switched
    public void SwitchRoom(int roomIndex)
    {
        EnableArrows();
        time = VoteTime;
        voted = false;
        background.GetComponent<SpriteRenderer>().sprite = bg[map.rooms[roomIndex].bgIndex];
        if (map.rooms[roomIndex].forward == -1)
            ArrowUp.GetComponent<SpriteRenderer>().sortingLayerName = invisibleLayer;
        else
            ArrowUp.GetComponent<SpriteRenderer>().sortingLayerName = visibleLayer;
        if (map.rooms[roomIndex].backward == -1)
            ArrowDown.GetComponent<SpriteRenderer>().sortingLayerName = invisibleLayer;
        else
            ArrowDown.GetComponent<SpriteRenderer>().sortingLayerName = visibleLayer;
        if (map.rooms[roomIndex].left == -1)
            ArrowLeft.GetComponent<SpriteRenderer>().sortingLayerName = invisibleLayer;
        else
            ArrowLeft.GetComponent<SpriteRenderer>().sortingLayerName = visibleLayer;
        if (map.rooms[roomIndex].right == -1)
            ArrowRight.GetComponent<SpriteRenderer>().sortingLayerName = invisibleLayer;
        else
            ArrowRight.GetComponent<SpriteRenderer>().sortingLayerName = visibleLayer;
    }

    public void EnableArrows()
    {
        reminder.text = "Vote for direction!";
        time = 15.0f;
        ArrowUp.GetComponent<ArrowClicked>().isEnabled = true;
        ArrowDown.GetComponent<ArrowClicked>().isEnabled = true;
        ArrowLeft.GetComponent<ArrowClicked>().isEnabled = true;
        ArrowRight.GetComponent<ArrowClicked>().isEnabled = true;
        ArrowUp.GetComponent<SpriteRenderer>().color = Color.white;
        ArrowDown.GetComponent<SpriteRenderer>().color = Color.white;
        ArrowLeft.GetComponent<SpriteRenderer>().color = Color.white;
        ArrowRight.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void DisableArrows()
    {
        ArrowUp.GetComponent<ArrowClicked>().isEnabled = false;
        ArrowDown.GetComponent<ArrowClicked>().isEnabled = false;
        ArrowLeft.GetComponent<ArrowClicked>().isEnabled = false;
        ArrowRight.GetComponent<ArrowClicked>().isEnabled = false;
    }
}
