using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class MessageParser : MonoBehaviour
{
    public GameController game;
    string[] parseStr = { ":" };

    //pass in the string message to be parsed and the client ID of the sender!
    public void parseUpdate(string res)
    {
        string[] messageBits = res.Split(parseStr, StringSplitOptions.RemoveEmptyEntries);

        switch (messageBits[0])
        {
            case "clientID" :
                print("Connected! You're client " + messageBits[1]);
                game.clientID = int.Parse(messageBits[1]); //inform GameController of our ID
                break;
            case "startgame":
                game.StartGame();
                break;
            case "nav":
                char b = messageBits[1][0];
                game.EnterRoom(b);
                break;
            case "plasmid":
                int allyID = int.Parse(messageBits[1]);
                int red = int.Parse(messageBits[2]);
                int green = int.Parse(messageBits[3]);
                int blue = int.Parse(messageBits[4]);
                game.ReceivePlasmid(allyID, red, green, blue);
                break;
        }
    }
}
