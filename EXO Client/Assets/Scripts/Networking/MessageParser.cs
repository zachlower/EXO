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
                int b = int.Parse(messageBits[1]);
                game.SwitchToNav();
                game.EnterRoom(b);
                break;
            case "combat":
                game.BeginCombat();
                break;
            case "trap":
                if(messageBits[1].Equals("encountered"))
                    game.SwitchToTrap();
                else if(messageBits[1].Equals("failed"))
                {
                    game.trapButton.hintText.text = "You FAILED!";
                }
                else if (messageBits[1].Equals("solved"))
                {
                    game.trapButton.hintText.text = "You deactivate the trap";
                }
                break;
            case "players": //acquire list of players and their character IDs
                Dictionary<int, int> playerCharacters = new Dictionary<int, int>();
                for(int i=1; i<messageBits.Length-1; i += 2)
                {
                    int playerID = int.Parse(messageBits[i]);
                    int charID = int.Parse(messageBits[i + 1]);
                    Debug.Log("setting client " + playerID + " as " + charID);
                    playerCharacters.Add(playerID, charID);
                }
                game.SetPlayers(playerCharacters);
                break;
            case "playernames":
                Dictionary<int, string> playerNames = new Dictionary<int, string>();
                for(int i=1; i<messageBits.Length-1; i += 2)
                {
                    int playerID = int.Parse(messageBits[i]);
                    string name = messageBits[i + 1];
                    playerNames.Add(playerID, name);
                }
                game.SetNames(playerNames);
                break;
            case "enemies": //acquire list of enemies and their character IDs
                Dictionary<int, int> enemyCharacters = new Dictionary<int, int>();
                for(int i=1; i<messageBits.Length-1; i += 2)
                {
                    int enemyID = int.Parse(messageBits[i]);
                    int charID = int.Parse(messageBits[i + 1]);
                    enemyCharacters.Add(enemyID, charID);
                }
                game.SetEnemies(enemyCharacters);
                break;
            case "plasmid": //receive plasmids
                int red = int.Parse(messageBits[1]);
                int green = int.Parse(messageBits[2]);
                int blue = int.Parse(messageBits[3]);
                game.ReceivePlasmid(red, green, blue);
                break;
            case "dead": //character has died
                int clientID = int.Parse(messageBits[1]);
                game.CharacterDead(clientID);
                break;
            case "end": //game over
                bool victory = bool.Parse(messageBits[1]);
                game.EndGame(victory);
                break;
        }
    }
}
