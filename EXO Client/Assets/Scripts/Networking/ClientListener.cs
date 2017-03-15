using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;
using UnityEngine.Networking;
public class ClientListener : MonoBehaviour
{
    bool messSent = false;
    static Socket listener = null;
    String[] eof = { "$$EOF$$" };
    public MessageParser parser;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    public void connectToServer(string ip)
    {
        IPAddress ipAddress = IPAddress.Parse(ip);
        print(ipAddress.ToString());
        IPEndPoint endPoint = new IPEndPoint(ipAddress, 25565);

        listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

        listener.Connect(endPoint);
    }

    // Update is called once per frame
    void Update()
    {
        if (listener != null)
        {
            byte[] bytes = new Byte[1024];
            string[] data;


            int avail = listener.Available;
            if (avail != 0)
            {
                string tempData = "";
                while (avail != 0)
                {
                    bytes = new Byte[1024];
                    int receivedBytes = listener.Receive(bytes);
                    tempData += Encoding.ASCII.GetString(bytes, 0, receivedBytes);
                    avail -= receivedBytes;
                }
                data = tempData.Split(eof,StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < data.Length; i++) {
                    print(data[i]+"\n");
                    parser.parseUpdate(data[i]);
                }
            }

        }

    }


    public void sendUpdateToServer(string mes)
    {
        byte[] clMes = Encoding.ASCII.GetBytes(mes+eof[0]);
        listener.Send(clMes);
    }
}
