using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ItemTimer : MonoBehaviour {

    public GameObject m_Items;
    public GameObject m_ItemID;
    public GameObject m_ItemCount;
    public GameObject m_BID;

    private int IID;
    private Double ICount;
    private Double Diff;
    private int BID;
    private List<B_Base> m_MyBB = new List<B_Base>();

    private long Dt;
	// Use this for initialization
	void Start () {
        Debug.Log("生产开始！！！！！！");
        m_MyBB = MyApp.GetInstance().GetDataManager().BB();
        Dt = DateTime.Now.Ticks;
        UIEventListener.Get(m_Items).onClick = onItems;
	}
	
	// Update is called once per frame
	void Update () {
        BID = Convert.ToInt32(m_BID.GetComponent<UILabel>().text);
        Debug.Log(m_ItemCount.GetComponent<UILabel>().text);
        ICount = Convert.ToInt32(m_ItemCount.GetComponent<UILabel>().text);
        
        Diff = Convert.ToDouble(m_MyBB[BID].ITEMS_MAX_COUNT / m_MyBB[BID].ITEMS_NEED_TIME);

        if (Dt ==0)
        {
            Dt = DateTime.Now.Ticks;
        }
        
        if (ICount > m_MyBB[BID].ITEMS_MAX_COUNT)
        {
            Debug.Log("产物长满了，关闭当前脚本！！！");
            m_ItemCount.GetComponent<UILabel>().text = m_MyBB[BID].ITEMS_MAX_COUNT.ToString();
            m_Items.transform.parent.GetComponent<ItemTimer>().enabled = false;
            Dt = 0;
        }
        else
        {
            long DTdiff = (DateTime.Now.Ticks - Dt) / 10000000;

            if (DTdiff == 1)
            {
                ICount += Diff;

                Dt = DateTime.Now.Ticks;
                m_ItemCount.GetComponent<UILabel>().text = ICount.ToString();
            }
            else if (DTdiff > 1)
            {
                Double ItCount = DTdiff * Diff;
                m_ItemCount.GetComponent<UILabel>().text = (ICount + ItCount).ToString();
                Dt = DateTime.Now.Ticks;
            }
        }
	}

    void onItems(GameObject go)
    {
        m_ItemCount.GetComponent<UILabel>().text = "0";
        m_Items.transform.parent.GetComponent<ItemTimer>().enabled = true;
        m_Items.SetActive(false);
        
    }
}
