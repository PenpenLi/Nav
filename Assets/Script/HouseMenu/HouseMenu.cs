using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class HouseMenu : MonoBehaviour {

    public List<GameObject> m_ButLise = new List<GameObject>();

    public GameObject m_CloseBut;
    public GameObject m_HouseMenu;
    public GameObject m_Warning;

    public UISprite  ceshi;
    public UILabel Inputsq;
    public UILabel m_GoldCount;

    private int GoldCount = 999999999;
    //获得
    private UIAtlas HouseIconAtlas;
    private string HouseIconName;

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
        string STR = m_GoldCount.text;
        string Inp = Inputsq.text; 
        int NO = int.Parse(STR);
        int InSq = int.Parse(Inp);
        for (int i = 0; i < m_ButLise.Count; i++)
        {
            
            if (m_ButLise[i].GetHashCode() == go.gameObject.GetHashCode())
            {
                switch (i)
                {
                    case 0:
                        Debug.Log(NO);
                        if (InSq < NO)
                        {
                            m_Warning.SetActive(true);
                        }
                        else
                        {
                            Debug.Log(NO);
                            HouseIconName = m_ButLise[i].transform.FindChild("HouseIcon").GetComponent<UISprite>().spriteName;
                            HouseIconAtlas = m_ButLise[i].transform.FindChild("HouseIcon").GetComponent<UISprite>().atlas;
                            ceshi.atlas = HouseIconAtlas;
                            ceshi.spriteName = HouseIconName;
                            
                        }
                        
                        break;
                    case 1:
                        
                        
                        break;
                    case 2:
                        
                        
                        break;
                    case 3:

                        
                        break;
                    case 4:
                        
                        
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

    
}
