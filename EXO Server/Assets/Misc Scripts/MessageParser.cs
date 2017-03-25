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
                print("Giving player " + cID + " Alein #" + messageBits[1]);
                con.updateCharacter(cID, Player.CreatePlayerClass(Player.PlayerClass.Boggle));
                break;
        }
    }

}
