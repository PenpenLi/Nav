using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipsObject
{
    public int id;
    public GameObject ship;
}

public class ShipsManager : MonoBehaviour {
    List<ShipsObject> m_shipList = new List<ShipsObject>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
