using UnityEngine;
using System.Collections;
using System;

public class ShipFight : MonoBehaviour {
	public GameObject m_FightPt;
    public GameObject m_Shell;
    private Vector3 m_targetPos;
    private int m_shellType = 1;
    //private Curve2D curve = new Curve2D();
    public float FireSpeed = 1.0f / 60.0f;//攻击速度
    bool bOpenFire = false;
	// Use this for initialization
	void Start () {
        //m_Shell.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	    if(bOpenFire)
        {
            if(m_Shell.GetComponent<Shell>().GetCubicPos() >= 1.0f)
            {
                bOpenFire = false;
            }
            Debug.Log("FireSpeed = " + FireSpeed);
            m_Shell.GetComponent<Shell>().AddCubicPos(FireSpeed);
        }
	}

    public void SetShellType(int type)
    {
        string shellname = "ani_paodan_0" + Convert.ToString(type);
        m_Shell.GetComponent<UISprite>().spriteName = shellname;
    }

    public void OpenFire(Vector3 targetPos, int shellType)
    {
        m_targetPos = targetPos;
        m_shellType = shellType;
        m_Shell.GetComponent<Shell>().SetShellTrack(m_FightPt.transform.localPosition, m_targetPos, shellType);
        bOpenFire = true;
    }
}
