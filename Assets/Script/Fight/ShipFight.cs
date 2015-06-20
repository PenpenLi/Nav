using UnityEngine;
using System.Collections;
using System;

public class ShipFight : MonoBehaviour {
	public GameObject m_FightPt;
    public GameObject m_FireEffect;
    public int m_Hp = 500;
    public int m_MaxHp = 2000;
    //public GameObject m_Shell;
    private Vector3 m_targetPos;
    //private Curve2D curve = new Curve2D();
    public float m_FireSpeed = 1.0f / 600.0f;//攻击速度
    public float m_MoveSpeed = 1.0f;//船身抖动速度
    public int m_ShellType = 1;//使用的炮弹类型
    bool bOpenFire = false;
    public int m_flameType = 1;
    public int m_maxFlameSprite = 6;
    string m_spriteName;
    int m_flameIndex = 1;
    public float m_playSpeed =  0.2f;
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
        IsFireState();
        Time.fixedDeltaTime = Time.timeScale;
	}

    void ResetAll()
    {
        //m_MaxHp = m_Hp;
        m_spriteName = "Ship_fire1_1";
        //m_Shell.SetActive(false);
        Time.timeScale = m_playSpeed ;
    }

    public void ChangeFlameType(int type, int maxSprite)
    {
        m_spriteName = "Ship_fire" + Convert.ToString(type) + "_1";
        m_flameType = type;
        m_maxFlameSprite = maxSprite;
        m_FireEffect.GetComponent<UISprite>().spriteName = m_spriteName;
    }

    void IsFireState()
    {
        //Debug.Log("IsFireState");
        if(m_Hp < m_MaxHp / 3)
        {
           // Debug.Log("m_Hp < m_MaxHp / 3");
            if(m_FireEffect.activeSelf == false)
            {
                m_FireEffect.SetActive(true);
            }
            OpenFlame(true);
        }
    }

    void OpenFlame(bool open = false)
    {
        if(open)
        {
            Debug.Log("Time.deltaTime = " + Time.deltaTime);
            //while(Time.fixedTime)
            if(m_flameIndex < m_maxFlameSprite)
            {
                m_spriteName = "Ship_fire" + Convert.ToString(m_ShellType) + "_" + Convert.ToString(m_flameIndex);
                m_FireEffect.GetComponent<UISprite>().spriteName = m_spriteName;
                //Debug.Log("m_sprtename = " + m_spriteName);
                m_flameIndex++;
            }
            else
            {
                m_flameIndex = 1;
            }
        }
        else
        {
            m_FireEffect.SetActive(false);
        }
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
