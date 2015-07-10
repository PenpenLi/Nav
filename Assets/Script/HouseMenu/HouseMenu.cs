using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class HouseMenu : MonoBehaviour {

    public List<GameObject> m_ButLise = new List<GameObject>();

    public GameObject m_CloseBut;
    public GameObject m_HouseMenu;
    public GameObject m_Warning;

    public UILabel Inputsq;
    public UILabel m_GoldCount;

    int GoldCount = 999999999;
    //获得
    UIAtlas HouseIconAtlas;
    string HouseIconName;

    string STR;
    string Inp ;
    int Nun;
    int InSq;

	// Use this for initialization
	void Start () {
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
                switch (i)
                {
                    case 0:
                        getNA(i);
                        break;
                    case 1:
                        getNA(i);
                        break;
                    case 2:
                        getNA(i);
                        break;
                    case 3:
                        getNA(i);
                        break;
                    case 4:
                        getNA(i);
                        break;
                    default:
                        break;
                }
            }
        } 
    }

    void onClose(GameObject go)
    {
        m_HouseMenu.SetActive(false);
    }

    void getNA(int m_Index)
    {
        STR = m_GoldCount.text;
        Inp = Inputsq.text; 
        Nun = int.Parse(STR);
        InSq = int.Parse(Inp);
        if (InSq < Nun)
        {
            m_Warning.SetActive(true);
        }
        else
        {
            HouseIconName = m_ButLise[m_Index].transform.FindChild("HouseIcon").GetComponent<UISprite>().spriteName;
            HouseIconAtlas = m_ButLise[m_Index].transform.FindChild("HouseIcon").GetComponent<UISprite>().atlas;
            PlayerDataManager.GetInstance().SetHouseIconN(HouseIconName);
            PlayerDataManager.GetInstance().SetHouseAtlas(HouseIconAtlas);
            Debug.Log("参数已传入PDM，并且等于：" + PlayerDataManager.GetInstance().m_houseicon);
            m_HouseMenu.SetActive(false);
        }
    }
}
