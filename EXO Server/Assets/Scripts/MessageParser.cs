using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class MessageParser : MonoBehaviour {
    // need to be set when nav combat scene is loaded for the first time
    public GameController game;

    public void parseUpdate(string res) {
        if (res.StartsWith("player:"))
        {
            res = res.Substring(7);
            string name = Next(res);
            string image = Next(res);
            GameObject.Find("Server Manager").GetComponent<ServerListener>().addPlayer(new ServerListener.player(name, image));
        }
        else if (res.StartsWith("plasmid:"))
        {
            res = res.Substring(8);
            int playerIndex = int.Parse(Next(res));
            game.SendPlasmid(playerIndex, res);
        }
        else if (res.StartsWith("ability:"))
        {
            res = res.Substring(8);
            string ability = Next(res);
            string target = Next(res);
            List<int> indices = new List<int>();
            while(res.Length != 0)
            {
                indices.Add(int.Parse(Next(res)));
            }
            game.ActivateAbility(ability, target, indices);
        }
        else if (res.StartsWith("direction:"))
        {
            // add one vote to the direction
            if (res.EndsWith("right"))
            {
                game.VoteDirection(GameController.Direction.Right);
            }
            else if (res.EndsWith("left"))
            {
                game.VoteDirection(GameController.Direction.Left);
            }
            else if (res.EndsWith("up"))
            {
                game.VoteDirection(GameController.Direction.Up);
            }
            else if (res.EndsWith("down"))
            {
                game.VoteDirection(GameController.Direction.Down);
            }
            else if (res.EndsWith("none"))
            {
                game.VoteDirection(GameController.Direction.None);
            }
        }
    }

    // helper func for parsing string
    private string Next(string str)
    {
        int index = str.IndexOf(",");
        string re;
        if (index == -1)
        {
            re = str;
            str = "";
            return re;
        }
        re = str.Substring(0, index - 1);
        str = str.Substring(index + 1);
        return re;
    }
}
