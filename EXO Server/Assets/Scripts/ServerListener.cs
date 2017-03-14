using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;
using System.Threading;
using UnityEngine.Networking;
public class ServerListener : MonoBehaviour
{
    bool messSent = false;
    Socket listener = null;
    int con = 0;
    public int maxConnections;
    bool didthething = false;
    public static String eof = "$$EOF$$";

    struct client {
        public Socket socket;
    }
    Dictionary<int, client> clientList = new Dictionary<int,client>();

    void Start()
    {
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
        print("Coroutine executed");
    }
    // Update is called once per frame
    void Update()
    {
        
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
                    byte[] joinedMes = Encoding.ASCII.GetBytes("Congratulations! You are client " + con+eof);
                    c.socket.Send(joinedMes);
                    c.socket.Send(Encoding.ASCII.GetBytes("Tstin"+eof));
                    con++;
                }
            }
            catch (Exception e)
            {
                print(e.Message);
            }
        }
       
        print("EVERYONE'S HERE!!");
    }

}
