using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ShipsObject
{
    public int id;
    public GameObject ship;
    public float waitSecond;
    public ShipState state;
}

public class ShipsManager : MonoBehaviour {
    List<ShipsObject> m_shipList = new List<ShipsObject>();
    public List<GameObject> m_anchorList = new List<GameObject>();
    //public GameObject m_anchorShip;
    int m_shipIndex = 0;
    const int m_shipMax = 250;
    float m_waitTime = 0.0f;
	// Use this for initialization
	void Start () {
        AddShip(1);
        AddShip(10);
        AddShip(12);
        AddShip(13);
        AddShip(15);
        AddShip(19);
        AddShip(21);
        AddShip(24);
        AddShip(4);
        AddShip(6);
        AddShip(7);
        AddShip(9);
	}
	
	// Update is called once per frame
	void Update () {
        LoopShipList();
	}

    bool VerifyID(int id)
    {
        for(int i = 0; i < m_shipList.Count; i++)
        {
            if(m_shipList[i].id == id)
            {
                return false;
            }
        }
        return true;
    }

    public int AddShip(int spriteid)
    {
        while(!VerifyID(m_shipIndex))
        {
            m_shipIndex++;
        }
        GameObject ship = Instantiate(Resources.Load("Prefab/Lobby/AchorShip")) as GameObject;
        ship.GetComponent<UISprite>().spriteName = "ani_ship_" + Convert.ToString(spriteid);
		ship.GetComponent<UISprite>().MakePixelPerfect();
        ship.transform.localScale = Vector3.one;
        ship.transform.SetParent(gameObject.transform, false);
        ShipsObject shipobj = new ShipsObject();
        shipobj.id = m_shipIndex;
        shipobj.ship = ship;
        shipobj.waitSecond = UnityEngine.Random.RandomRange(6.0f, 10.0f);
        Debug.Log("shipobj.waitSecond = " + shipobj.waitSecond);
        shipobj.state = ShipState.Begin;
        m_shipList.Add(shipobj);
        return m_shipIndex;
    }

    void LoopShipList()
    {
        for(int i = 0; i < m_shipList.Count; i++)
        {
            if (m_shipList[i].state == ShipState.Begin)
            {
                if (m_waitTime >= m_shipList[i].waitSecond)
                {
                    m_shipList[i].ship.SetActive(true);
                    m_shipList[i].state = ShipState.EnterPort;
                    m_waitTime = 0.0f;
                }
                
            }
        }
        m_waitTime = m_waitTime + Time.deltaTime;
        //Debug.Log("m_waitTime = " + m_waitTime);
        if(m_waitTime >= 1000000.0f)
        {
            m_waitTime = 0.0f;
        }
        //Debug.Log("m_waitTime = " + m_waitTime);
    }

    void DockShip()
    {
         for(int i = 0; i < m_shipList.Count; i++)
         {
             if (m_shipList[i].state == ShipState.Docking)
             {
                 m_shipList[i].ship.GetComponent<AchorShip>().SetDockParent(GetFreeAchor());
                 Transform tf = m_shipList[i].ship.transform.FindChild("Gold");
                 if(tf != null)
                 {
                     tf.gameObject.SetActive(true);
                 }
             }
         }
    }

    public GameObject GetFreeAchor()
    {
        for(int i = 0; i < m_anchorList.Count; i++)
        {
            //Debug.Log("i = " + i + " m_anchorList[i].transform.childCount = " + m_anchorList[i].transform.childCount);
            if(m_anchorList[i].transform.childCount== 0)
            {
                return m_anchorList[i];
            }
        }
        return null;
    }

    //处理入港队列

}
