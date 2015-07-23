using UnityEngine;
using System;
using System.Collections;
using NetManager;
using SimpleJson;
using LitJson;

public class PublicTimer : MonoBehaviour
{
    //private static long serverTime = 0;
    private static DateTime serverTime = DateTime.Now;
    public delegate void ServerTimerDelegate(float delay);

    public event ServerTimerDelegate serverTimer;

    private float deltaTime = 0.0f;

    //private float deltaTime = 0.0f;

    void Awake()
    {
        Application.runInBackground = true;
    }
    // Use this for initialization
    void Start()
    {
    }
    
    // Update is called once per frame
    void Update()
    {
        deltaTime += RealTime.deltaTime;
        if(deltaTime>=0.5f)
        {
            DateTime newTime = serverTime.AddSeconds(deltaTime);
            ServerTimeChange(newTime);
            deltaTime = 0.0f;
        }
    }

    public static void ResetServerTime(long sTime)
    {
        System.DateTime baseDate = new System.DateTime(1970, 1, 1, 8, 0, 0);
        System.DateTime serverDate = baseDate.AddTicks(sTime * 10000);
        GetPublicTimer().ServerTimeChange(serverDate);
        Debug.Log("ServerTime:"+serverTime.ToString());
    }

    public static System.DateTime GetServerTime()
    {
        return serverTime;
    }

    public static PublicTimer GetPublicTimer()
    {
        PublicTimer ret = PublicGameObject.publicGameObj.GetComponent<PublicTimer>();
        return ret;
    }

    private void ServerTimeChange(DateTime newTime)
    {
        TimeSpan tSpan = newTime.Subtract(serverTime);
        serverTime = newTime;

        float deltaTime = tSpan.Ticks / 10000000.0f;
        if(serverTimer != null)
        {
            serverTimer(deltaTime);
        }
    }
}
