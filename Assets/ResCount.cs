using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResCount : MonoBehaviour {

    public List<UILabel> m_ResCount;

	// Use this for initialization
	void Start () 
    {
        m_ResCount[0].text = PlayerDataManager.GetInstance().m_PI.GNu.ToString();
        m_ResCount[1].text = PlayerDataManager.GetInstance().m_PI.RumNu.ToString();
        m_ResCount[2].text = PlayerDataManager.GetInstance().m_PI.PNu.ToString();
        m_ResCount[3].text = PlayerDataManager.GetInstance().m_PI.IronNn.ToString();
        m_ResCount[4].text = PlayerDataManager.GetInstance().m_PI.LignumNu.ToString();
        m_ResCount[5].text = PlayerDataManager.GetInstance().m_PI.GemNn.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
