using UnityEngine;
using System.Collections;

public class FPS : MonoBehaviour {

	// Use this for initialization
	float m_time=0;
	int  m_count=0;
	int  m_fps = 0;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		m_time +=RealTime.deltaTime;
		m_count ++;
		if(m_time>=1.0f)
		{
			m_fps =(int) (m_count/m_time);
			m_time = 0.0f;
			m_count = 0;
		}

		GetComponent<UILabel>().text = m_fps.ToString();


	}
}
