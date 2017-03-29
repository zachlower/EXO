using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavGameController : MonoBehaviour {
    // direction arrows
    public GameObject[] arrows;
    /* Left
     * Right
     * Up
     * Down
     * None
     */

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
        Left,
        Right,
        Up,
        Down,
        None
    };

    void Start () {
        game = GameObject.Find("GameController").GetComponent<GameController>();

        time = VoteTime;
        voted = false;
    }
	
	void Update () {
        if (currentlyVoting)
        {
            time -= Time.deltaTime;
            int t = (int)time;
            timer.text = "" + t;
            if (time <= 0)
            {
                currentlyVoting = false;
                if (!voted)
                {
                    VoteForDirection(Direction.None);
                }
            }
        }
	}

    public void VoteForDirection(Direction dir)
    {
        DisableArrows();
        string str = "direction:";
        string s = "You voted ";
        voted = true;

        switch (dir)
        {
            case Direction.None:
                str += "none";
                s += "to stay";
                break;
            case Direction.Up:
                str += "up";
                s += "forward";
                arrows[2].GetComponent<Image>().color = Color.red;
                break;
            case Direction.Down:
                str += "down";
                s += "backward";
                arrows[3].GetComponent<Image>().color = Color.red;
                break;
            case Direction.Left:
                str += "left";
                s += "left";
                arrows[0].GetComponent<Image>().color = Color.red;
                break;
            case Direction.Right:
                str += "right";
                s += "right";
                arrows[1].GetComponent<Image>().color = Color.red;
                break;
        }
        reminder.text = s;
        timer.text = "";
        game.broadcast.cl.sendUpdateToServer(str);
    }

    // called when room is switched, room info should be received prior to this call
    public void SwitchRoom(char adjacent)
    {
        currentlyVoting = true;

        DisableArrows();
        EnableArrows(adjacent);
        time = VoteTime;
        voted = false;
    }

    public void EnableArrows(char adjacent)
    {
        reminder.text = "Vote for direction!";
        time = 15.0f;

        for(int i = 0; i<arrows.Length; i++)
        {
            //enable the proper arrows, according to adjacency byte
            int b = (int)Mathf.Pow(2, i);
            if ((adjacent & b) == b)
            {
                arrows[i].GetComponent<ArrowClicked>().isEnabled = true;
                arrows[i].GetComponent<Image>().color = Color.white;
            }
        }
    }

    public void DisableArrows()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].GetComponent<ArrowClicked>().isEnabled = false;
            arrows[i].GetComponent<Image>().color = Color.black;
        }
    }
}
