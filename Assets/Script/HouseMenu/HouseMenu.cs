using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;


public class HouseMenu : MonoBehaviour
{

    public List<GameObject> m_BList = new List<GameObject>(); //建筑list
    public List<GameObject> m_ResList = new List<GameObject>();
    public GameObject m_CloseBut;
    public GameObject m_HouseMenu;
    public GameObject m_CameraMap;
    public GameObject m_Gold;
    public GameObject m_HouseInfo;


    // Use this for initialization
    void Start()
    {
        float PosX = -272;
        int BListCount = (from n in MyApp.GetInstance().GetDataManager().BB()
                          where n.Lv == 1
                          select n).Count();
        Debug.Log("等级为一的数据个数:" + BListCount);
        for (int i = 0; i < BListCount; i++)
        {

            PosX += 272;
            Vector3 Vt = new Vector3(PosX - 272, -17f, 0);

            m_BList.Add(NetAddBuild(i, Vt));
        }
        UIEventListener.Get(m_CloseBut).onClick = onClose;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void onClose(GameObject go)
    {
        m_HouseMenu.SetActive(false);
        m_CameraMap.GetComponent<CameraDragMove>().enabled = true;
        m_CameraMap.GetComponent<ScalingMap>().enabled = true;
        GlobalVar.GetInstance().BuildObjname = null;
    }

    GameObject NetAddBuild(int BInd, Vector3 VT) //添加Item perfab
    {
        GameObject m_Obj = Instantiate(Resources.Load("Prefab/HouseMenu/BuildingList")) as GameObject;
        m_Obj.transform.parent = GameObject.Find("HouseList").transform;
        m_Obj.transform.localScale = Vector3.one;
        m_Obj.transform.localPosition = VT;
        m_Obj.name = "B" + BInd.ToString();

        //挂在外部对象
        m_Obj.GetComponent<BuyBuilding>().m_HouseMenu = m_HouseMenu;
        m_Obj.GetComponent<BuyBuilding>().m_CameraMap = m_CameraMap;
        m_Obj.GetComponent<BuyBuilding>().m_ResList = m_ResList;
        
        var BList = from n in MyApp.GetInstance().GetDataManager().BB()
                    where n.BType == BInd
                    where n.Lv == 1
                    select new { B_ATLAS_NAME = n.Atlas, B_ICON_NAME = n.Icon, B_NAME = n.Name,BUILD_STR = n.InfoStr,n.ID  };
        foreach (var BLi in BList)
        {
            //Debug.Log("BLi.Batlasname = " + BLi.Batlasname);
            //Debug.Log("BLi.Biconname = " + BLi.Biconname);

            UIAtlas UA = Resources.Load("Atlas/House/" + BLi.B_ATLAS_NAME, typeof(UIAtlas)) as UIAtlas;
            m_Obj.transform.FindChild("HouseIcon").GetComponent<UISprite>().atlas = UA;
            m_Obj.transform.FindChild("HouseIcon").GetComponent<UISprite>().spriteName = BLi.B_ICON_NAME;
            m_Obj.transform.FindChild("HouseName").GetComponent<UILabel>().text = BLi.B_NAME; ;
            m_Obj.transform.FindChild("BID").GetComponentInChildren<UILabel>().text = BLi.ID.ToString();
            m_Obj.transform.FindChild("HouseInfo").GetComponent<UILabel>().text = BLi.BUILD_STR;
        }

        return m_Obj;
    }

}
