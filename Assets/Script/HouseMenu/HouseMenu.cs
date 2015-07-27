using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class HouseMenu : MonoBehaviour {

    public List<GameObject> m_ButLise = new List<GameObject>();

    public GameObject m_CloseBut;
    public GameObject m_HouseMenu;
    public GameObject m_Warning;
    public GameObject m_InputGold;
    public GameObject m_HMCamera;

    int GoldCount = 999999999;

    //获得
    public UIAtlas H_HouseIconAtlas;
    public string H_HouseIconName;
    public bool H_HCStatus;//building update switch

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
                Debug.Log("你已选好目标建筑了！");
                long a =long.Parse(m_ButLise[i].transform.parent.FindChild("Sell").FindChild("MoneyCount").GetComponentInChildren<UILabel>().text);
                long b =long.Parse(m_InputGold.transform.FindChild("TestGold").GetComponent<UILabel>().text);
                Debug.Log (a +"@@@"+b);
                if (a < b )
                {
                    H_HouseIconName = m_ButLise[i].transform.parent.FindChild("HouseIcon").GetComponent<UISprite>().spriteName;
                    H_HouseIconAtlas = m_ButLise[i].transform.parent.FindChild("HouseIcon").GetComponent<UISprite>().atlas;
                    m_HouseMenu.SetActive(false);
                    H_HCStatus = false;
                    m_InputGold.transform.FindChild("TestGold").GetComponent<UILabel>().text = (b -a).ToString();
                }
                else
                { m_Warning.SetActive(true); }
            }
        } 
    }

    void onClose(GameObject go)
    {
        m_HouseMenu.SetActive(false);
        H_HCStatus =false;
        m_HMCamera.GetComponent<CameraDragMove>().enabled = true;
        m_HMCamera.GetComponent<ScalingMap>().enabled = true;

    }
}
