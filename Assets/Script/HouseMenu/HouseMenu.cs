using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class HouseMenu : MonoBehaviour {

    public List<GameObject> m_ButLise = new List<GameObject>();

    public GameObject m_CloseBut;
    public GameObject m_HouseMenu;
    public GameObject m_Warning;

    int GoldCount = 999999999;

    //获得
    public UIAtlas m_HouseIconAtlas;
    public string m_HouseIconName;
    public bool m_CStatus;//building update switch


	// Use this for initialization
	void Start () 
    {
        for (int i = 0; i < m_ButLise.Count; i++)
        {
            UIEventListener.Get(m_ButLise[i]).onClick = onButList;
        }
        UIEventListener.Get(m_CloseBut).onClick = onClose;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void onButList(GameObject go)
    {
        for (int i = 0; i < m_ButLise.Count; i++)
        {
            
            if (m_ButLise[i].GetHashCode() == go.gameObject.GetHashCode())
            {
                m_HouseIconName = m_ButLise[i].transform.FindChild("HouseIcon").GetComponent<UISprite>().spriteName;
                m_HouseIconAtlas = m_ButLise[i].transform.FindChild("HouseIcon").GetComponent<UISprite>().atlas;
                m_HouseMenu.SetActive(false);
                m_CStatus = false;
            }
        } 
    }

    void onClose(GameObject go)
    {
        m_HouseMenu.SetActive(false);
        m_CStatus =false;

    }
}
