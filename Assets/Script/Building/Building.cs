using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : MonoBehaviour {

    //button list
    public List<GameObject> ButList = new List<GameObject>();

    public GameObject m_Sale;
    public GameObject m_House;
    public GameObject m_UpIcon;
    public GameObject m_Progressbar;   
    public GameObject m_HouseMenu;
    public GameObject m_Builing;

    private UIAtlas LHIA;
    private int SHC;
    private int GOGHC;
    private bool SetOpt;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < ButList.Count; i++)
        {
            UIEventListener.Get(ButList[i]).onClick = onButList;
        }
        
	}
	
	// Update is called once per frame
    void Update()
    {
        setNA();
    }

    void onButList(GameObject go)
    {
        for (int i = 0; i < ButList.Count; i++)
        {
            if (ButList[i].GetHashCode() == go.gameObject.GetHashCode())
            {
                GOGHC = go.gameObject.GetHashCode();
                PlayerDataManager.GetInstance().SetSaleHashCode(ButList[i].GetHashCode());
                Debug.Log("SHC==" + ButList[i].GetHashCode());
                Debug.Log("GOGHC=="+GOGHC);
                switch (i)
                {
                    case 0: //Sale
                        SetOpt = true;
                        m_HouseMenu.SetActive(true);
                        Debug.Log("i =" + i); 
                        break;
                    case 1: //house
                        Debug.Log("GetHashCode" + ButList[i].GetHashCode());
                        break;
                    case 2: //upicon
                        break;
                    case 3: //progressbar
                        break;
                    case 4: //
                        break;
                    default:
                        break;
                }
                Debug.Log(i);
            }
        }
    }

    void setNA()
    {
        if (SetOpt == true)
        {
            SHC = PlayerDataManager.GetInstance().m_salehashcode;
            if (SHC == GOGHC)
            {
                LHIA = m_Builing.transform.FindChild("House").GetComponent<UISprite>().atlas;
                Debug.Log("LHIA ==" + LHIA);
                if (LHIA == null)
                {
                    m_Builing.transform.FindChild("House").GetComponent<UISprite>().atlas = PlayerDataManager.GetInstance().m_houseatlas;
                    m_Builing.transform.FindChild("House").GetComponent<UISprite>().spriteName = PlayerDataManager.GetInstance().m_houseicon;
                    Debug.Log("有名字就表示接通了：" + m_Builing.transform.FindChild("House").GetComponent<UISprite>().spriteName);
                    Debug.Log("有图集就表示接通了：" + m_Builing.transform.FindChild("House").GetComponent<UISprite>().atlas);
                    UIAtlas HIA=null;
                    PlayerDataManager.GetInstance().SetHouseAtlas(HIA);
                }
                else 
                { 
                    SetOpt = false;
                    m_Sale.SetActive(false);
                }
            }                
            else { m_Sale.SetActive(false); }  
        }
    }
}