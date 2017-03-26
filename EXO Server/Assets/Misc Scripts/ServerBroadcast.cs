using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ServerBroadcast : NetworkDiscovery {

	void Start () {
        broadcastPort = 25565;
        broadcastData = "exo";
        broadcastInterval = 200;
        useNetworkManager = false;
        showGUI = false;
        Initialize();
        if (!running) print(StartAsServer());
    }


    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        print(fromAddress);
       // StopBroadcast();
    }
}
