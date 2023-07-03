using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using TMPro;


public class UPDClient : MonoBehaviour
{
    UdpClient Client;
    public TMP_Text Debug_Text;


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
        if (x < 100 && x % 10 == 0){
            Debug_Text.text += "Number of packets: " + x + "\n";
        }


        if (x == 100){
            watch.Stop();
            Debug_Text.text += "Message: " + Encoding.UTF8.GetString(received) + "\n";
            Debug_Text.text += "Number of packets: " + y + "\n";
            Debug_Text.text += "Time elapsed: " + watch.ElapsedMilliseconds;
        }

        Encoding.UTF8.GetString(received);
        Client.BeginReceive(new AsyncCallback(recv), null);
    }
}
