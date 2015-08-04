using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class HouseMenu : MonoBehaviour {

    public List<GameObject> m_BuyButLise = new List<GameObject>();

    public GameObject m_CloseBut;
    public GameObject m_HouseMenu;
    public GameObject m_InputGold;
    public GameObject m_CameraMap;

    int GoldCount = 999999999;

    //获得
    public bool BuyStatus;//是否购买成功

    //获得当前购买的建筑资源名字与图集
    public string HouseName;
    public UIAtlas HouseAtlas;

	// Use this for initialization
	void Start () 
    {
        BuyStatus = false;//每次进入建筑选择界面前先初始化购买状态
        for (int i = 0; i < m_BuyButLise.Count; i++)
        {
            UIEventListener.Get(m_BuyButLise[i]).onClick = onBuyBut;
        }
        UIEventListener.Get(m_CloseBut).onClick = onClose;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void onBuyBut(GameObject go)
    {
        if (GlobalVar.GetInstance().BulidQueues == 0) // 使用建筑队列来控制是否进行购买操作
        {
            for (int i = 0; i < m_BuyButLise.Count; i++)
            {

                if (m_BuyButLise[i].GetHashCode() == go.gameObject.GetHashCode())
                {
                    long a = long.Parse(m_BuyButLise[i].transform.parent.FindChild("Sell").FindChild("MoneyCount").GetComponentInChildren<UILabel>().text);
                    long b = long.Parse(m_InputGold.transform.FindChild("TestGold").GetComponent<UILabel>().text);
                    if (a < b)
                    {
                        HouseName = m_BuyButLise[i].transform.parent.FindChild("HouseIcon").GetComponent<UISprite>().spriteName;
                        HouseAtlas = m_BuyButLise[i].transform.parent.FindChild("HouseIcon").GetComponent<UISprite>().atlas;
                        m_InputGold.transform.FindChild("TestGold").GetComponent<UILabel>().text = (b - a).ToString();
                        m_CameraMap.GetComponent<CameraDragMove>().enabled = true;
                        m_CameraMap.GetComponent<ScalingMap>().enabled = true;
                        GlobalVar.GetInstance().AtlaseQueues = 1;//购买成功同意传送图集
                        BuyStatus = true; //购买成功
                        m_HouseMenu.SetActive(false);
                        Debug.Log("目标建筑了已成功购买！BuyStatus =" + BuyStatus);
                    }
                    else
                    {
                        warning Warning = new warning();
                        string WarningStr = "岛主我们的现金不足，无法购买此建筑！您可以变卖宝石来快速获得现金！";
                        Warning.AddWarning(WarningStr);
                    }
                }
            }
        }
        else if (GlobalVar.GetInstance().BulidQueues == 1)
        {
            warning Warning = new warning();
            string WarningStr = "岛主我们目前只能建造一座建筑";
            Warning.AddWarning(WarningStr);
        }
        
    }

    void onClose(GameObject go)
    {
        BuyStatus = false;
        m_HouseMenu.SetActive(false);
        m_CameraMap.GetComponent<CameraDragMove>().enabled = true;
        m_CameraMap.GetComponent<ScalingMap>().enabled = true;

    }
}
