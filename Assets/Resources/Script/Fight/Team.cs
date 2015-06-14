using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Team : MonoBehaviour {

    public GameObject TeamSelf;
    private List<GameObject> m_ShipList = new List<GameObject>();
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool AddShip(int nIndex)
    {
        GameObject prefab = Instantiate(Resources.Load("Prefab/Ships/Ship" + Convert.ToString(nIndex)), Vector3.zero, Quaternion.identity) as GameObject;
        if(prefab == null)
        {
            Debug.Log("Load Prefab/Ships/Ship" + nIndex + "failed");
            return false;
        }
        m_ShipList.Add(prefab);
        prefab.transform.parent = TeamSelf.transform;
        prefab.transform.localPosition = Vector3.zero;
        prefab.transform.localScale = Vector3.one;
        return true;
    }

    public bool AddShip(int nIndex, Vector3 pos, float speed,int type)
    {
		
        GameObject prefab = Instantiate(Resources.Load("Prefab/Ships/Ship" + Convert.ToString(nIndex)), Vector3.zero, Quaternion.identity) as GameObject;
        if (prefab == null)
        {
            Debug.Log("Load Prefab/Ships/Ship" + nIndex + "failed");
            return false;
        }
        m_ShipList.Add(prefab);
        prefab.transform.parent = TeamSelf.transform;
		prefab.transform.localPosition = pos;
        prefab.transform.localScale = Vector3.one;
        prefab.GetComponent<TweenPosition>().from = pos;
        pos.y += 5.0f;
        prefab.GetComponent<TweenPosition>().to = pos;
        prefab.GetComponent<TweenPosition>().duration = speed;
        prefab.GetComponent<ShipFight>().SetShellType(type);
        //Debug.Log("nIndex = " + nIndex + " pos = " + pos);
		//Debug.Log ("m_ShipList Count = " + m_ShipList.Count);
        return true;
    }

    public List<GameObject> GetShipList()
    {
        return m_ShipList;
    }
}
