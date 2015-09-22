using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Building : MonoBehaviour
{
    public List<UILabel> m_ResList = new List<UILabel>();

    public GameObject m_HouseMenu;
    public GameObject m_CameraMap;

    public GameObject m_BObj;
    public GameObject m_Sale;
    public GameObject m_House;
    public GameObject m_BUManager;
    public GameObject m_BId;

    public GameObject m_Items;
    public GameObject m_ItemID;
    public GameObject m_ItemCount;
 
    // Use this for initialization
    void Start()
    {
        UIEventListener.Get(m_House).onClick = onHouse;
        UIEventListener.Get(m_Sale).onClick = onSale;
        UIEventListener.Get(m_Items).onClick = onItems;
    }

    // Update is called once per frame
    void Update()
    {
        int Itcount = Convert.ToInt32(m_ItemCount.GetComponent<UILabel>().text);
        //Debug.Log("Itcount → " + m_ItemCount.GetComponent<UILabel>().text);
        if (Itcount > 10)
        {
            m_Items.SetActive(true);
        }
    }
    void onHouse(GameObject go)
    {
        if (GlobalVar.GetInstance().UpObjname == null)
        {
            //Debug.Log("第一次点击建筑，打开自己！");
            m_BUManager.SetActive(true);
            m_House.GetComponent<TweenColor>().enabled = true;
            m_BUManager.GetComponent<Upgrade>().enabled = true;
            m_Items.transform.localPosition = new Vector3(78f,93f,0);
            GlobalVar.GetInstance().UpObjname = m_BObj.name;
        }
        else
        {
            if (GlobalVar.GetInstance().UpObjname == m_BObj.name)
            {
                //Debug.Log("关闭自己！");
                m_BUManager.SetActive(false);
                m_House.GetComponent<TweenColor>().enabled = false;
                m_House.GetComponent<TweenColor>().ResetToBeginning();
                m_BUManager.GetComponent<Upgrade>().enabled = false;
                m_Items.transform.localPosition = new Vector3(18f, 93f, 0);
                GlobalVar.GetInstance().UpObjname = null;
            }
            else
            {
                m_BUManager.SetActive(true);
                m_House.GetComponent<TweenColor>().enabled = true;
                m_BUManager.GetComponent<Upgrade>().enabled = true;
                m_Items.transform.localPosition = new Vector3(78f, 93f, 0);

                Transform Tf = m_BObj.transform.parent.FindChild(GlobalVar.GetInstance().UpObjname);
                Tf.GetComponent<Building>().m_BUManager.SetActive(false);
                Tf.GetComponent<Building>().m_House.GetComponent<TweenColor>().enabled = false;
                Tf.GetComponent<Building>().m_House.GetComponent<TweenColor>().ResetToBeginning();
                Tf.GetComponent<Building>().m_BUManager.GetComponent<Upgrade>().enabled = true;
                Tf.GetComponent<Building>().m_Items.transform.localPosition = new Vector3(18f, 93f, 0);
                GlobalVar.GetInstance().UpObjname = m_BObj.name;
            }
        }
    }
    void onSale(GameObject go)
    {
        if (GlobalVar.GetInstance().UpObjname != null)
        {
            Transform Tf = m_BObj.transform.parent.Find(GlobalVar.GetInstance().UpObjname);
            Debug.Log("Tf名字 = " + Tf.name);
            Tf.GetComponent<Building>().m_BUManager.SetActive(false);
            Tf.GetComponent<Building>().m_Items.transform.localPosition = new Vector3(18f, 93f, 0);
            Tf.GetComponent<Building>().m_House.GetComponent<TweenColor>().enabled = false;
            Tf.GetComponent<Building>().m_House.GetComponent<TweenColor>().ResetToBeginning();
            GlobalVar.GetInstance().UpObjname = null;
        }
        
            m_HouseMenu.SetActive(true);
            m_CameraMap.GetComponent<CameraDragMove>().enabled = false;
            m_CameraMap.GetComponent<ScalingMap>().enabled = false;
            GlobalVar.GetInstance().BuildObjname = m_BObj.name;
       
        
    }
    void onItems(GameObject go)
    {
       
    }
}