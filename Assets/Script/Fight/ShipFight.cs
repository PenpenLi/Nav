using UnityEngine;
using System.Collections;
using System;

public class ShipFight : MonoBehaviour {
	public GameObject m_FightPt;
    public GameObject m_FireEffect;
    public float m_Hp = 2000;
    public float m_MaxHp = 2000;
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
    public int m_playSpeed = 5;//一秒播几帧
    float speed;
    TimeSpan m_ts;
    TimeSpan m_lastts;
    public GameObject m_bloodText;
    public GameObject m_bloodSlider;
    public int m_Attack = 200;

	// Use this for initialization
	void Start () {
        ResetAll();
        m_lastts = new TimeSpan(DateTime.Now.Ticks);
        speed = 1000 / m_playSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateBlood();
	}

    void UpdateBlood()
    {
        m_bloodText.GetComponent<UILabel>().text = Convert.ToString(m_Hp) + "/" + Convert.ToString(m_MaxHp);
        m_bloodSlider.GetComponent<UISlider>().value = (float)(m_Hp / m_MaxHp);
        //Debug.Log("m_Hp / m_MaxHp = " + m_bloodSlider.GetComponent<UISlider>().value);
        if(m_bloodSlider.GetComponent<UISlider>().value <= 0.3f)
        {
            m_FireEffect.SetActive(true);
        }
        else
        {
            m_FireEffect.SetActive(false);
        }
    }

    void ResetAll()
    {
        m_MaxHp = m_Hp;
        m_spriteName = "Ship_fire1_1";
    }

    public void ChangeFlameType(int type, int maxSprite)
    {
        m_spriteName = "Ship_fire" + Convert.ToString(type) + "_1";
        m_flameType = type;
        m_maxFlameSprite = maxSprite;
        m_FireEffect.GetComponent<UISprite>().spriteName = m_spriteName;
    }

    public int GetAttack()
    {
        return m_Attack;
    }

    public void SetBlood(int blood, int maxblood)
    {
        m_Hp = blood;
        m_MaxHp = maxblood;
    }

    public bool AddBlood(int nBlood)
    {
        m_Hp += nBlood;
        if (m_Hp <= 0)
            return false;
        return true;
    }

    void OpenFlame(bool open = false)
    {
        if(open)
        {
            //Debug.Log("Time.deltaTime = " + Time.deltaTime);
            if(m_flameIndex < m_maxFlameSprite)
            {
                m_ts = new TimeSpan(DateTime.Now.Ticks);
                TimeSpan ts = m_ts.Subtract(m_lastts).Duration();
                //Debug.Log("ts.Milliseconds =" + ts.Milliseconds + " m_playSpeed = " + speed);
                if (ts.Milliseconds > speed)
                {
                    m_spriteName = "Ship_fire" + Convert.ToString(m_ShellType) + "_" + Convert.ToString(m_flameIndex);
                    m_FireEffect.GetComponent<UISprite>().spriteName = m_spriteName;
                    //Debug.Log("m_sprtename = " + m_spriteName);
                    m_flameIndex++;
                    m_lastts = m_ts;
                }
               
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
    }
}
