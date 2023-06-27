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

    public void toast(){
        if (_pluginInstance != null)
        {
            var watch = new System.Diagnostics.Stopwatch();
            //parse string as integer
            int x = Int32.Parse(getRequest());
            int y = 0;
            for (int i = 0; i < 10; i++)
            {
                
                watch.Start();
                for (int j = 0; j < 100; j++)
                {
                    y = Int32.Parse(getRequest());
                    if (x != y - 1)
                    {
                        Debug.Log("Error: " + x + " " + y);
                    }
                    x++;
                }
                watch.Stop();
                Debug.Log($"Execution Time: {watch.ElapsedMilliseconds} ms");
                Debug.Log($"Execution Time per request: {watch.ElapsedMilliseconds / 100} ms");
                watch.Restart();
            }


        }
        else{
            Debug.Log("Plugin error");
        }
    }

}
