using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Building : MonoBehaviour
{

    public List<GameObject> m_BaseList = new List<GameObject>();
    public GameObject m_HouseMenu;
    public GameObject m_CameraMap;
    public GameObject m_BObj;
    public GameObject m_Sale;
    public GameObject m_House;
    public GameObject m_BUManager;
    public GameObject m_BId;

    // Use this for initialization
    void Start()
    {
        UIEventListener.Get(m_House).onClick = onHouse;
        UIEventListener.Get(m_Sale).onClick = onSale;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void onHouse(GameObject go)
    {
        if (GlobalVar.GetInstance().UpObjname == null)
        {
            //Debug.Log("第一次点击建筑，打开自己！");
            m_BUManager.SetActive(true);
            m_House.GetComponent<TweenColor>().enabled = true;
            m_BUManager.GetComponent<Upgrade>().enabled = true;
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
                GlobalVar.GetInstance().UpObjname = null;
            }
            else
            {
                m_BUManager.SetActive(true);
                m_House.GetComponent<TweenColor>().enabled = true;
                m_BUManager.GetComponent<Upgrade>().enabled = true;

                Transform Tf = m_BObj.transform.parent.FindChild(GlobalVar.GetInstance().UpObjname);
                Tf.GetComponent<Building>().m_BUManager.SetActive(false);
                Tf.GetComponent<Building>().m_House.GetComponent<TweenColor>().enabled = false;
                Tf.GetComponent<Building>().m_House.GetComponent<TweenColor>().ResetToBeginning();
                Tf.GetComponent<Building>().m_BUManager.GetComponent<Upgrade>().enabled = true;
                GlobalVar.GetInstance().UpObjname = m_BObj.name;
            }
        }
    }
    void onSale(GameObject go)
    {
        if (GlobalVar.GetInstance().BuildObjname == null)
        {
            m_HouseMenu.SetActive(true);
            m_CameraMap.GetComponent<CameraDragMove>().enabled = false;
            m_CameraMap.GetComponent<ScalingMap>().enabled = false;
            GlobalVar.GetInstance().BuildObjname = m_BObj.name;
        }
        else
        {
            //关闭上次打开的
            Transform Tf = m_BObj.transform.parent.FindChild(GlobalVar.GetInstance().UpObjname);
            Tf.GetComponent<Building>().m_BUManager.SetActive(false);
            Tf.GetComponent<Building>().m_House.GetComponent<TweenColor>().enabled = false;
            Tf.GetComponent<Building>().m_House.GetComponent<TweenColor>().ResetToBeginning();
     

            //打开自己
            m_HouseMenu.SetActive(true);
            m_CameraMap.GetComponent<CameraDragMove>().enabled = false;
            m_CameraMap.GetComponent<ScalingMap>().enabled = false;
            GlobalVar.GetInstance().BuildObjname = m_BObj.name;
        }
    }
}