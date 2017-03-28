using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class MessageParser : MonoBehaviour {
    // need to be set when nav combat scene is loaded for the first time
    public GameController game;
    public ConnectionManager con;
    string[] parseStr = { ":" };

    //pass in the string message to be parsed and the client ID of the sender!
    public void parseUpdate(string res, int cID) {
        string[] messageBits = res.Split(parseStr, StringSplitOptions.RemoveEmptyEntries);

        switch (messageBits[0]){
            case "character":
                print("Giving player " + cID + " Alein #" + int.Parse(messageBits[1]));

                int charID = int.Parse(messageBits[1]);
                con.updateCharacter(cID, Player.CreatePlayerClass(charID));

                break;
            case "direction":
                switch (messageBits[1]) {
                    case "up":
                        game.VoteDirection(GameController.Direction.Up);
                        break;
                    case "down":
                        game.VoteDirection(GameController.Direction.Down);
                        break;
                    case "left":
                        game.VoteDirection(GameController.Direction.Left);
                        break;
                    case "right":
                        game.VoteDirection(GameController.Direction.Right);
                        break;
                }
                break;
            case "plasmid": //plasmids are being sent from one player to another
                int allyID = int.Parse(messageBits[1]);
                int red = int.Parse(messageBits[2]);
                int green = int.Parse(messageBits[3]);
                int blue = int.Parse(messageBits[4]);
                game.SendPlasmids(allyID, red, green, blue);
                break;
            case "ability":
                int targetID = int.Parse(messageBits[1]);
                int abilityID = int.Parse(messageBits[2]);
                float powerModifier = float.Parse(messageBits[3]);
                game.CastAbility(cID, targetID, abilityID, powerModifier);
                break;
        }
    }

}
