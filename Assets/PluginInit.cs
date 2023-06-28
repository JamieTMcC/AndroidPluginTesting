using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PluginInit : MonoBehaviour
{
    AndroidJavaClass unityClass;
    AndroidJavaObject unityActivity;
    AndroidJavaObject _pluginInstance;
    AndroidJavaObject unityContext;
    bool UPDsetup = false;
    public TMP_Text Debug_Text;
    string final_result = "";


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
        return result;
    }
    public string getUDPPacket(){
        if(!UPDsetup){
            _pluginInstance.Call("setUpUDP");
            Debug_Text.text += "UDP set up" + "\n";
            UPDsetup = true;
        }
        string result = _pluginInstance.Call<string>("getUDPPacket");
        return result;

    }

    public void toast(){
        if (_pluginInstance != null)
        {
            var watch = new System.Diagnostics.Stopwatch();
            //parse string as integer
            //for (int i = 0; i < 10; i++)
            //{
                
                watch.Start();
                for (int j = 0; j < 52; j++)
                {   
                  final_result += getUDPPacket()[0];
                }
                watch.Stop();
                Debug_Text.text += "final result: " + final_result + "\n";
                Debug_Text.text += $"Execution Time: {watch.ElapsedMilliseconds} ms" + "\n";
                Debug_Text.text += $"Execution Time per request: {watch.ElapsedMilliseconds / 100} ms" + "\n";
                watch.Restart();
            //}
            final_result = "";
        }
    }

}
