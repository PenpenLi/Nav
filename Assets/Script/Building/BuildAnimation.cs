using UnityEngine;
using System.Collections;

public class BuildAnimation : MonoBehaviour {

    public GameObject m_BuildAnimation;
    public GameObject m_Start;
    public GameObject m_Center;
    public GameObject m_TheEnd;
    public GameObject m_TimeLine1;
    public GameObject m_HouseName;
    public GameObject m_Buy;
	// Use this for initialization
	void Start () 
    {
        UIEventListener.Get(m_Start).onClick = onStart;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void onStart(GameObject go)
    {
        m_HouseName.SetActive(true);
        m_Buy.SetActive(true);
        m_TimeLine1.transform.localPosition = new Vector3(0f,-80f,0f);
    }
}
