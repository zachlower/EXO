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
        IPAddress ipAddress = IPAddress.Loopback;
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
        /*if (sock.Connected)
         {
             byte[] bytes = new Byte[1024];
             string data = "";
             int avail = sock.Available;
             while (avail != 0)
             {
                 bytes = new Byte[1024];
                 int receivedBytes = sock.Receive(bytes);
                 data += Encoding.ASCII.GetString(bytes, 0, receivedBytes);
                 avail -= receivedBytes;
             }

             if (!data.Equals("")) print("Received: " + data);
           /*  if (!messSent)
             {
                 byte[] msg = Encoding.ASCII.GetBytes("Here's YOUR message, asshole");
                 sock.Send(msg);
                 messSent = true;
             }//

         }*/
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
