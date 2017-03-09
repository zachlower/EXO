﻿using System.Collections;
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
            string data = "";

            if (!messSent)
            {
                byte[] clMes = Encoding.ASCII.GetBytes("Here's your message dingus");
                listener.Send(clMes);
                messSent = true;
            }
            
            int avail = listener.Available;
            while (avail != 0)
            {
                bytes = new Byte[1024];
                int receivedBytes = listener.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, receivedBytes);
                avail -= receivedBytes;
            }
            if (!data.Equals("")) die = true;
            print("Received callback: " + data);

         /*   if (die)
            {
                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
            }*/
        }
    }

    static public void sendUpdateToServer(string mes) {
        byte[] clMes = Encoding.ASCII.GetBytes(mes);
        listener.Send(clMes);
    }
}