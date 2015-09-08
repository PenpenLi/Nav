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
    public List<GameObject> m_ResList = new List<GameObject>();

    //本地数据
    List<BuildingInfo> m_BI = MyApp.GetInstance().GetDataManager().BI();
    List<B_Base> m_BB = MyApp.GetInstance().GetDataManager().BB();
    List<Item> m_Item = MyApp.GetInstance().GetDataManager().Item();
    List<B_Pos> m_BP = MyApp.GetInstance().GetDataManager().BP();

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
        m_BObj.name = m_BI[BInd].POSID.ToString();
        m_BObj.transform.localScale = Vector3.one;

        //根据坐标索引查找并设置建筑坐标
        var Pos = from n in m_BP
                  where n.POS_ID == m_BI[BInd].POSID
                  select new { n.POS_X, n.POS_Y };
        foreach (var pos in Pos)
            m_BObj.transform.localPosition = new Vector3((float)pos.POS_X,(float)pos.POS_Y, 0);

        //挂载perfab外部GameObject
        m_BObj.GetComponent<Building>().m_HouseMenu = m_BMHouseMenu;
        m_BObj.GetComponent<Building>().m_CameraMap = m_BMCamera;
        m_BObj.GetComponent<Building>().m_BaseList = m_ResList;

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

            //更具建筑ID加载建筑数据
            var BInfo = from n in m_BB
                        where n.ID == m_BI[BInd].B_ID
                        select new { n.B_ATLAS_NAME, n.B_ICON_NAME, n.B_NAME, n.B_LEV, n.ITEM_ID };
            foreach (var BBase in BInfo)
            {
                UIAtlas UA = Resources.Load("Atlas/House/" + BBase.B_ATLAS_NAME, typeof(UIAtlas)) as UIAtlas;
                m_BObj.transform.FindChild("House").GetComponent<UISprite>().atlas = UA;
                m_BObj.transform.FindChild("House").GetComponent<UISprite>().spriteName = BBase.B_ICON_NAME;

                m_BObj.transform.FindChild("BUManager").FindChild("HName").GetComponent<UILabel>().text = BBase.B_NAME;
                m_BObj.transform.FindChild("BUManager").FindChild("HLev").GetComponent<UILabel>().text = BBase.B_LEV.ToString();

                var IInfo = from m in m_Item
                            where m.ITEM_ID == BBase.ITEM_ID
                            select new { m.ITEM_ATLAS_NAME, m.ITEM_ICON_NAME };
                foreach (var IBase in IInfo)
                {
                    UIAtlas IUA = Resources.Load("Atlas/Lobby/" + IBase.ITEM_ATLAS_NAME, typeof(UIAtlas)) as UIAtlas;
                    m_BObj.transform.FindChild("BUManager").FindChild("Items").FindChild("2").GetComponent<UISprite>().atlas = IUA;
                    m_BObj.transform.FindChild("BUManager").FindChild("Items").FindChild("2").GetComponent<UISprite>().spriteName = IBase.ITEM_ICON_NAME;
                }
            }

        }
        return m_BObj;
    }
}
