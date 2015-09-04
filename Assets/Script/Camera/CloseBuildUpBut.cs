using UnityEngine;
using System.Collections;

public class CloseBuildUpBut : MonoBehaviour
{
    public GameObject m_Close;
    public GameObject m_CloseBM;
    // Use this for initialization
    void Start()
    {
        UIEventListener.Get(m_Close).onClick = onCABUB;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void onCABUB(GameObject go)
    {
        Debug.Log("你在点击地图！");
        int ListCount = m_CloseBM.transform.GetComponent<BuildingManager>().m_BuildingList.Count;
        for (int i = 0; i < ListCount; i++)
        {
            m_CloseBM.transform.GetComponent<BuildingManager>().m_BuildingList[i].GetComponent<Building>().m_UpgraedManager.SetActive(false);
            m_CloseBM.transform.GetComponent<BuildingManager>().m_BuildingList[i].GetComponent<Building>().m_House.GetComponent<TweenColor>().ResetToBeginning();
            m_CloseBM.transform.GetComponent<BuildingManager>().m_BuildingList[i].GetComponent<Building>().m_House.GetComponent<TweenColor>().enabled = false;

            GlobalVar.GetInstance().Bobjname = null;

        }
    }
}
