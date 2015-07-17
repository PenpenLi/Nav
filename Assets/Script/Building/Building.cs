using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : MonoBehaviour {

    //button list
    public List<GameObject> m_ButList = new List<GameObject>();
    
    public GameObject m_HouseMenu;
   
    bool m_UDswitch ; //update switch
    bool m_HUswitch = false; //house up switch
    bool m_HUS;

	// Use this for initialization
	void Start () 
    {
        m_HUswitch = false;
        if (m_ButList[1].transform.GetComponentInChildren<UISprite>().atlas != null)
        {
           m_ButList[0].SetActive(false);
        }

        for (int i = 0; i < m_ButList.Count; i++)
        {
            UIEventListener.Get(m_ButList[i]).onClick = onButList;
        }
	}
	
	// Update is called once per frame
    void Update()
    {
        if (m_UDswitch == true)
        {
            if (m_ButList[1].transform.GetComponentInChildren<UISprite>().atlas == null)
            {
                m_ButList[1].transform.GetComponentInChildren<UISprite>().atlas = m_HouseMenu.GetComponent<HouseMenu>().m_HouseIconAtlas;
                m_ButList[1].transform.GetComponentInChildren<UISprite>().spriteName = m_HouseMenu.GetComponent<HouseMenu>().m_HouseIconName;
                if (m_ButList[1].transform.GetComponentInChildren<UISprite>().atlas != null)
                {
                    m_UDswitch = false;
                    m_ButList[0].SetActive(false);
                }

                if (m_HouseMenu.GetComponent<HouseMenu>().m_CStatus == false)
                {
                    m_UDswitch = false;
                }
            }
        }
        m_HUswitch = m_HUS;
    }

    void onButList(GameObject go)
    {
        for (int i = 0; i < m_ButList.Count; i++)
        {
            if (m_ButList[i].GetHashCode() == go.gameObject.GetHashCode())
            {
                switch (i)
                {
                    case 0: //Sale
                        m_UDswitch = true;
                        m_HouseMenu.GetComponent<HouseMenu>().m_CStatus = true;
                        m_HouseMenu.GetComponent<HouseMenu>().m_HouseIconAtlas = null;
                        m_HouseMenu.SetActive(true);
                        break;
                    case 1: //house
                        Debug.Log("m_HUswitch:"+m_HUswitch);
                        if (m_HUswitch == false)
                        {
                            //m_HUswitch = true;
                            m_HUS = true;
                            m_ButList[3].SetActive(true);
                            m_ButList[1].GetComponent<TweenColor>().enabled = true;
                        }
                        else
                        {
                            //m_HUswitch = false;
                            m_HUS = false;
                            m_ButList[3].SetActive(false);
                            m_ButList[1].GetComponent<TweenColor>().enabled = false;
                            m_ButList[1].GetComponent<TweenColor>().ResetToBeginning();
                        }
                        
                        break;
                    case 2: //upicon
                        break;
                    case 3: //HouseUP
                        
                        break;
                    case 4: //progressbar
                        break;
                    default:
                        break;
                }
            }
        }
    }    
}