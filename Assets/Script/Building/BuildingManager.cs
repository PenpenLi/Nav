 using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NetManager;
using System;
using System.Linq;

public class BuildingManager : MonoBehaviour
{
    // User GameObject
    public List<GameObject> m_BuildingList = new List<GameObject>();
    public GameObject m_BMHouseMenu;
    public GameObject m_BMCamera;

    //资源列表
    public List<UILabel> m_ResList = new List<UILabel>();

    //本地数据
    List<BuildingInfo> m_BI = MyApp.GetInstance().GetDataManager().BI();
    List<bbase> m_BB = MyApp.GetInstance().GetDataManager().BB();
    List<Item> m_Item = MyApp.GetInstance().GetDataManager().Item();
    List<BuildingPos> m_BP = MyApp.GetInstance().GetDataManager().BP();

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < m_BI.Count; i++)
        {
           m_BuildingList.Add(NetAddBuild(i));
        }
    }

    // Update is called once per frame
    void Update()//null
    {

    }

    GameObject NetAddBuild(int BInd) //添加建筑
    {
        GameObject m_BObj = Instantiate(Resources.Load("Prefab/Building/Building")) as GameObject;//get perfab
        m_BObj.transform.parent = GameObject.Find("BuildingManager").transform;//set building parent
        m_BObj.name = m_BI[BInd].POS_ID.ToString();
        m_BObj.transform.localScale = Vector3.one;

        ////根据坐标索引查找并设置建筑坐标
        float Px = (float)m_BP[m_BI[BInd].POS_ID].PosX; //Debug.Log("Px → " + Px);
        float Py = (float)m_BP[m_BI[BInd].POS_ID].PosY; //Debug.Log("Py → " + Py);
        m_BObj.transform.localPosition = new Vector3(Px, Py, 0);
        //挂载perfab外部GameObject
        m_BObj.GetComponent<Building>().m_HouseMenu = m_BMHouseMenu;
        m_BObj.GetComponent<Building>().m_CameraMap = m_BMCamera;
        m_BObj.GetComponent<Building>().m_ResList = m_ResList;

        //将建筑ID记录在每座对应的建筑上,以备后续查找调用
        m_BObj.transform.FindChild("BuildingID").GetComponent<UILabel>().text = m_BI[BInd].B_ID.ToString();

        //判断当前位置是否已建造过建筑，-1 是无建筑
        if (m_BI[BInd].B_ID == -1)
        {
            m_BObj.transform.GetComponent<Building>().m_House.SetActive(false);
            m_BObj.transform.GetComponent<Building>().m_Sale.SetActive(true);
        }
        else
        {
            m_BObj.transform.GetComponent<Building>().m_House.SetActive(true);
            m_BObj.transform.GetComponent<Building>().m_Sale.SetActive(false);

            int BBID = m_BI[BInd].B_ID;
            //加载建筑信息
            UIAtlas UA = Resources.Load("Atlas/House/" + m_BB[BBID].Atlas, typeof(UIAtlas)) as UIAtlas;
            m_BObj.transform.FindChild("House").GetComponent<UISprite>().atlas = UA;
            m_BObj.transform.FindChild("House").GetComponent<UISprite>().spriteName = m_BB[BBID].Icon;

            m_BObj.transform.FindChild("BUManager").FindChild("HName").GetComponent<UILabel>().text = m_BB[BBID].Name;
            m_BObj.transform.FindChild("BUManager").FindChild("HLev").GetComponent<UILabel>().text = m_BB[BBID].Lv.ToString();

            int IID = m_BB[BBID].ItemID;
            //加载产物信息
            UIAtlas IUA = Resources.Load("Atlas/Lobby/" + m_Item[IID].Atlas, typeof(UIAtlas)) as UIAtlas;
            m_BObj.transform.FindChild("Items").FindChild("2").GetComponent<UISprite>().atlas = IUA;
            m_BObj.transform.FindChild("Items").FindChild("2").GetComponent<UISprite>().spriteName = m_Item[IID].Icon;
            m_BObj.transform.FindChild("Items").FindChild("ItemID").GetComponent<UILabel>().text = IID.ToString();
            m_BObj.transform.FindChild("Items").FindChild("ItemCount").GetComponent<UILabel>().text = m_BI[BInd].ITEMS_COUNT.ToString();

            //提示玩家可以收取产物
            if (m_BI[BInd].ITEMS_COUNT > 0)
            {
                m_BObj.GetComponent<Building>().m_Items.SetActive(true);
                m_BObj.GetComponent<ItemTimer>().enabled = true;
            }
            else
            {
                m_BObj.GetComponent<Building>().m_Items.SetActive(false);
            }
        }
        return m_BObj;
    }
}
