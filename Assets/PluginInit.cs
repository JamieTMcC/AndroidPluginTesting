using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PluginInit : MonoBehaviour
{
    AndroidJavaClass unityClass;
    AndroidJavaObject unityActivity;
    AndroidJavaObject _pluginInstance;
    AndroidJavaObject unityContext;
    bool UPDsetup = false;


    // Start is called before the first frame update
    void Start()
    {
        InitialisePlugin("com.example.unityplugin.PluginInstance");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitialisePlugin(string pluginName){
        unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
        unityContext = unityActivity.Call<AndroidJavaObject>("getApplicationContext");
        _pluginInstance = new AndroidJavaObject(pluginName);
        if (_pluginInstance != null)
        {
            Debug.Log("Plugin error");
        }
        _pluginInstance.CallStatic("setUnityActivity", unityActivity);

    }

    public long getTime(){
        long result = _pluginInstance.Call<long>("getTime");
        Debug.Log("Time: " + result);
        return result;
    }
    public string getRequest(){
        string result = _pluginInstance.Call<string>("getHTML");
       // Debug.Log("Request: " + result);
        return result;
    }
    public string getUDPPacket(){
        if(!UPDsetup){
            _pluginInstance.Call("setUpUDP");
            Debug.Log("UDP set up");
            UPDsetup = true;
        }
        string result = _pluginInstance.Call<string>("getUDPPacket");
        Debug.Log("UDP Packet: " + result);
        return result;

    }

    public void toast(){
        if (_pluginInstance != null)
        {
            int numberofcalls = 0;
            var watch = new System.Diagnostics.Stopwatch();
            //parse string as integer
            List<int> numbers = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                
                watch.Start();
                for (int j = 0; j < 100; j++)
                {   
                    int v = Int32.Parse(getUDPPacket());
                    Debug.Log(v);
                    numbers.Add(v);
                }
                watch.Stop();
                Debug.Log($"Execution Time: {watch.ElapsedMilliseconds} ms");
                Debug.Log($"Execution Time per request: {watch.ElapsedMilliseconds / 100} ms");
                watch.Restart();
            }
            numbers.Sort();
            Debug.Log(string.Join(" ", numbers));
            Debug.Log("Number of Numbers: " + numbers.Count);


        }
        else{
            Debug.Log("Plugin error");
        }
    }

}
