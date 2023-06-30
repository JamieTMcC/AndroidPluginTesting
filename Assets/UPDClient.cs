using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UPDClient : MonoBehaviour
{
    UdpClient Client;

    int x = 0;
    int y = 0;
    System.Diagnostics.Stopwatch watch;

    // Start is called before the first frame update
    void Start()
    {
        Client = new UdpClient(2000);
        try
        {
            Client.BeginReceive(new AsyncCallback(recv), null);
            Debug.Log("Started");
            watch = new System.Diagnostics.Stopwatch();
            watch.Start();

        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }


    // Update is called once per frame
    void Update()
    {
    }

    private void recv(IAsyncResult res)
    {
        IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 2000);
        byte[] received = Client.EndReceive(res, ref RemoteIpEndPoint);
        
        if (received.Length > 10)
        {
            y++;
        }
        else{
            Debug.Log("Packet dropped");
        }
        //Process codes
        x++;
        if (x < 1000 && x % 10 == 0){
            Debug.Log("Number of packets: " + x);
        }


        if (x == 1000){
            watch.Stop();
            Debug.Log("Number of packets: " + y);
            Debug.Log("Time elapsed: " + watch.ElapsedMilliseconds);
        }

        Encoding.UTF8.GetString(received);
        Client.BeginReceive(new AsyncCallback(recv), null);
    }
}
