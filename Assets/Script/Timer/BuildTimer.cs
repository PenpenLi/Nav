﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
public class BuildTimer : MonoBehaviour {

    public List<GameObject> m_AnimationList = new List<GameObject>();
    private List<B_Base> m_MyBB = new List<B_Base>();
    private List<Item> m_MyItem = new List<Item>();
    
    public GameObject m_BuildingUpgraed;
    public GameObject m_TimeLine;
    public GameObject m_HouseName;
    public GameObject m_Buy;

    public UISprite m_ProgressBar;
    public UILabel m_UpgradeTime;
    public UILabel m_Level;

    private long Stime;
    private int ID;
    private string HouseName;
    private bool Ft = false;

    private DateTime Dt;
    // Use this for initialization
    void Start()
    {
        Debug.Log("开始建造！！！！！！");
        m_BuildingUpgraed.transform.parent.GetComponent<Building>().m_Sale.SetActive(false);
        m_MyBB = MyApp.GetInstance().GetDataManager().BB();
        m_MyItem = MyApp.GetInstance().GetDataManager().Item();

        ID = GlobalVar.GetInstance().BuildID;
        Dt = GlobalVar.GetInstance().BuildStartTime;
        GlobalVar.GetInstance().ceshi = ID.ToString();

        //点击动画显示建筑当前简略信息
        for (int i = 0; i < m_AnimationList.Count; i++)
        {
            UIEventListener.Get(m_AnimationList[i]).onClick = onAnimation;
        }
        Stime = m_MyBB[ID].LEV_UP_TIME;

        TimeCount(Stime);
        if (Stime > 0)
        {
            m_AnimationList[0].SetActive(true);
        }
    }

    void onAnimation(GameObject go)//点击动画查看信息
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
                    m_HouseName.GetComponent<UILabel>().text = m_MyBB[ID].B_NAME;
                    m_Level.text = m_MyBB[ID].B_LEV.ToString();
                    Ft = true;
                }
                else if (Ft == true)
                {
                    m_TimeLine.transform.localPosition = new Vector3(0, 75f, 0);
                    m_HouseName.SetActive(false);
                    m_Buy.SetActive(false);
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
        long DTdiff = DateTime.Now.Ticks - Dt.Ticks; //Debug.Log("DTdiff " + DTdiff);
        long DTnow = Stime - Convert.ToInt32(DTdiff / 10000000); //Debug.Log("DTnow " + DTnow);
        TimeCount(DTnow);
        m_ProgressBar.GetComponent<UISprite>().fillAmount = (1f / Stime) * (Stime - DTnow);

        if (DTnow < Stime / 3 * 2)
        {
            m_AnimationList[0].SetActive(false);
            m_AnimationList[1].SetActive(true);
        }
        if (DTnow < Stime / 3)
        {
            m_AnimationList[1].SetActive(false);
            m_AnimationList[2].SetActive(true);
        }
        if (DTnow <= 0)
        {
            m_ProgressBar.GetComponent<UISprite>().fillAmount = 0;

            GlobalVar.GetInstance().BuildQueues = 0; //清空升级列表

            UIAtlas Ua = Resources.Load("Atlas/House/" + m_MyBB[ID].B_ATLAS_NAME, typeof(UIAtlas)) as UIAtlas;
            m_BuildingUpgraed.transform.parent.GetComponent<Building>().m_House.GetComponent<UISprite>().atlas = Ua;
            m_BuildingUpgraed.transform.parent.GetComponent<Building>().m_House.GetComponent<UISprite>().spriteName = m_MyBB[ID].B_ICON_NAME;
            m_BuildingUpgraed.transform.parent.GetComponent<Building>().m_BId.GetComponent<UILabel>().text = ID.ToString();
            m_BuildingUpgraed.transform.parent.FindChild("BUManager").FindChild("HName").GetComponent<UILabel>().text = m_MyBB[ID].B_NAME;
            m_BuildingUpgraed.transform.parent.FindChild("BUManager").GetComponent<Upgrade>().m_HLev.GetComponent<UILabel>().text = m_MyBB[ID].B_LEV.ToString();

            GlobalVar.GetInstance().ceshi = m_MyItem[m_MyBB[ID].ITEM_ID].ITEM_ATLAS_NAME;
            UIAtlas Uia = Resources.Load("Atlas/Lobby/" + m_MyItem[m_MyBB[ID].ITEM_ID].ITEM_ATLAS_NAME, typeof(UIAtlas)) as UIAtlas;
            m_BuildingUpgraed.transform.parent.FindChild("Items").FindChild("2").GetComponent<UISprite>().atlas = Uia;
            m_BuildingUpgraed.transform.parent.FindChild("Items").FindChild("2").GetComponent<UISprite>().spriteName = m_MyItem[m_MyBB[ID].ITEM_ID].ITEM_ICON_NAME;

            string WarningStr = "恭喜岛主" + m_MyBB[ID].B_NAME + " 已经顺利建成！！";
            warning Warning = new warning(WarningStr);

            m_BuildingUpgraed.transform.parent.GetComponent<Building>().m_House.SetActive(true);            
           
            m_BuildingUpgraed.GetComponent<UpgradeTimer>().enabled = false;
            Destroy(m_BuildingUpgraed);
        }
    }
    void TimeCount(long TimeS) //倒计时显示换算
    {
        long H_TimeDay = TimeS / (60 * 60 * 24);
        long H_TimeHour = (TimeS / 60 - TimeS / (60 * 60 * 24) * 24 * 60) / 60;
        long H_TimeMinute = (TimeS / 60) % 60;
        long H_TimeSecond = TimeS % 60;

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
    public BuildTimer(string Parent)
    {
        GameObject Obj = Instantiate(Resources.Load("Prefab/Building/BuildingUpgraed")) as GameObject;//get perfab; 
        Obj.transform.parent = GameObject.Find(Parent).transform;
        Obj.transform.localScale = Vector3.one;
        Obj.transform.localPosition = Vector3.zero;
        Obj.GetComponent<BuildTimer>().enabled = true;
    }
}