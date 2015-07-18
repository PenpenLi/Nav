using UnityEngine;
using System.Collections;

public enum ShipState
{
    Begin,
    EnterPort,
    Docking,
    Dock,
    Leave,
    LevelPos,
    Go
}

public class AchorShip : MonoBehaviour {
    Vector3 m_comeBeginPos = new Vector3(-2263, 1339.0f, 0.0f);
    Vector3 m_comeEndPos = new Vector3(-368.0f, 38.0f, 0.0f);
    float m_offsetbase = -1.0f;
    //Vector3 m_dockPos = new Vector3();
    GameObject m_dockGO;
    Vector3 m_tempVec3 = new Vector3();
    ShipState m_shipState = new ShipState();
    Vector3 m_leaveStart = new Vector3(114.0f, -97.0f, 0.0f);
    Vector3 m_leaveEnd = new Vector3(-2250.0f, -1127.0f, 0.0f);
    Curve2D m_curveLeave = new Curve2D();
    Curve2D m_curveCom = new Curve2D();
    float m_scaleSpeed = 1.0f;
    float m_fStep = 0.0f;
    float m_fComStep = 0.0f;
    float m_moveSpeed = 0.3f;
	// Use this for initialization
	void Start () {
        UIEventListener.Get(gameObject).onClick = ClickShip;
        m_curveLeave.SetLineCurve(m_leaveStart, m_leaveEnd);
        m_curveCom.SetLineCurve(m_comeBeginPos, m_comeEndPos);
        m_fStep = 0.0f;
        m_fComStep = 0.0f;
        Come(50.0f);
	}
	
    void ClickShip(GameObject go)
    {
        Debug.Log("click ship");
        switch (m_shipState)
        {
            case ShipState.EnterPort:

                break;
            case ShipState.Dock://在停泊时点击船体
                m_shipState = ShipState.Leave;
                break;
            case ShipState.Leave:

                break;
        }
    }

    public void SetDockParent(GameObject go)
    {
        m_dockGO = go;
    }


	// Update is called once per frame
	void Update () {
        switch(m_shipState)
        {
            case ShipState.EnterPort:
                if(EnterPort())
                {
                    Debug.Log(" m_shipState = ShipState.Dock;");
                    m_shipState = ShipState.Docking;
                }
                break;
            case ShipState.Docking:
                if (Apear(true))
                {
                    m_shipState = ShipState.Dock;
                    Dock();
                }
                break;
            case ShipState.Dock:
                Apear();
                break;
            case ShipState.Leave:
                if(Apear(true))
                {
                    m_shipState = ShipState.LevelPos;
                    gameObject.transform.parent = GameObject.FindWithTag("Anchor").transform;
                    gameObject.transform.localPosition = m_leaveStart;
                }
                break;
            case ShipState.LevelPos:
                if(Apear())
                {
                    m_shipState = ShipState.Go;
                }
                break;
            case ShipState.Go:
                GoOut();
                break;
        }
	}

    void ReconnectShipManager(GameObject go)
    {

    }

    void GoOut()
    {
        m_fStep = m_fStep + Time.deltaTime * m_scaleSpeed * m_moveSpeed;
        if (m_fStep >= 1.0f)
        {
            m_fStep = 1.0f;
        }
        if (m_fStep <= 0.0f)
        {
            m_fStep = 0.0f;
        }
        Vector3 vecTemp = m_curveLeave.SetPos(m_fStep);
        transform.localPosition = vecTemp;
    }

    bool EnterPort()
    {
        m_fComStep = m_fComStep + Time.deltaTime * m_scaleSpeed * m_moveSpeed;
        if (m_fComStep >= 1.0f)
        {
            m_fComStep = 1.0f;
            return true;
        }
        if (m_fComStep <= 0.0f)
        {
            m_fComStep = 0.0f;
        }
        Vector3 vecTemp = m_curveCom.SetPos(m_fComStep);
        transform.localPosition = vecTemp;
        return false;
    }

    public ShipState GetState()
    {
        return m_shipState;
    }

    void WaveShip()
    {
        float yoffset = Time.deltaTime * m_offsetbase * 5;
        m_tempVec3 = gameObject.transform.localPosition;
        m_tempVec3.y += yoffset;
        gameObject.transform.localPosition = m_tempVec3;
        Debug.Log("m_tempVec3 = " + m_tempVec3);
        m_offsetbase *= -1.0f;
    }

    public void Come(float timeLength)//入港
    {
        m_shipState = ShipState.EnterPort;
        gameObject.GetComponent<UISprite>().flip = UIBasicSprite.Flip.Horizontally;
        
    }

    public void ComComplete()
    {
        Debug.Log(" ComComplete()");
        gameObject.GetComponent<TweenColor>().enabled = true;
    }

    public void Dock()
    {
        m_shipState = ShipState.Docking;
        VisbleWave(false);
        gameObject.GetComponent<UISprite>().flip = UIBasicSprite.Flip.Nothing;

       // m_dockGO = gameObject.transform.parent.transform.parent.GetComponent<ShipsManager>().GetFreeAchor();
        m_dockGO = GameObject.FindWithTag("Anchor").GetComponent<ShipsManager>().GetFreeAchor();
        gameObject.transform.parent = m_dockGO.transform;
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localScale = Vector3.one;
        m_shipState = ShipState.Dock;
    }

    void VisbleWave(bool open)
    {
        Transform tf = transform.FindChild("Wave");
        if (tf != null)
        {
            tf.gameObject.SetActive(open);
        }
        tf = transform.FindChild("Wave1");
        if(tf != null)
        {
            tf.gameObject.SetActive(open);
        }

    }

    bool Apear(bool visable = false)
    {
        if(visable)
        {
            if (gameObject.GetComponent<UISprite>().alpha > 0.0f)
            {
                gameObject.GetComponent<UISprite>().alpha -= (Time.deltaTime * m_scaleSpeed);
                return false;
            }
            else
            {
                //m_shipState = ShipState.Leave;
                return true;
            }
        }
        else
        {
            Debug.Log("visable = false gameObject.GetComponent<UISprite>().alpha = " + gameObject.GetComponent<UISprite>().alpha);
            if (gameObject.GetComponent<UISprite>().alpha < 1.0f)
            {
                Debug.Log("gameObject.GetComponent<UISprite>().alpha < 1.0f");
                gameObject.GetComponent<UISprite>().alpha += (Time.deltaTime * m_scaleSpeed);
                return false;
            }
            else
            {
                //m_shipState = ShipState.Leave;
                return true;
            }
        }
      

    }

    public void Back()//出港
    {
        gameObject.GetComponent<TweenColor>().ResetToBeginning();
    }
}
