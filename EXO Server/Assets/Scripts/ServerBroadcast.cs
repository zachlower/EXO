using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ServerBroadcast : NetworkDiscovery {
    public ServerListener lis;
	// Use this for initialization
	void Start () {
        broadcastPort = 25565;
        broadcastData = "exo";
        broadcastInterval = 200;
        //   print(StartAsServer());
        Initialize();
        if (!running) print(StartAsServer());
    }


    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        print(fromAddress);
        StopBroadcast();
    }
}
