using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Building : MonoBehaviour
{
    public GameObject m_Sale;
    public GameObject m_House;
    //public GameObject m_UpIcon;

    public GameObject m_HouseUp;
    public GameObject m_UpInfo;
    public GameObject m_Upgrade;
    public GameObject m_Items;

    public GameObject m_building;
    public GameObject m_HouseMenu;
    public GameObject m_CameraMap;


    bool H_UDswitch= false; //update swicth
    //bool H_HUswitch = false; //house up switch
    int H_Switch;
    string  H_SaveHouseName;
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
        
        if (H_UDswitch == true)
        {
            if (m_House.transform.GetComponentInChildren<UISprite>().atlas == null)
            {
                m_House.transform.GetComponentInChildren<UISprite>().atlas = m_HouseMenu.GetComponent<HouseMenu>().H_HouseIconAtlas;
                m_House.transform.GetComponentInChildren<UISprite>().spriteName = m_HouseMenu.GetComponent<HouseMenu>().H_HouseIconName;
                if (m_House.transform.GetComponentInChildren<UISprite>().atlas != null)
                {
                    H_UDswitch = false;
                    m_Sale.SetActive(false);
                }

                if (m_HouseMenu.GetComponent<HouseMenu>().H_HCStatus == false)
                {
                    H_UDswitch = false;
                }
            }
        }
    }
    void onSale(GameObject go)
    {
        Debug.Log("当前perfab的名字是→ " + m_building.name);
        m_CameraMap.GetComponent<CameraDragMove>().enabled = false;
        m_CameraMap.GetComponent<ScalingMap>().enabled = false;
        H_UDswitch = true;
        m_House.SetActive(true);
        m_HouseMenu.GetComponent<HouseMenu>().H_HCStatus = true;
        m_HouseMenu.GetComponent<HouseMenu>().H_HouseIconAtlas = null;
        m_HouseMenu.SetActive(true);
        
        if (GlobalVar.GetInstance().Bobjname != null)
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
        H_Switch = GlobalVar.GetInstance().BobjS;
        H_SaveHouseName = GlobalVar.GetInstance().Bobjname;
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
        if (H_SaveHouseName != null)
        {
            if (H_SaveHouseName == m_building.name)
            {
                if (H_Switch == -1)
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
        string S = m_House.GetComponentInChildren<UISprite>().spriteName;
        char[] S1 = S.ToCharArray();
        string Y = Convert.ToString(S1[S1.Length - 1]);
        int Y1 = Convert.ToInt32(Y);
        Debug.Log("char Y =" + Y);
        Debug.Log("char Y =" + Y1);
        switch (Y1)
        {
            case 1:
                GlobalVar.GetInstance().UpgradeTime = 9;
                break;
            case 2:
                GlobalVar.GetInstance().UpgradeTime = 18;
                break;
            case 3:
                GlobalVar.GetInstance().UpgradeTime = 27;
                break;
            case 4:
                GlobalVar.GetInstance().UpgradeTime = 36;
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

    void onItems(GameObject go)
    {
        warning Warning = new warning();
        string Str = "岛主当前没有可领取的金币，请先找点别的乐子，等会再来！";
        Warning.AddWarning(Str);
    }
}