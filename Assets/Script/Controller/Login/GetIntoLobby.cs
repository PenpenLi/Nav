using UnityEngine;
using System.Collections;
using System;

public class GetIntoLobby : MonoBehaviour {

    public UIPanel m_Login;
    public UISprite m_Moveline;
    
    float Fidd = 0;
    float Ah = 1;
	// Use this for initialization
	void Start () {
        
        InvokeRepeating("MoveLine", 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
        if (Fidd >= 1 && !IsInvoking("SneakOut"))
        {
            CancelInvoke("MoveLine");
            InvokeRepeating("SneakOut",0,0);
        }
        if(Ah<=0.5)
        {
            SceneSwitch.GetSceneSwitch().Switch(GameScene.Lobby);
            CancelInvoke("SeakOut");
            m_Login.GetComponent<GetIntoLobby>().enabled = false;
        }
	}

    void MoveLine()
    {
        Fidd += 0.1f;
        m_Moveline.fillAmount = Fidd;
        Debug.Log(Fidd);
    }

    void SneakOut()
    {
        Ah -= 0.03f;
        m_Login.alpha = Ah;
    }
}
