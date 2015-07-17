using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuilClone
{
    public int Id;
    public Vector3 BuildPos; //position of building
    public int Lev; // building level
    public int Status;// get building current status
    //public GameObject Building;
}
public class BuildingManager : MonoBehaviour
{
    // User GameObject
    public List<GameObject> m_BuildingList = new List<GameObject>();
    public List<UIAtlas> m_AtlasList = new List<UIAtlas>();

    List<GetBuildingInfo> m_Binfolist = new List<GetBuildingInfo>();
    List<BuilClone> m_Bclone = new List<BuilClone>();//get building perfab 

    public GameObject m_BMHouseMenu;
    

    // User Parameters
    UIAtlas m_housesa;          //get house icon atlas
    string m_housesn;          //get house icon sprite name


    // Use this for initialization
    void Start()
    {
        //For testing----------------------------------------------------

        setBCloneList();
        for (int i = 0; i < m_Bclone.Count; i++)
        {
            m_BuildingList.Add(AddBuild(i));
        }

        //test end--------------------------------------------------------

    }

    // Update is called once per frame
    void Update()
    {

    }


     GameObject AddBuild(int BInd)
    {
         getHouseA(BInd);
         GameObject m_building = Instantiate(Resources.Load("Prefab/Building/Building")) as GameObject;//get perfab
         m_building.transform.parent = GameObject.Find("BuildingManager").transform;//set building parent
         m_building.name = BInd.ToString();
         m_building.transform.localScale = Vector3.one;
         m_building.transform.localPosition = m_Bclone[BInd].BuildPos;//set building position
         if (m_Bclone[BInd].Status == 1)
         {
             m_building.transform.FindChild("House").GetComponent<UISprite>().atlas = m_housesa;
             m_building.transform.FindChild("House").GetComponent<UISprite>().spriteName = m_housesn;
         }
         return m_building;
    }


    void setBCloneList()
    {
        m_Bclone.Add(new BuilClone() { Id = 0, BuildPos = new Vector3(752.9f, -22.5f, 0), Lev = 3, Status = -1 });
        m_Bclone.Add(new BuilClone() { Id = 1, BuildPos = new Vector3(752.9f, 126f, 0), Lev = 5, Status = 1 });
        m_Bclone.Add(new BuilClone() { Id = 2, BuildPos = new Vector3(1036f, 126f, 0), Lev = 4, Status = -1 });
        m_Bclone.Add(new BuilClone() { Id = 3, BuildPos = new Vector3(903f, 42f, 0), Lev = 2, Status = 1 });
        m_Bclone.Add(new BuilClone() { Id = 4, BuildPos = new Vector3(903f, 210f, 0), Lev = 1, Status = 1 });

    }

    void getHouseA(int AtlasId)
    {
        for (int i = 0; i < m_Bclone.Count; i++)
        {
            switch (AtlasId)
            {
                case 0:
                    m_housesa = m_AtlasList[AtlasId];
                    m_housesn = "ani_house_a_" + m_Bclone[AtlasId].Lev;
                    break;
                case 1:
                    m_housesa = m_AtlasList[AtlasId];
                    m_housesn = "ani_house_b_" + m_Bclone[AtlasId].Lev;
                    break;
                case 2:
                    m_housesa = m_AtlasList[AtlasId];
                    m_housesn = "ani_house_c_" + m_Bclone[AtlasId].Lev;
                    break;
                case 3:
                    m_housesa = m_AtlasList[AtlasId];
                    m_housesn = "ani_house_d_" + m_Bclone[AtlasId].Lev;
                    break;
                case 4:
                    m_housesa = m_AtlasList[AtlasId];
                    m_housesn = "ani_house_e_" + m_Bclone[AtlasId].Lev;
                    break;
                default:
                    break;
            }
        }
    }
}
