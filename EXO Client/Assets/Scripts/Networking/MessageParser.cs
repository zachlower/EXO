using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
public class MessageParser : MonoBehaviour
{
    GameController game;
    void Start()
    {
        game = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public void parseUpdate(string res)
    {

    }
}
