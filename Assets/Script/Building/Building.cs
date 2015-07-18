using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : MonoBehaviour {

    //button list
    //public List<GameObject> m_ButList = new List<GameObject>();
    public GameObject m_Sale;
    public GameObject m_House;
    public GameObject m_UpIcon;
    public GameObject m_HouseUP;
    public GameObject m_ProgressBar;

    public GameObject m_building;
    public GameObject m_HouseMenu;
   
    bool H_UDswitch ; //update switch
    bool H_HUswitch = false; //house up switch
    string H_BuildObjName;
	// Use this for initialization
	void Start () 
    {
        H_HUswitch = false;
        if (m_House.transform.GetComponentInChildren<UISprite>().atlas != null)
        {
            m_Sale.SetActive(false);
        }
        UIEventListener.Get(m_Sale).onClick = onSale;
        UIEventListener.Get(m_House).onClick = onHouse;
        //UIEventListener.Get(m_UpIcon).onClick = onUpIcon;
        //UIEventListener.Get(m_HouseUP).onClick = onHouseUP;
        //UIEventListener.Get(m_ProgressBar).onClick = onProgressBar;
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
        H_UDswitch = true;
        m_HouseMenu.GetComponent<HouseMenu>().H_HCStatus = true;
        m_HouseMenu.GetComponent<HouseMenu>().H_HouseIconAtlas = null;
        m_HouseMenu.SetActive(true);
    }
    void onHouse(GameObject go)
    {
        if (H_HUswitch == false)
        {
            H_HUswitch = true;
            m_HouseUP.SetActive(true);
            m_House.GetComponent<TweenColor>().enabled = true;
            
        }
        else
        {
            H_HUswitch = false;
            m_HouseUP.SetActive(false);
            m_House.GetComponent<TweenColor>().enabled = false;
            m_House.GetComponent<TweenColor>().ResetToBeginning();
        }
    }    
}