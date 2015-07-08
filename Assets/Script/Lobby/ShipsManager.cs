using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ShipsObject
{
    public int id;
    public GameObject ship;
}

public class ShipsManager : MonoBehaviour {
    List<ShipsObject> m_shipList = new List<ShipsObject>();
    public GameObject m_anchorShip;
    int m_shipIndex = 0;
    const int m_shipMax = 250;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
        GameObject ship = Resources.Load("AchorShip") as GameObject;
        ship.GetComponent<UISprite>().spriteName = "ani_ship_" + Convert.ToString(spriteid);
        ship.transform.localScale = Vector3.one;
        ship.transform.parent = transform;
        ShipsObject shipobj = new ShipsObject();
        while(!VerifyID(m_shipIndex))
        {
            m_shipIndex++;
        }
        shipobj.id = m_shipIndex;
        shipobj.ship = ship;
        m_shipList.Add(shipobj);
        return m_shipIndex;

    }
}
