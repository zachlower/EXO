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

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        IPAddress ipAddress = IPAddress.Loopback;
        IPEndPoint endPoint = new IPEndPoint(ipAddress, 25565);

        listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

        listener.Connect(endPoint);
    }

    // Update is called once per frame
    void Update()
    {
        if (listener.Connected)
        {
            bool die = false;

            byte[] bytes = new Byte[1024];
            string[] data;

            if (!messSent)
            {
                byte[] clMes = Encoding.ASCII.GetBytes("Here's your message dingus");
                listener.Send(clMes);
                messSent = true;
            }

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
                }
            }
            //if (!data.Equals("")) die = true;

        }
        /*   if (die)
           {
               listener.Shutdown(SocketShutdown.Both);
               listener.Close();
           }*/
    }


    public void sendUpdateToServer(string mes)
    {
        byte[] clMes = Encoding.ASCII.GetBytes(mes);
        listener.Send(clMes);
    }
}
