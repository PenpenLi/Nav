using UnityEngine;
using System.Collections;

public enum ShipState
{
    Begin,
    EnterPort,
    Docking,
    Dock,
    Leave,
    Go
}

public class AchorShip : MonoBehaviour {
    Vector3 m_comeBeginPos = new Vector3(-2280.0f, 1037.0f, 0.0f);
    Vector3 m_comeEndPos = new Vector3(-545.0f, -99.0f, 0.0f);
    float m_offsetbase = -1.0f;
    //Vector3 m_dockPos = new Vector3();
    GameObject m_dockGO;
    Vector3 m_tempVec3 = new Vector3();
    ShipState m_shipState = new ShipState();
	// Use this for initialization
	void Start () {
        Come(50.0f);
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

                break;
            case ShipState.Dock:
                DockApear();
                break;
            case ShipState.Leave:
                Back();
                break;
        }
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
        gameObject.GetComponent<TweenPosition>().from = m_comeBeginPos;
        gameObject.GetComponent<TweenPosition>().to = m_comeEndPos;
        gameObject.GetComponent<TweenPosition>().duration = timeLength;
        gameObject.GetComponent<TweenPosition>().enabled = true;
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
        m_dockGO = gameObject.transform.parent.GetComponent<ShipsManager>().GetFreeAchor();
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

    bool DockApear()
    {
        if(gameObject.GetComponent<UISprite>().alpha < 1.0f)
        {
            gameObject.GetComponent<UISprite>().alpha += (Time.deltaTime * 1);
            return false;
        }
        else
        {
            m_shipState = ShipState.Leave;
            return true;
        }

    }

    public void Back()//出港
    {
        gameObject.GetComponent<TweenColor>().ResetToBeginning();
    }
}
