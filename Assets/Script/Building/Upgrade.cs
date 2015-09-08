using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using NetManager;


public class Upgrade : MonoBehaviour
{
    public List<GameObject> m_ResList = new List<GameObject>();
    public GameObject m_BObj;
    public GameObject m_UpgradeManager;
    public GameObject m_House;

    public GameObject m_HName;
    public GameObject m_HLev;
    public GameObject m_BId;
    public GameObject m_UpInfo;
    public GameObject m_Upgrade;
    public GameObject m_Items;

    private List<B_Base> m_MyBB = new List<B_Base>();

    // Use this for initialization
    void Start()
    {
        m_ResList = m_BObj.GetComponent<Building>().m_BaseList;
        m_MyBB = MyApp.GetInstance().GetDataManager().BB();
        UIEventListener.Get(m_Upgrade).onClick = onUpgrade;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void onUpgrade(GameObject go)
    {
        int Ind = Convert.ToInt32(m_BId.GetComponent<UILabel>().text);
        if (GlobalVar.GetInstance().UpgradeQueues == 0)
        { 
            if (m_MyBB[Ind].B_LEV < m_MyBB[Ind].MAX_LEV)
            {
                //现有资源数量
                int Item0 = Convert.ToInt32(m_ResList[0].GetComponentInChildren<UILabel>().text);
                int Item1 = Convert.ToInt32(m_ResList[m_MyBB[Ind].NEED_TIME1].GetComponentInChildren<UILabel>().text);
                int Item2 = Convert.ToInt32(m_ResList[m_MyBB[Ind].NEED_TIME2].GetComponentInChildren<UILabel>().text);
                int Item3 = Convert.ToInt32(m_ResList[m_MyBB[Ind].NEED_TIME3].GetComponentInChildren<UILabel>().text);

                if (m_MyBB[Ind].BUY_NEED_GOLD < Item0 && m_MyBB[Ind].COUNT1 < Item1 && m_MyBB[Ind].COUNT2 < Item2 && m_MyBB[Ind].COUNT3 < Item3)
                {
                    m_ResList[0].GetComponentInChildren<UILabel>().text = (Item0 - m_MyBB[Ind].BUY_NEED_GOLD).ToString();
                    m_ResList[m_MyBB[Ind].NEED_TIME1].GetComponentInChildren<UILabel>().text = (Item1 - m_MyBB[Ind].COUNT1).ToString();
                    m_ResList[m_MyBB[Ind].NEED_TIME2].GetComponentInChildren<UILabel>().text = (Item2 - m_MyBB[Ind].COUNT2).ToString();
                    m_ResList[m_MyBB[Ind].NEED_TIME3].GetComponentInChildren<UILabel>().text = (Item3 - m_MyBB[Ind].COUNT3).ToString();

                    //保值
                    GlobalVar.GetInstance().UpgradeStartTime = DateTime.Now;
                    GlobalVar.GetInstance().UpgradeID = Convert.ToInt32(m_BId.GetComponent<UILabel>().text);
                    GlobalVar.GetInstance().UpgradeQueues = 1;
                    GlobalVar.GetInstance().UpgradeTime = m_MyBB[Ind].LEV_UP_TIME;

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
}
