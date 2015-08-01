using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : MonoBehaviour
{
    public GameObject m_Sale;
    public GameObject m_House;
    public GameObject m_UpIcon;

    public GameObject m_HouseUP;
    public GameObject m_UpInfo;
    public GameObject m_Upgrade;
    public GameObject m_Items;

    public GameObject m_BuildInfo;
    public GameObject m_TimeLine1;
    public GameObject m_HouseName;
    public GameObject m_Buy;

    public GameObject m_BuildAnimation;
    public GameObject m_Statr;
    public GameObject m_Center;
    public GameObject m_End;
    public GameObject m_building;
    public GameObject m_HouseMenu;
    public GameObject m_Camera;

    bool H_UDswitch= false; //update swicth
    //bool H_HUswitch = false; //house up switch
    int H_Switch;
    string  H_SaveHouseName;
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
        H_UDswitch = true;
        m_House.SetActive(true);
        m_HouseMenu.GetComponent<HouseMenu>().H_HCStatus = true;
        m_HouseMenu.GetComponent<HouseMenu>().H_HouseIconAtlas = null;
        m_HouseMenu.SetActive(true);
        
        if (GlobalVar.GetInstance().Bobjname != null)
        {
            m_building.transform.parent.FindChild(GlobalVar.GetInstance().Bobjname).GetComponent<Building>().m_HouseUP.SetActive(false);
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
                m_HouseUP.transform.FindChild("UpInfo").localPosition = new Vector3(-19.0f, 0f, 0f);
                m_HouseUP.transform.FindChild("Upgrade").localPosition = new Vector3(41.0f, 0f, 0f);
                break;
        }

        //Close and open building upgrade button UI
        if (H_SaveHouseName != null)
        {
            if (H_SaveHouseName == m_building.name)
            {
                if (H_Switch == -1)
                {
                    m_HouseUP.SetActive(false);
                    m_House.GetComponent<TweenColor>().enabled = false;
                    m_House.GetComponent<TweenColor>().ResetToBeginning();
                    GlobalVar.GetInstance().BobjS = 1;
                }
                else
                {
                    m_HouseUP.SetActive(true);
                    m_House.GetComponent<TweenColor>().enabled = true;
                    GlobalVar.GetInstance().BobjS = -1;
                }
            }
            else
            {
                m_building.transform.parent.FindChild(GlobalVar.GetInstance().Bobjname).GetComponent<Building>().m_HouseUP.SetActive(false);
                m_building.transform.parent.FindChild(GlobalVar.GetInstance().Bobjname).GetComponent<Building>().m_House.GetComponent<TweenColor>().enabled = false;
                m_building.transform.parent.FindChild(GlobalVar.GetInstance().Bobjname).GetComponent<Building>().m_House.GetComponent<TweenColor>().ResetToBeginning();
                m_HouseUP.SetActive(true);
                m_House.GetComponent<TweenColor>().enabled = true;
                GlobalVar.GetInstance().Bobjname = m_building.name;
                GlobalVar.GetInstance().BobjS = -1;
            }
        }
        else
        {
            m_HouseUP.SetActive(true);
            m_House.GetComponent<TweenColor>().enabled = true;
            GlobalVar.GetInstance().Bobjname = m_building.name;
            GlobalVar.GetInstance().BobjS = -1;
        }
    }


    void onUpgrade(GameObject go)
    {
        Debug.Log("你点击了升级确定按钮→Upgrade！");
        m_HouseUP.SetActive(false);
        m_House.GetComponent<TweenColor>().enabled = false; 
        m_House.GetComponent<TweenColor>().ResetToBeginning();
        m_House.SetActive(false);
        m_Statr.SetActive(true);
        m_TimeLine1.SetActive(true);
        m_BuildInfo .GetComponent<TimerLine>().enabled = true;
    }

    void onItems(GameObject go)
    {
        warning Warning = new warning();
        string Str = "岛主当前没有可领取的金币，请先找点别的乐子，等会再来！";
        Warning.AddWarning(Str);
    }
}