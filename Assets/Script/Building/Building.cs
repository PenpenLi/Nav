using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : MonoBehaviour {

    //button list
    public List<GameObject> ButList = new List<GameObject>();
    
    public GameObject m_HouseMenu;
   
    private UIAtlas m_HIA;

	// Use this for initialization
	void Start () 
    {
        if (ButList[1].transform.GetComponentInChildren<UISprite>().atlas != null)
        {
           ButList[0].SetActive(false);
        }

        for (int i = 0; i < ButList.Count; i++)
        {
            UIEventListener.Get(ButList[i]).onClick = onButList;
        }
	}
	
	// Update is called once per frame
    void Update()
    {
    }

    void onButList(GameObject go)
    {
        m_HIA = ButList[1].transform.GetComponentInChildren<UISprite>().atlas;
        for (int i = 0; i < ButList.Count; i++)
        {
            if (ButList[i].GetHashCode() == go.gameObject.GetHashCode())
            {
                switch (i)
                {
                    case 0: //Sale
                        m_HouseMenu.SetActive(true);
                        break;
                    case 1: //house
                        Debug.Log("GetHashCode" + ButList[i].GetHashCode());
                        break;
                    case 2: //upicon
                        break;
                    case 3: //progressbar
                        break;
                    case 4: //
                        break;
                    default:
                        break;
                }
            }
        }
    }
    
}