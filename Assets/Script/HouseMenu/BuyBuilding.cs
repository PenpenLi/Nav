using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class BuyBuilding : MonoBehaviour
{

    public List<GameObject> m_BuyButLise = new List<GameObject>();
    public List<GameObject> m_ResList = new List<GameObject>();

    public GameObject m_NeedGold;
    public GameObject m_BuyBut;
    public GameObject m_HouseIcon;
    public GameObject m_HouseName;
    public GameObject m_BId;
    public GameObject m_HouseMenu;
    public GameObject m_CameraMap;

    private List<bbase> m_MyBB = new List<bbase>();
    private int Ind;
    // Use this for initialization
    void Start()
    {
        Ind = Convert.ToInt32(m_BId.GetComponent<UILabel>().text);
        m_MyBB = MyApp.GetInstance().GetDataManager().BB();
        UIEventListener.Get(m_BuyBut).onClick = onBuyBut;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void onBuyBut(GameObject go)
    {
        GlobalVar.GetInstance().BuildTime = m_MyBB[Ind].UpgradeTime;
        if (GlobalVar.GetInstance().BuildQueues == 0)
        {
            //现有资源数量
            int Item0 = Convert.ToInt32(m_ResList[0].GetComponentInChildren<UILabel>().text);
            int Item1 = Convert.ToInt32(m_ResList[m_MyBB[Ind].NeedID1].GetComponentInChildren<UILabel>().text);
            int Item2 = Convert.ToInt32(m_ResList[m_MyBB[Ind].NeedID2].GetComponentInChildren<UILabel>().text);
            int Item3 = Convert.ToInt32(m_ResList[m_MyBB[Ind].NeedID3].GetComponentInChildren<UILabel>().text);

            if (m_MyBB[Ind].Cost < Item0 && m_MyBB[Ind].Count1 < Item1 && m_MyBB[Ind].Count2 < Item2 && m_MyBB[Ind].Count3 < Item3)
            {
                m_ResList[0].GetComponentInChildren<UILabel>().text = (Item0 - m_MyBB[Ind].Cost).ToString();
                m_ResList[m_MyBB[Ind].NeedID1].GetComponentInChildren<UILabel>().text = (Item1 - m_MyBB[Ind].Count1).ToString();
                m_ResList[m_MyBB[Ind].NeedID2].GetComponentInChildren<UILabel>().text = (Item2 - m_MyBB[Ind].Count2).ToString();
                m_ResList[m_MyBB[Ind].NeedID3].GetComponentInChildren<UILabel>().text = (Item3 - m_MyBB[Ind].Count3).ToString();

                //保值
                GlobalVar.GetInstance().BuildStartTime = DateTime.Now;
                GlobalVar.GetInstance().BuildID = Convert.ToInt32(m_BId.GetComponent<UILabel>().text);

                GlobalVar.GetInstance().BuildQueues = 1;           

                m_HouseMenu.SetActive(false);
                m_CameraMap.GetComponent<CameraDragMove>().enabled = true;
                m_CameraMap.GetComponent<ScalingMap>().enabled = true;
                

                BuildTimer UI = new BuildTimer(GlobalVar.GetInstance().BuildObjname); //派遣建造队
            }
            else
            {
                string str = "岛主，金币不足目前无法对目标建筑进行升级！！";
                warning warning = new warning(str);
            }
        }
        else
        {
            string Str = "岛主，当前没有闲置的建造队，所以无法进行装修！";
            warning Warning = new warning(Str);
        }

    }
}
