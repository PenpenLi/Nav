using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class HouseBut : MonoBehaviour {


    public GameObject m_BObj;
    public GameObject m_House;
    public GameObject m_UpgraedManager;
    

	// Use this for initialization
	void Start () 
    {
        UIEventListener.Get(m_House).onClick = onHouse;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void onHouse(GameObject go)
    {
        if (GlobalVar.GetInstance().Bobjname == null)
        {
            //Debug.Log("第一次点击建筑，打开自己！");
            m_UpgraedManager.SetActive(true);
            m_House.GetComponent<TweenColor>().enabled = true;
            GlobalVar.GetInstance().Bobjname = m_BObj.name;
            m_UpgraedManager.GetComponent<BuildingUpgradeManager>().enabled = true;
        }
        else
        {
            if (GlobalVar.GetInstance().Bobjname == m_BObj.name)
            {
                //Debug.Log("关闭自己！");
                m_UpgraedManager.SetActive(false);
                m_House.GetComponent<TweenColor>().enabled = false;
                m_House.GetComponent<TweenColor>().ResetToBeginning();
                GlobalVar.GetInstance().Bobjname = null;
                m_UpgraedManager.GetComponent<BuildingUpgradeManager>().enabled = false;
            }
            else
            {
                //Debug.Log("关闭上次打开的！");
                CloseOldOpened(GlobalVar.GetInstance().Bobjname);

                //Debug.Log("//打开自己！");
                m_UpgraedManager.SetActive(true);
                m_House.GetComponent<TweenColor>().enabled = true;
                GlobalVar.GetInstance().Bobjname = m_BObj.name;
            }
        }
    }
    private void CloseOldOpened(string ObjStr)
    {
        Transform Tf = m_BObj.transform.parent.FindChild(ObjStr);
        Tf.GetComponent<Building>().m_UpgraedManager.SetActive(false);
        Tf.GetComponent<Building>().m_House.GetComponent<TweenColor>().enabled = false;
        Tf.GetComponent<Building>().m_House.GetComponent<TweenColor>().ResetToBeginning();
        Tf.GetComponent<Building>().m_UpgraedManager.GetComponent<BuildingUpgradeManager>().enabled = false;
    }
}
