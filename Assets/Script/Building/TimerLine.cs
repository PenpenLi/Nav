using UnityEngine;
using System.Collections;

public class TimerLine : MonoBehaviour {

    public GameObject m_BuildInfo;
    public GameObject m_Warning;
    //public GameObject m_Center;
    //public GameObject m_End;

    //之前的一个时间点  
    //public long startTime = 1435870304;
    //限定时间秒  
    private long fixedTime ;
    private long nowTime;

    bool isOver = true;
    float Fa;
	// Use this for initialization
	void Start () 
    {
        //Fa = m_BuildInfo.transform.FindChild("TimeLine1").FindChild("ProgressBar").GetComponent<UISprite>().fillAmount;
        fixedTime = GlobalVar.GetInstance().UpgradeTime;
        Debug.Log("你已经进入建筑升级阶段！");
        nowTime = (System.DateTime.Now.Ticks - System.DateTime.Parse("1970-01-01").Ticks) / 10000000;
        InvokeRepeating("CountDown", 0, 1);
        InvokeRepeating("DisplayCenter", fixedTime / 3,0);
        InvokeRepeating("DisplayEnd", fixedTime / 3 * 2,0);
        InvokeRepeating("UpGradeOver", fixedTime,0);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!isOver && nowTime >= fixedTime)//- startTime
        {
            Debug.Log("倒计时结束");
            isOver = true;
        } 
	}

    void CountDown()
    {
        if (fixedTime > 0)
        {
            fixedTime -= 1;
            
            m_BuildInfo.transform.FindChild("TimeLine").FindChild("ProgressBar").GetComponent<UISprite>().fillAmount += 1f / GlobalVar.GetInstance().UpgradeTime;
            long H_TimeDay = fixedTime / (60 * 60 * 24);
            long H_TimeHour = (fixedTime / 60 - fixedTime / (60 * 60 * 24) * 24 * 60) / 60;
            long H_TimeMinute = (fixedTime / 60) % 60;
            long H_TimeSecond = fixedTime % 60;
            //m_BuildInfo.transform.parent.FindChild("BuildInfo").
            //    FindChild("TimeLine").FindChild("BuildTime").
            //    GetComponent<UILabel>().text =
            //   (fixedTime / (60 * 60 * 24)).ToString() + "天"
            //    + ((fixedTime / 60 - fixedTime / (60 * 60 * 24) * 24 * 60) / 60).ToString() + "时"
            //    + ((fixedTime / 60) % 60).ToString() + "分"
            //    + (fixedTime % 60).ToString() + "秒";
            if (H_TimeDay > 0)
            {
                m_BuildInfo.transform.parent.FindChild("BuildInfo").FindChild("TimeLine").FindChild("BuildTime").GetComponent<UILabel>().text =
                       H_TimeDay + "天" + H_TimeHour + "小时" + H_TimeMinute + "分" + H_TimeSecond + "秒";
            }
            else
            {
                if (H_TimeHour > 0)
                {
                    m_BuildInfo.transform.parent.FindChild("BuildInfo").FindChild("TimeLine").FindChild("BuildTime").GetComponent<UILabel>().text =
                       H_TimeHour + "小时" + H_TimeMinute + "分" + H_TimeSecond + "秒";
                }
                else
                {
                    if (H_TimeMinute > 0)
                    {
                        m_BuildInfo.transform.parent.FindChild("BuildInfo").FindChild("TimeLine").FindChild("BuildTime").GetComponent<UILabel>().text =
                              H_TimeMinute + "分" + H_TimeSecond + "秒";
                    }
                    else
                    {
                        if (H_TimeSecond > 0)
                        {
                            m_BuildInfo.transform.parent.FindChild("BuildInfo").FindChild("TimeLine").FindChild("BuildTime").GetComponent<UILabel>().text =
                               H_TimeSecond + "秒";
                        }
                    }
                }
            }
        }
        else
        { fixedTime = GlobalVar.GetInstance().UpgradeTime; }
    }


    void DisplayCenter()
    {
        m_BuildInfo.transform.parent.GetComponent<Building>().m_Statr.SetActive(false);
        m_BuildInfo.transform.parent.GetComponent<Building>().m_Center.SetActive(true);
    }


    void DisplayEnd()
    {
        Debug.Log("你已经进入建筑升级中期阶段！");
        m_BuildInfo.transform.parent.GetComponent<Building>().m_Center.SetActive(false);
        m_BuildInfo.transform.parent.GetComponent<Building>().m_End.SetActive(true);
    }

    void UpGradeOver()
    {
        Debug.Log("你已经进入建筑升级后期阶段！");
        m_BuildInfo.transform.parent.GetComponent<Building>().m_End.SetActive(false);
        m_BuildInfo.transform.parent.GetComponent<Building>().m_TimeLine1.SetActive(false);
        m_BuildInfo.transform.parent.GetComponent<Building>().m_House.SetActive(true);
        m_BuildInfo.GetComponent<TimerLine>().enabled = false;
        GlobalVar.GetInstance().BobjS = 1;
        Debug.Log("建筑升级结束！");
    }
}
