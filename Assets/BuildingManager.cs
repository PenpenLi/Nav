using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuilClone
{
    public int Id;
    public Vector3 BuildPos; //position of building
    public GameObject Building;
}
public class BuildingManager : MonoBehaviour {
    public List<GameObject> m_BuildingList = new List<GameObject>();
    List<GetBuildingInfo> m_Binfolist = new List<GetBuildingInfo>();
    List<BuilClone> m_Bclone = new List<BuilClone>();//get building perfab 
    List<Vector3> m_BposList = new List<Vector3>();

    int BIndex = 0; // set m_BuildingList.count for initialization
    const int BMaxIndex = 5; //set building max count
    
	// Use this for initialization
	void Start () {
    //For testing-----------------------------------------------------

        setCloneList();
        Debug.Log("m_BposList.Count =" + m_BposList.Count);
        for (int i = 0; i < BMaxIndex; i++)
        {
            m_BuildingList.Add(AddBuild(i));
        }

    //test end--------------------------------------------------------
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public GameObject AddBuild(int BInd )
    {
        int i = BInd;
        GameObject building = Instantiate(Resources.Load("Prefab/Building/Building")) as GameObject;//get perfab
        building.transform.parent = GameObject.Find("BuildingManager").transform;//set building parent
        building.transform.localScale = Vector3.one;
        building.transform.localPosition = m_BposList[i];//set building position
        return building;
    }

    void setCloneList()
    {
        m_BposList.Add(new Vector3(752.9f, -22.5f, 0));
        m_BposList.Add(new Vector3(752.9f, 126f, 0));
        m_BposList.Add(new Vector3(1036f, 126f, 0));
        m_BposList.Add( new Vector3(903f, 42f, 0));
        m_BposList.Add(new Vector3(903f, 210f, 0));
    }
}
