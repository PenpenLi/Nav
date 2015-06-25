using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : MonoBehaviour {

    //button list
    public List<GameObject> ButList = new List<GameObject>();

    public GameObject m_Sale;
    public GameObject m_House;
    public GameObject m_UpIcon;
    public GameObject m_Progressbar;   
    public GameObject m_Architecture;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < ButList.Count; i++)
        {
            UIEventListener.Get(ButList[i]).onClick = onButList;
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void onButList(GameObject go)
    {
        for (int i = 0; i < ButList.Count; i++)
        {
            if (ButList[i].GetHashCode() == go.gameObject.GetHashCode())
            {
                switch (i)
                {
                    case 0: //Sale
                        m_Sale.SetActive(false);
                        m_Architecture.SetActive(true);
                        m_House.SetActive(true);
                        break;
                    case 1: //House
                        m_Architecture.SetActive(true);
                        break;
                    case 2: //UpIcon
                        break;
                    case 3: //ProgressBar
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
