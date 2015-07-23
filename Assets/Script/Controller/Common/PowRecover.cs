using UnityEngine;
using System.Collections;

public class PowRecover : MonoBehaviour
{
    private const float recoverInterval = 15*60f;

    private float time = 0f;

    // Use this for initialization
    void Start()
    {
        PublicTimer.GetPublicTimer().serverTimer += ServerTimer;
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    private void ServerTimer(float delay)
    {
        //Player pInfo = PlayerDataManager.GetInstance().GetPlayerInfo();
        //if((pInfo != null)&&(pInfo.CurPower<pInfo.MaxPower))
        //{
        //    time += delay;
        //    if(time>=recoverInterval)
        //    {
        //        int pow = (int)(time/recoverInterval);
        //        PlayerDataManager.GetInstance().AddPlayerPow(pow);
        //        time -= pow*recoverInterval;
        //    }
        //}else
        //{
        //    time = 0f;
        //}
    }
}
