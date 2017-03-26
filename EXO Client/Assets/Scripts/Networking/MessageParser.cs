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
            case "clientID" : //connect to server
                print("Connected! You're client " + messageBits[1]);
                game.clientID = int.Parse(messageBits[1]); //inform GameController of our ID
                break;
            case "startgame": //begin the game
                game.StartGame();
                break;
            case "nav": //enter a new room
                char b = messageBits[1][0];
                game.EnterRoom(b);
                break;
            case "players": //acquire list of players and their character IDs
                Dictionary<int, int> playerCharacters = new Dictionary<int, int>();
                for(int i=1; i<messageBits.Length; i += 2)
                {
                    int playerID = int.Parse(messageBits[i]);
                    int charID = int.Parse(messageBits[i + 1]);
                    playerCharacters.Add(playerID, charID);
                }
                game.SetPlayers(playerCharacters);
                break;
            case "enemies": //acquire list of enemies and their character IDs
                Dictionary<int, int> enemyCharacters = new Dictionary<int, int>();
                for(int i=1; i<messageBits.Length; i += 2)
                {
                    int enemyID = int.Parse(messageBits[i]);
                    int charID = int.Parse(messageBits[i + 1]);
                    enemyCharacters.Add(enemyID, charID);
                }
                game.SetEnemies(enemyCharacters);
                break;
            case "plasmid": //receive plasmids
                int allyID = int.Parse(messageBits[1]);
                int red = int.Parse(messageBits[2]);
                int green = int.Parse(messageBits[3]);
                int blue = int.Parse(messageBits[4]);
                game.ReceivePlasmid(allyID, red, green, blue);
                break;
        }
    }
}
