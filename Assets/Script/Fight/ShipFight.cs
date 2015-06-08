using UnityEngine;
using System.Collections;
using System;

public class ShipFight : MonoBehaviour {
	public GameObject m_FightPt;
    //public GameObject m_Shell;
    private Vector3 m_targetPos;
    private int m_shellType = 1;
    //private Curve2D curve = new Curve2D();
    public float m_FireSpeed = 1.0f / 600.0f;//攻击速度
    public float m_MoveSpeed = 1.0f;//船身抖动速度
    public int m_ShellType = 1;//使用的炮弹类型
    bool bOpenFire = false;
	// Use this for initialization
	void Start () {
        ResetAll();
	}
	
	// Update is called once per frame
	void Update () {
        //if(bOpenFire)
        //{
        //    if(m_Shell.GetComponent<Shell>().GetCubicPos() >= 1.0f)
        //    {
        //        bOpenFire = false;
        //        ResetAll();
        //        GlobalVar.GetInstance().SetFinishShell(true);
        //    }
        //   // Debug.Log("FireSpeed = " + FireSpeed);
        //    m_Shell.transform.position = m_Shell.GetComponent<Shell>().AddCubicPos(FireSpeed);
            
        //}
	}

    void ResetAll()
    {
        //m_Shell.SetActive(false);
    }


    public int GetShellType()
    {
        return m_ShellType;
    }

    public void SetShellType(int type)
    {
        if(type < 1)
        {
            Debug.Log("炮弹类型设置出错");
        }
        m_ShellType = type;
        //string shellname = "ani_paodan_0" + Convert.ToString(type);
        //m_Shell.GetComponent<UISprite>().spriteName = shellname;
    }

    public void OpenFire(Vector3 targetPos, int shellType)
    {
        //GlobalVar.GetInstance().SetFinishShell(false);
        //m_Shell.SetActive(true);
        //m_targetPos = targetPos;
        //m_shellType = shellType;
    }

    //public void SetMoveSpeed(float speed)
    //{
    //    m_MoveSpeed = speed;
    //    this.GetComponent<TweenPosition>().duration = m_MoveSpeed;
    //}
}
