using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Building : MonoBehaviour
{
    public GameObject m_Sale;
    public GameObject m_House;

    public GameObject m_HouseUp;
    public GameObject m_UpInfo;
    public GameObject m_Upgrade;
    public GameObject m_Items;

    public GameObject m_building;
    public GameObject m_HouseMenu;
    public GameObject m_CameraMap;


    bool UDswitch = false; //update swicth
    int Switch;
    string  SaveHouseName;
    int MaxLevel = 5;


    // Use this for initialization
    void Start()
    {
        
        UIEventListener.Get(m_Sale).onClick = onSale;
        UIEventListener.Get(m_House).onClick = onHouse;
        UIEventListener.Get(m_Upgrade).onClick = onUpgrade;
        UIEventListener.Get(m_Items).onClick = onItems;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (UDswitch == true) // 打开数据接收
        {
            Debug.Log("等待接收购买建筑的数据UDswitch = " + UDswitch);
            if (m_HouseMenu.GetComponent<HouseMenu>().BuyStatus == true)
            {
                Debug.Log("接收到购买建筑的数据HCStatus = " + m_HouseMenu.GetComponent<HouseMenu>().BuyStatus);
                GlobalVar.GetInstance().BulidQueues = 1; //购买成功建筑队列+1
                m_Sale.SetActive(false);
                UpgradeTimer UpTime = new UpgradeTimer();
                string Str = m_building.name;
                UpTime.AddUpgraed(Str);
                UDswitch = false; //  数据接收成功结束当次数据接收
                m_HouseMenu.GetComponent<HouseMenu>().BuyStatus = false;//初始化购买状态

                Debug.Log("关闭当前数据接收UDswitch = " + UDswitch);
            }
        }
    }
    void onSale(GameObject go)
    {
        Debug.Log("当前perfab的名字是→ " + m_building.name);
        m_CameraMap.GetComponent<CameraDragMove>().enabled = false;
        m_CameraMap.GetComponent<ScalingMap>().enabled = false;
        UDswitch = true;//打开Update,等待接收购买建筑的数据
        m_HouseMenu.SetActive(true);

        if (GlobalVar.GetInstance().Bobjname != null) // 关闭上次打开的升级按钮菜单
        {
            m_building.transform.parent.FindChild(GlobalVar.GetInstance().Bobjname).GetComponent<Building>().m_HouseUp.SetActive(false);
            m_building.transform.parent.FindChild(GlobalVar.GetInstance().Bobjname).GetComponent<Building>().m_House.GetComponent<TweenColor>().enabled = false;
            m_building.transform.parent.FindChild(GlobalVar.GetInstance().Bobjname).GetComponent<Building>().m_House.GetComponent<TweenColor>().ResetToBeginning();
            GlobalVar.GetInstance().BobjS = 1;
            GlobalVar.GetInstance().Bobjname = null;
        }
    }

    void onHouse(GameObject go)
    {
        Switch = GlobalVar.GetInstance().BobjS;
        SaveHouseName = GlobalVar.GetInstance().Bobjname;
        //According to building type display item
        switch (m_House.transform.GetComponent<UISprite>().atlas.name)
        {
            case "ani_house_a": // Gold
                Debug.Log("产物是金币！");
                m_Items.transform.FindChild("2").GetComponent<UISprite>().spriteName = "icon_money5@2x";
                break;
            case "ani_house_b": // Rum
                Debug.Log("产物是朗姆酒！");
                m_Items.transform.FindChild("2").GetComponent<UISprite>().spriteName = "bar_grain@2x";
                break;
            default:
                m_Items.SetActive(false);
                m_HouseUp.transform.FindChild("UpInfo").localPosition = new Vector3(-19.0f, 0f, 0f);
                m_HouseUp.transform.FindChild("Upgrade").localPosition = new Vector3(41.0f, 0f, 0f);
                break;
        }

        //Close and open building upgrade button UI
        if (SaveHouseName != null)
        {
            if (SaveHouseName == m_building.name)
            {
                if (Switch == -1)
                {
                    m_HouseUp.SetActive(false);
                    m_House.GetComponent<TweenColor>().enabled = false;
                    m_House.GetComponent<TweenColor>().ResetToBeginning();
                    GlobalVar.GetInstance().BobjS = 1;
                }
                else
                {
                    m_HouseUp.SetActive(true);
                    m_House.GetComponent<TweenColor>().enabled = true;
                    GlobalVar.GetInstance().BobjS = -1;
                }
            }
            else
            {
                m_building.transform.parent.FindChild(GlobalVar.GetInstance().Bobjname).GetComponent<Building>().m_HouseUp.SetActive(false);
                m_building.transform.parent.FindChild(GlobalVar.GetInstance().Bobjname).GetComponent<Building>().m_House.GetComponent<TweenColor>().enabled = false;
                m_building.transform.parent.FindChild(GlobalVar.GetInstance().Bobjname).GetComponent<Building>().m_House.GetComponent<TweenColor>().ResetToBeginning();
                m_HouseUp.SetActive(true);
                m_House.GetComponent<TweenColor>().enabled = true;
                GlobalVar.GetInstance().Bobjname = m_building.name;
                GlobalVar.GetInstance().BobjS = -1;
            }
        }
        else
        {

            m_HouseUp.SetActive(true);
            m_House.GetComponent<TweenColor>().enabled = true;
            GlobalVar.GetInstance().Bobjname = m_building.name;
            GlobalVar.GetInstance().BobjS = -1;
        }
    }

    void onUpgrade(GameObject go)
    {
        //判断当前是否有升级队列存在
        if (GlobalVar.GetInstance().UpgradeQueues == 0)
        {
            string S = m_House.GetComponent<UISprite>().spriteName;
            char[] S1 = S.ToCharArray();
            string Y = Convert.ToString(S1[S1.Length - 1]);
            int Y1 = Convert.ToInt32(Y);
            Debug.Log("char Y =" + Y);
            Debug.Log("char Y =" + Y1);
            switch (Y1)
            {
                case 1:
                    GlobalVar.GetInstance().UpgradeTime = 18;
                    break;
                case 2:
                    GlobalVar.GetInstance().UpgradeTime = 27;
                    break;
                case 3:
                    GlobalVar.GetInstance().UpgradeTime = 36;
                    break;
                case 4:
                    GlobalVar.GetInstance().UpgradeTime = 45;
                    break;
                default:
                    break;
            }
            if (Y1 < MaxLevel)
            {
                m_HouseUp.SetActive(false);
                m_House.SetActive(false);
                UpgradeTimer UpTime = new UpgradeTimer();
                string Str = m_building.name;
                UpTime.AddUpgraed(Str);
            }
            else if (Y1 >= MaxLevel)
            {
                warning Warning = new warning();
                string Str = "岛主当前建筑已经达到最高级，已经无法继续加盖了！";
                Warning.AddWarning(Str);
            }
            
        }
        else if (GlobalVar.GetInstance().UpgradeQueues == 1)
        {
            warning Warning = new warning();
            string WarningStr = "岛主我们目前只能升级一座建筑";
            Warning.AddWarning(WarningStr);
        }

    }

    void onItems(GameObject go)
    {
        warning Warning = new warning();
        string Str = "岛主当前没有可领取的金币，请先找点别的乐子，等会再来！";
        Warning.AddWarning(Str);
    }
}