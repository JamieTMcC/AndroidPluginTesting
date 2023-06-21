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

    public void toast(){
        if (_pluginInstance != null)
        {
            _pluginInstance.Call("Toast", getTime().ToString());
        }
    }

}
