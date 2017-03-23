using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net.Sockets;
using System.Net;
using System.Text;
public class ClientBroadcast : NetworkDiscovery {
    public ClientListener cl;
	// Use this for initialization
	void Start () {
        
    }

    // Called when Button is clicked
    public void Broadcast()
    {
        broadcastPort = 25565;
        broadcastData = "exo";
        broadcastInterval = 200;
        Initialize();
        if (!running) print(StartAsClient());
    }

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        string[] temp = fromAddress.Split(":".ToCharArray());
        cl.connectToServer(temp[temp.Length - 1]);
        StopBroadcast();
       // StopBroadcast();
    }
}
