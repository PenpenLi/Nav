using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MoveMenu : MonoBehaviour {
    //public List<GameObject> m_MenuButList = new List<GameObject>();//创建按钮集
    public GameObject m_UMenuBG; //Up背板
    public GameObject m_RMenuBG; //Right背板
    public GameObject m_OpMenu; //伸缩按钮 伸
    
    private bool H_Move = false;

	// Use this for initialization
	void Start () {

        UIEventListener.Get(m_OpMenu).onClick = onOpMenu;
    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void onOpMenu(GameObject go)
    {
        if (H_Move == false)
        {
            H_Move = true;

            m_UMenuBG.GetComponent<TweenPosition>().from.y = -320;
            m_UMenuBG.GetComponent<TweenPosition>().to.y = 0;
            m_UMenuBG.GetComponent<TweenPosition>().enabled = true;
            m_UMenuBG.GetComponent<TweenPosition>().ResetToBeginning();

            m_RMenuBG.GetComponent<TweenPosition>().from.x = -628;
            m_RMenuBG.GetComponent<TweenPosition>().to.x = 0;
            m_RMenuBG.GetComponent<TweenPosition>().enabled = true;
            m_RMenuBG.GetComponent<TweenPosition>().ResetToBeginning();

        }
        else
        {
            H_Move = false;

            m_UMenuBG.GetComponent<TweenPosition>().from.y = 0;
            m_UMenuBG.GetComponent<TweenPosition>().to.y = -320;
            m_UMenuBG.GetComponent<TweenPosition>().enabled = true;
            m_UMenuBG.GetComponent<TweenPosition>().ResetToBeginning();

            m_RMenuBG.GetComponent<TweenPosition>().from.x = 0;
            m_RMenuBG.GetComponent<TweenPosition>().to.x = -628;
            m_RMenuBG.GetComponent<TweenPosition>().enabled = true;
            m_RMenuBG.GetComponent<TweenPosition>().ResetToBeginning();
        
        }
        
        

    }
}
