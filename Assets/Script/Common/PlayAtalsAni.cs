//作者：鲁力源
//时间：2015/06/22 23：21
//功能：播放Atals动画通用类
//备注：
using UnityEngine;
using System.Collections;
using System;

public class PlayAtalsAni : MonoBehaviour {
    public string m_spriteName = "";
    public bool m_bIsFnish = false;
    public int m_indexSprite = 1;
    public int m_maxSprite;
    public int m_playSpeed = 5;//一秒播几帧
    public bool m_bLoop = false;
    public int m_typeSprite = 1;
    float m_speed;
    TimeSpan m_ts;
    TimeSpan m_lastts;
    public bool m_play;
	// Use this for initialization
	void Start () {
        Reset();
	}

    void Reset()
    {
        m_lastts = new TimeSpan(DateTime.Now.Ticks);
        m_speed = 1000 / m_playSpeed;
        //m_bIsFnish = false;
        //m_bLoop = false;

    }
	
	// Update is called once per frame
	void Update () {
        AniLoop();
	}

    void AniLoop()
    {
        if(m_play)
        {
            if (m_indexSprite <= m_maxSprite)
            {
                m_ts = new TimeSpan(DateTime.Now.Ticks);
                TimeSpan ts = m_ts.Subtract(m_lastts).Duration();
                Debug.Log("ts.Milliseconds =" + ts.Milliseconds + " m_playSpeed = " + m_speed);
                if (ts.Milliseconds > m_speed)
                {
                    string spriteName = m_spriteName + Convert.ToString(m_typeSprite) + "_" + Convert.ToString(m_indexSprite);
                    GetComponent<UISprite>().spriteName = spriteName;
                    Debug.Log("m_sprtename = " + spriteName);
                    m_indexSprite++;
                    m_lastts = m_ts;
                    if (m_indexSprite > m_maxSprite)
                    {
                        m_indexSprite = 1;
                    }
                    if (!m_bLoop)
                    {
                        if (m_indexSprite > m_maxSprite)
                        {
                            m_bIsFnish = true;
                            m_play = false;
                        }
                    }
                }
                

            }
        }
    }
}
