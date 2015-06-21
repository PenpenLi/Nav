using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuUpBut : MonoBehaviour {
    public List<GameObject> m_MenuButList = new List<GameObject>();
    
	// Use this for initialization
	void Start () {
        for (int i = 0; i < m_MenuButList.Count; i++)
        {
            UIEventListener.Get(m_MenuButList[i]).onClick = onList;
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void onList(GameObject go)
    {
        for (int i = 0; i < m_MenuButList.Count; i++)
        {
            if (m_MenuButList[i].GetHashCode() == go.gameObject.GetHashCode())
            {
                switch (i)
                {
                    case 0:
                        Debug.Log(m_MenuButList[i].name);
                        break;
                    case 1:
                        Debug.Log(m_MenuButList[i].name);
                        break;
                    case 2:
                        Debug.Log(m_MenuButList[i].name);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
