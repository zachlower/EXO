using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;
using UnityEngine.Networking;
public class ServerListener : MonoBehaviour
{
    bool messSent = false;
    Socket listener = null;
    Socket sock = null;
    // Use this for initialization
    void Start()
    {
        try
        {
            print("Hey");
            IPAddress ipAddress = IPAddress.Loopback;
            IPEndPoint endPoint = new IPEndPoint(ipAddress, 25565);

            listener = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(endPoint);
            listener.Listen(100);
            sock = listener.Accept();
            if (sock != null) print("HEY");
        }
        catch (Exception e)
        {
            print(e.Message);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (sock.Connected)
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

            print("Received: " + data);
            if (!messSent)
            {
                byte[] msg = Encoding.ASCII.GetBytes("Here's YOUR message, asshole");
                sock.Send(msg);
                messSent = true;
            }

        }
    }
}
