using UnityEngine;
using System.Collections;

public class AchorShip : MonoBehaviour {
    Vector3 m_comeBeginPos = new Vector3(-2280.0f, 1037.0f, 0.0f);
    Vector3 m_comeEndPos = new Vector3(-545.0f, -99.0f, 0.0f);
	// Use this for initialization
	void Start () {
        Come(50.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Come(float timeLength)//入港
    {

        gameObject.GetComponent<UISprite>().flip = UIBasicSprite.Flip.Horizontally;
        gameObject.GetComponent<TweenPosition>().from = m_comeBeginPos;
        gameObject.GetComponent<TweenPosition>().to = m_comeEndPos;
        gameObject.GetComponent<TweenPosition>().duration = timeLength;
        gameObject.GetComponent<TweenPosition>().enabled = true;
    }

    public void Back()//出港
    {

    }
}
