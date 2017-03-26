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

    private GameObject background;

    public GameController game;

    public Text timer;
    public Text reminder;


    private float time;
    private static float VoteTime = 15.99f;
    private bool voted;
    private bool currentlyVoting = false;

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        None
    };

    void Start () {
        game = GameObject.Find("GameController").GetComponent<GameController>();
        timer = GameObject.Find("Timer").GetComponent<Text>();
        reminder = GameObject.Find("Reminder").GetComponent<Text>();
        time = VoteTime;
        voted = false;
    }
	
	void Update () {
        if (currentlyVoting)
        {
            time -= Time.deltaTime;
            int t = (int)time + 1;
            timer.text = "" + t;
            if (time <= 0)
                currentlyVoting = false;
        }
        else
        {
            
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
        game.broadcast.cl.sendUpdateToServer(str);
    }

    // called when room is switched, room info should be received prior to this call
    public void SwitchRoom()
    {
        currentlyVoting = true;

        //TODO: only enable arrows which lead to rooms
        EnableArrows();
        time = VoteTime;
        voted = false;
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
