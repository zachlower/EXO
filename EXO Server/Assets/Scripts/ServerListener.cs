using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;
using System.Threading;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ServerListener : MonoBehaviour
{
    Socket listener = null;
    int con = 0;
    public int maxConnections;
    String[] eof = { "$$EOF$$" };
    public ServerBroadcast sb;
    public ConnectionManager conManager;
    struct client {
        public Socket socket;
    }
    public MessageParser parser;
    Dictionary<int, client> clientList = new Dictionary<int,client>();

    public struct player
    {
        string name;
        string image;
        public player(string name, string image)
        {
            this.name = name;
            this.image = image;
        }
    }
    List<player> players;

    void Start()
    {
        players = new List<player>();
        IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = null;
        foreach (IPAddress add in ipHost.AddressList) {
            if (add.AddressFamily == AddressFamily.InterNetwork) {
                ipAddress = add;
                break;
            }
        }
        print(ipAddress.ToString());
        IPEndPoint endPoint = new IPEndPoint(ipAddress, 25565);

        listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
        listener.Bind(endPoint);
        listener.Listen(100);
        Thread connectionManager = new Thread(new ThreadStart(acceptConnections));
        connectionManager.Start();
    }
    // Update is called once per frame
    void Update()
    {
        foreach (var c in clientList)
        {
            Socket s = c.Value.socket;
            if (s != null)
            {
                byte[] bytes = new Byte[1024];
                string[] data;


                int avail = s.Available;
                if (avail != 0)
                {
                    string tempData = "";
                    while (avail != 0)
                    {
                        bytes = new Byte[1024];
                        int receivedBytes = s.Receive(bytes);
                        tempData += Encoding.ASCII.GetString(bytes, 0, receivedBytes);
                        avail -= receivedBytes;
                    }
                    data = tempData.Split(eof, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < data.Length; i++)
                    {
                        print(data[i] + "\n");
                        parser.parseUpdate(data[i]);
                    }
                }

            }
        }
    }

    public void acceptConnections() {
        while (con < maxConnections)
        {
            try
            {
                Socket sock = listener.Accept();
                if (sock != null) {
                    client c = new client();
                    c.socket = sock;
                    print("HEY "+con);
                    clientList.Add(con, c);
                    byte[] joinedMes = Encoding.ASCII.GetBytes("Congratulations! You are client " + con+eof[0]);
                    c.socket.Send(joinedMes);
                    c.socket.Send(Encoding.ASCII.GetBytes("Tstin"+eof[0]));
                    con++;

                    conManager.playernames.Add("Player " + con);
                }
            }
            catch (Exception e)
            {
                print(e.Message);
            }
        }
       
        print("EVERYONE'S HERE!!");

        // load game scene
        SceneManager.LoadScene("nav combat", LoadSceneMode.Single);
        parser.game = GameObject.Find("GameController").GetComponent<GameController>();
        // pass in the player info
        //parser.game.players = players;
    }

    public void sendMessageToAllClients(string mes) {
        foreach (var c in clientList) {
            c.Value.socket.Send(Encoding.ASCII.GetBytes(mes+eof[0]));
        }
    }

    public void addPlayer(player player)
    {
        players.Add(player);
    }
}
