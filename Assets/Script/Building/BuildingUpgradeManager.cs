using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using NetManager;


public class BuildingUpgradeManager : MonoBehaviour
{
    public List<GameObject> m_BaseList = new List<GameObject>();
    public GameObject m_BObj;
    public GameObject m_UpgradeManager;
    public GameObject m_House;

    public GameObject m_HName;
    public GameObject m_HLev;
    public GameObject m_BId;
    public GameObject m_UpInfo;
    public GameObject m_Upgrade;
    public GameObject m_Items;

    //本地库数据
    private int ItemID1;
    private int ItemID2;
    private int ItemID3;

    private int ItemCount1;
    private int ItemCount2;
    private int ItemCount3;

    private int NeedGold;
    private int MaxLev;

    private int UpgradeTime;



    // Use this for initialization
    void Start()
    {
        m_BaseList = m_BObj.GetComponent<Building>().m_BaseList; 
        UIEventListener.Get(m_Upgrade).onClick = onUpgrade;
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    void onUpgrade(GameObject go)
    {
        GetResBase();
        if (GlobalVar.GetInstance().UpgradeQueues == 0)
        {
            int Lev = Convert.ToInt32(m_HLev.GetComponent<UILabel>().text);
            if(Lev < MaxLev)
            {
                //现有资源数量
                int Item0 = Convert.ToInt32(m_BaseList[0].GetComponentInChildren<UILabel>().text);
                int Item1 = Convert.ToInt32(m_BaseList[ItemID1].GetComponentInChildren<UILabel>().text);
                int Item2 = Convert.ToInt32(m_BaseList[ItemID2].GetComponentInChildren<UILabel>().text);
                int Item3 = Convert.ToInt32(m_BaseList[ItemID3].GetComponentInChildren<UILabel>().text);
                if (NeedGold < Item0 && ItemCount1 < Item1 && ItemCount2 < Item2 && ItemCount3 < Item3)
                {
                    Debug.Log("升级资源充足，向服务发送升级请求！");
                    Debug.Log("当前升级所需资源 →  Glod：" + NeedGold);
                    Debug.Log("当前升级所需资源 →  "+m_BaseList[ItemID1].name+"：" + ItemCount1);
                    Debug.Log("当前升级所需资源 →  "+m_BaseList[ItemID2].name+"：" + ItemCount2);
                    Debug.Log("当前升级所需资源 →  "+m_BaseList[ItemID3].name+"：" + ItemCount3);
                    Debug.Log("当前升级所需时间 →  "+UpgradeTime);

                    m_BaseList[0].GetComponentInChildren<UILabel>().text = (Item0 - NeedGold).ToString();
                    m_BaseList[ItemID1].GetComponentInChildren<UILabel>().text = (Item1 - ItemCount1).ToString();
                    m_BaseList[ItemID2].GetComponentInChildren<UILabel>().text = (Item2 - ItemCount2).ToString();
                    m_BaseList[ItemID3].GetComponentInChildren<UILabel>().text = (Item3 - ItemCount3).ToString();

                    //保值
                    GlobalVar.GetInstance().StartTime = DateTime.Now;
                    GlobalVar.GetInstance().UpgradeQueues = 1;
                    GlobalVar.GetInstance().BID = Convert.ToInt32(m_BId.GetComponent<UILabel>().text);
                    GlobalVar.GetInstance().UpgradeTime = UpgradeTime;
                    GlobalVar.GetInstance().UpType = -1;
                    
                    m_UpgradeManager.SetActive(false);
                    m_House.SetActive(false);
                    m_House.GetComponent<TweenColor>().enabled = false;
                    m_House.GetComponent<TweenColor>().ResetToBeginning();

                    UpgradeTimer UI = new UpgradeTimer(m_BObj.name); //派遣装修队
                }
                else
                {
                    string str = "岛主，金币不足目前无法对目标建筑进行升级！！";
                    warning warning = new warning(str);
                }
            }
            else
            {
                string str = "岛主，当前建筑已升级到最高等级了！！";
                warning warning = new warning(str);
            }
        }
        else
        {
            string Str = "岛主，当前没有闲置的装修队，所以无法进行装修！";
            warning Warning = new warning(Str);
        }

    }

    private void UpgradeEventCallback(EventBase eb)
    {
        string eventname = eb.eventName;
        object obj = eb.eventValue;
        if (CGNetConst.ROUTE_BUILDING_UPGRADE.Equals(eventname))
        {
            if (obj != null)
            {
                CommonResult<PlayerInfo> commonResult = (CommonResult<PlayerInfo>)obj;

            }
        }
    }
    void CloseSelf()
    {

        m_HName.SetActive(false);
        m_HLev.SetActive(false);
        m_UpInfo.SetActive(false);
        m_Upgrade.SetActive(false);
        m_Items.SetActive(false);
        m_House.SetActive(false);
        m_House.GetComponent<TweenColor>().enabled = false;
        m_House.GetComponent<TweenColor>().ResetToBeginning();
    }
    void GetResBase()
    {
        int BID = Convert.ToInt32(m_BId.GetComponent<UILabel>().text);

        var Base = from n in MyApp.GetInstance().GetDataManager().BB()
                   where n.ID == BID
                   select new { n.BUY_NEED_GOLD, n.NEED_TIME1, n.NEED_TIME2, n.NEED_TIME3, n.COUNT1, n.COUNT2, n.COUNT3 ,n.MAX_LEV,n.LEV_UP_TIME};
        foreach (var B in Base)
        {          
            ItemID1 = B.NEED_TIME1;
            ItemID2 = B.NEED_TIME2;
            ItemID3 = B.NEED_TIME3;

            ItemCount1 = B.COUNT1;
            ItemCount2 = B.COUNT2;
            ItemCount3 = B.COUNT3;

            NeedGold = B.BUY_NEED_GOLD;
            MaxLev = B.MAX_LEV;

            UpgradeTime = B.LEV_UP_TIME;
        }
    }
}
