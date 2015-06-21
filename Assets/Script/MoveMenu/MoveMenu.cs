using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MoveMenu : MonoBehaviour {
    //public List<GameObject> m_MenuButList = new List<GameObject>();//创建按钮集
    public GameObject m_UMenuBG; //Up背板
    public GameObject m_RMenuBG; //Right背板
    public GameObject m_OpMenu; //伸缩按钮 伸
    public GameObject m_ClMenu;//伸缩按钮 缩

	// Use this for initialization
	void Start () {

        UIEventListener.Get(m_OpMenu).onClick = onOpMenu;
        UIEventListener.Get(m_ClMenu).onClick = onClMenu;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void onOpMenu(GameObject go)
    {
        m_OpMenu.SetActive(false);
        m_ClMenu.SetActive(true);
        m_UMenuBG.SetActive(true);
        m_RMenuBG.SetActive(true);

        m_UMenuBG.GetComponent<TweenPosition>().from.y = -320;
        m_UMenuBG.GetComponent<TweenPosition>().to.y = 0;
        m_UMenuBG.GetComponent<TweenPosition>().enabled = true;
        m_UMenuBG.GetComponent<TweenPosition>().ResetToBeginning();

        m_RMenuBG.GetComponent<TweenPosition>().from.x = -628;
        m_RMenuBG.GetComponent<TweenPosition>().to.x = 0;
        m_RMenuBG.GetComponent<TweenPosition>().enabled = true;
        m_RMenuBG.GetComponent<TweenPosition>().ResetToBeginning();
        

    }

    void onClMenu(GameObject go)
    {
        m_ClMenu.SetActive(false);
        m_OpMenu.SetActive(true);

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
