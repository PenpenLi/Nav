using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UpgradeTimer : MonoBehaviour
{
    public List<GameObject> m_AnimationList = new List<GameObject>();

    public GameObject m_BuildingUpgraed;
    public GameObject m_TimeLine;
    public GameObject m_HouseName;
    public GameObject m_Buy;

    public UISprite m_ProgressBar;
    public UILabel m_UpgradeTime;
    public UILabel m_Level;


    private int FixedTime; //接收当前升级所需时间
    private int NowTime;
    private int Stime = 60;
    private string HouseName;
    private bool Ft = false;//加速提示开关


    // Use this for initialization
    void Start()
    {

        for (int i = 0; i < m_AnimationList.Count; i++)
        {
            UIEventListener.Get(m_AnimationList[i]).onClick = onAnimation;
        }
        if (GlobalVar.GetInstance().AtlaseQueues == 1)
        {
            Debug.Log("你点击的是Sale地标！购买成功派出建筑队！");
            HouseName = m_BuildingUpgraed.transform.parent.GetComponent<Building>().m_HouseMenu.GetComponent<HouseMenu>().HouseName;
            FixedTime = 9;
            NowTime = 9;
            GlobalVar.GetInstance().BulidQueues = 1;
            Debug.Log("加入建造队列！");
        }
        else
        {
            Debug.Log("你点击的是建筑！资源齐备派出装修队！");
            HouseName = m_BuildingUpgraed.transform.parent.GetComponent<Building>().m_House.GetComponent<UISprite>().spriteName;
            FixedTime = GlobalVar.GetInstance().UpgradeTime;
            NowTime = GlobalVar.GetInstance().UpgradeTime;
            GlobalVar.GetInstance().UpgradeQueues = 1;
            Debug.Log("加入装修队列！");
        }
        
        
        TimeCount(FixedTime);
        if (FixedTime > 0)
        {
            m_AnimationList[0].SetActive(true);
        }
    }

    void onAnimation(GameObject go)
    {
        for (int i = 0; i < m_AnimationList.Count; i++)
        {
            if (m_AnimationList[i].GetHashCode() == go.gameObject.GetHashCode())
            {
                if (Ft == false)
                {
                    m_TimeLine.transform.localPosition = new Vector3(0, 30f, 0);
                    m_HouseName.SetActive(true);
                    m_Buy.SetActive(true);
                    m_HouseName.GetComponent<UILabel>().text = HouseName;
                    m_Level.text = "等级 " + HouseName[HouseName.Length-1];
                    Ft = true;
                }
                else if (Ft == true)
                {
                    m_TimeLine.transform.localPosition = new Vector3(0, 75f, 0);
                    m_HouseName.SetActive(false);
                    m_Buy.SetActive(false);
                    m_HouseName.GetComponent<UILabel>().text = HouseName;
                    Ft = false;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        MyTimer();
    }

    void MyTimer()
    {
        //Debug.Log("NowTime;" + NowTime);
        if (NowTime > 0)
        {
            Stime--;
            if (Stime <= 0)
            {
                //Debug.Log("过了一秒");
                m_ProgressBar.GetComponent<UISprite>().fillAmount += 1f / FixedTime;
                NowTime -= 1;
                TimeCount(NowTime);
                if (NowTime < FixedTime / 3 * 2) { DisplayCenter(); }
                if (NowTime < FixedTime / 3) { DisplayEnd(); }
                Stime = 60;
            }
        }
        else
        {
            UpGradeOver();
            m_ProgressBar.GetComponent<UISprite>().fillAmount = 0;
            //NowTime = GlobalVar.GetInstance().UpgradeTime;
            Debug.Log("升级结束！");
            m_BuildingUpgraed.GetComponent<UpgradeTimer>().enabled = false;
        }
    }

    void TimeCount(int TimeS)
    {
        int H_TimeDay = TimeS / (60 * 60 * 24);
        int H_TimeHour = (TimeS / 60 - TimeS / (60 * 60 * 24) * 24 * 60) / 60;
        int H_TimeMinute = (TimeS / 60) % 60;
        int H_TimeSecond = TimeS % 60;

        if (H_TimeDay > 0)
        {
            m_UpgradeTime.GetComponent<UILabel>().text = H_TimeDay + "天" + H_TimeHour + "小时" + H_TimeMinute + "分" + H_TimeSecond + "秒";
        }
        else
        {
            if (H_TimeHour > 0)
            {
                m_UpgradeTime.GetComponent<UILabel>().text = H_TimeHour + "小时" + H_TimeMinute + "分" + H_TimeSecond + "秒";
            }
            else
            {
                if (H_TimeMinute > 0)
                {
                    m_UpgradeTime.GetComponent<UILabel>().text = H_TimeMinute + "分" + H_TimeSecond + "秒";
                }
                else
                {
                    if (H_TimeSecond > 0)
                    {
                        m_UpgradeTime.GetComponent<UILabel>().text = H_TimeSecond + "秒";
                    }
                }
            }
        }
    }

    void DisplayCenter()
    {
        m_AnimationList[0].SetActive(false);
        m_AnimationList[1].SetActive(true);
    }

    void DisplayEnd()
    {
        m_AnimationList[1].SetActive(false);
        m_AnimationList[2].SetActive(true);
    }

    void UpGradeOver()
    {
        Transform Tf = m_BuildingUpgraed.transform.parent;
        m_AnimationList[2].SetActive(false);
        m_TimeLine.SetActive(false);
        char[] S = HouseName.ToCharArray();
        int Y;
        if (GlobalVar.GetInstance().AtlaseQueues == 1)
        {
            Debug.Log("当前建造的建筑图集是 =" + Tf.GetComponent<Building>().m_HouseMenu.GetComponent<HouseMenu>().HouseAtlas);
            Tf.GetComponent<Building>().m_House.GetComponent<UISprite>().atlas = Tf.GetComponent<Building>().m_HouseMenu.GetComponent<HouseMenu>().HouseAtlas;
            Y = Convert.ToInt32(S[S.Length - 1]);
            GlobalVar.GetInstance().BulidQueues = 0;//建造完后队列清空
            GlobalVar.GetInstance().AtlaseQueues = -1;//接收完图集后初始化
        }
        else
        {
            Y = Convert.ToInt32(S[S.Length - 1] + 1);
            GlobalVar.GetInstance().UpgradeQueues = 0;//装修完后队列清空
        }
        S[S.Length - 1] = Convert.ToChar(Y);
        string Str = new string(S);
        Tf.GetComponent<Building>().m_House.GetComponent<UISprite>().spriteName = Str;
        Tf.GetComponent<Building>().m_House.SetActive(true);
        Tf.GetComponent<Building>().m_House.GetComponent<TweenColor>().enabled = false;
        Tf.GetComponent<Building>().m_House.GetComponent<TweenColor>().ResetToBeginning();
        m_BuildingUpgraed.GetComponent<UpgradeTimer>().enabled = false;
        m_BuildingUpgraed.transform.parent.GetComponent<Building>().m_House.SetActive(true);
        Destroy(m_BuildingUpgraed);
        //GlobalVar.GetInstance().UpgradeQueues = 0;//装修完后队列清空
        //GlobalVar.GetInstance().BulidQueues = 0;//建造完后队列清空
        GlobalVar.GetInstance().BobjS = 1;
        //GlobalVar.GetInstance().AtlaseQueues = -1;//接收完图集后初始化
        Debug.Log("建筑升级结束！" + Str);
    }

    public void AddUpgraed(string Parent)
    {
        GameObject Obj = Instantiate(Resources.Load("Prefab/Building/BuildingUpgraed")) as GameObject;//get perfab; 
        Obj.transform.parent = GameObject.Find(Parent).transform;
        Obj.transform.localScale = Vector3.one;
        Obj.transform.localPosition = Vector3.zero;
    }
}
