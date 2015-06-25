using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Architecture : MonoBehaviour {

    public GameObject m_Close;
    public GameObject m_Architecture;

	// Use this for initialization
	void Start () {
        UIEventListener.Get(m_Close).onClick = onClose;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void onClose(GameObject go)
    {
        m_Architecture.SetActive(false);
    }
}
