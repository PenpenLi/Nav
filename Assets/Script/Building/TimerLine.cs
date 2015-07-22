using UnityEngine;
using System.Collections;

public class TimerLine : MonoBehaviour {

    public GameObject m_BuildInfo;
    //public GameObject m_Start;
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
            
            m_BuildInfo.transform.FindChild("TimeLine1").FindChild("ProgressBar").GetComponent<UISprite>().fillAmount += 1f / GlobalVar.GetInstance().UpgradeTime;
            m_BuildInfo.transform.parent.FindChild("BuildInfo").
                FindChild("TimeLine1").FindChild("BuildTime").
                GetComponent<UILabel>().text =
               (fixedTime / (60 * 60 * 24)).ToString() + " D "
                + ((fixedTime / 60 - fixedTime / (60 * 60 * 24) * 24 * 60) / 60).ToString() + " H "
                + ((fixedTime / 60) % 60).ToString() + " M "
                + (fixedTime % 60).ToString() + " S ";
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
