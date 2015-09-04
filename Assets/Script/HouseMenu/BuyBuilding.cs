using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class BuyBuilding : MonoBehaviour
{

    public List<GameObject> m_BuyButLise = new List<GameObject>();

    public GameObject m_Gold;
    public GameObject m_NeedGold;
    public GameObject m_BuyBut;
    public GameObject m_HouseIcon;
    public GameObject m_HouseName;

    public GameObject m_HouseMenu;
    public GameObject m_CameraMap;

    // Use this for initialization
    void Start()
    {

        UIEventListener.Get(m_BuyBut).onClick = onBuyBut;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void onBuyBut(GameObject go)
    {
        if (GlobalVar.GetInstance().BulidQueues == 0)
        {
            GlobalVar.GetInstance().Bobjatlas = m_HouseIcon.GetComponent<UISprite>().atlas;
            GlobalVar.GetInstance().Bobjicon = m_HouseIcon.GetComponent<UISprite>().spriteName;
            Debug.Log("当前购买的建筑图集是→  " + GlobalVar.GetInstance().Bobjatlas);
            Debug.Log("当前购买的建筑用地是→  " + GlobalVar.GetInstance().Bobjname);

            var UpTime = from n in MyApp.GetInstance().GetDataManager().BB()
                         where n.B_NAME == m_HouseIcon.GetComponent<UISprite>().spriteName
                         where n.B_ICON_NAME == m_HouseIcon.GetComponent<UISprite>().spriteName
                         select n.B_LEV;
            foreach (var UT in UpTime)
                GlobalVar.GetInstance().NowLev = UT;

            int a = Convert.ToInt32(m_NeedGold.GetComponentInChildren<UILabel>().text);
            int b = Convert.ToInt32(m_Gold.GetComponent<UILabel>().text);
            if (a < b)
            {
                Debug.Log("派遣建造队，进行建造！！");
                string Str = GlobalVar.GetInstance().Bobjname;
                UpgradeTimer UTime = new UpgradeTimer(Str);
                 GlobalVar.GetInstance().StartTime = DateTime.Now; //记录开始时间

                m_Gold.GetComponent<UILabel>().text = (b - a).ToString();
                m_CameraMap.GetComponent<CameraDragMove>().enabled = true;
                m_CameraMap.GetComponent<ScalingMap>().enabled = true;

                GlobalVar.GetInstance().BulidQueues = 1;
                GlobalVar.GetInstance().BuySuccess = true; //购买成功
                m_HouseMenu.SetActive(false);
            }
            else
            {
                string WarningStr = "岛主我们的现金不足，无法购买此建筑！您可以变卖宝石来快速获得现金！";
                warning Warning = new warning(WarningStr);

            }
        }
        else if (GlobalVar.GetInstance().BulidQueues == 1)
        {
            string WarningStr = "岛主我们目前只能建造一座建筑";
            warning Warning = new warning(WarningStr);
        }

    }
}
