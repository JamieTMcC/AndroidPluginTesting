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
    DateTime currenttime;
    bool printed = false;
    string receivedString = "";
    public float[] gyro = new float[3];
    public float[] accel = new float[3];

    int x = 0;
    int y = -1;
    System.Diagnostics.Stopwatch watch;

    // Start is called before the first frame update
    void Start()
    {
        Client = new UdpClient(2000);
        Debug_Text.text = "";
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
        Debug.Log(receivedString);
        gyro = getGyro(receivedString);
        accel = getAccel(receivedString);

    }

    private void recv(IAsyncResult res)
    {
        IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 2000);
        byte[] received = Client.EndReceive(res, ref RemoteIpEndPoint);
        receivedString = Encoding.ASCII.GetString(received);
        Client.BeginReceive(new AsyncCallback(recv), null);
    }

    public float[] getGyro(String s){
        float[] gyro = new float[3];
        string[] split = s.Split('|');
        for (int i = 3; i < 6; i++)
        {
            gyro[i-3] = float.Parse(split[i])/65536;
        }
        Debug.Log(gyro[0]);

        return gyro;
    }

    public float[] getAccel(String s){
        float[] accel = new float[3];
        string[] split = s.Split('|');
        for (int i = 0; i < 3; i++)
        {
            accel[i] = float.Parse(split[i]);
        }
        return accel;
    }
}
