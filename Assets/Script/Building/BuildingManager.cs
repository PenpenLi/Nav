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
    public GameObject m_BMCamera;
    public GameObject m_Input;

    // User Parameters
    UIAtlas H_Hhousesa;          //get house icon atlas
    string H_Housesn;          //get house icon sprite name


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
         m_building.GetComponent<Building>().m_HouseMenu = m_BMHouseMenu;
         m_building.GetComponent<Building>().m_Camera = m_BMCamera;
         if (m_Bclone[BInd].Status == 1)
         {
             m_building.transform.GetComponent<Building>().m_House.SetActive(true);
             m_building.transform.GetComponent<Building>().m_Sale.SetActive(false);
             m_building.transform.FindChild("House").GetComponent<UISprite>().atlas = H_Hhousesa;
             m_building.transform.FindChild("House").GetComponent<UISprite>().spriteName = H_Housesn;
         }
         return m_building;
    }


    void setBCloneList()
    {
        m_Bclone.Add(new BuilClone() { Id = 0, BuildPos = new Vector3(752.9f, -22.5f, 0), Lev =3, Status = 1 });
        m_Bclone.Add(new BuilClone() { Id = 1, BuildPos = new Vector3(752.9f, 126f, 0), Lev = 5, Status = 1 });
        m_Bclone.Add(new BuilClone() { Id = 2, BuildPos = new Vector3(1036f, 126f, 0), Lev = -1, Status = -1 });
        m_Bclone.Add(new BuilClone() { Id = 3, BuildPos = new Vector3(903f, 42f, 0), Lev = 2, Status = 1 });
        m_Bclone.Add(new BuilClone() { Id = 4, BuildPos = new Vector3(903f, 210f, 0), Lev = 1, Status = 1 });
        m_Bclone.Add(new BuilClone() { Id = 4, BuildPos = new Vector3(1220f, 220f, 0), Lev = -1, Status = -1 });
        m_Bclone.Add(new BuilClone() { Id = 4, BuildPos = new Vector3(1070f, 300f, 0), Lev = -1, Status = -1 });
        m_Bclone.Add(new BuilClone() { Id = 4, BuildPos = new Vector3(920f, 380f, 0), Lev = -1, Status = -1 });

    }

    void getHouseA(int AtlasId)
    {
        for (int i = 0; i < m_Bclone.Count; i++)
        {
            switch (AtlasId)
            {
                case 0:
                    H_Hhousesa = m_AtlasList[AtlasId];
                    H_Housesn = "ani_house_a_" + m_Bclone[AtlasId].Lev;
                    break;
                case 1:
                    H_Hhousesa = m_AtlasList[AtlasId];
                    H_Housesn = "ani_house_b_" + m_Bclone[AtlasId].Lev;
                    break;
                case 2:
                    H_Hhousesa = m_AtlasList[AtlasId];
                    H_Housesn = "ani_house_c_" + m_Bclone[AtlasId].Lev;
                    break;
                case 3:
                    H_Hhousesa = m_AtlasList[AtlasId];
                    H_Housesn = "ani_house_d_" + m_Bclone[AtlasId].Lev;
                    break;
                case 4:
                    H_Hhousesa = m_AtlasList[AtlasId];
                    H_Housesn = "ani_house_e_" + m_Bclone[AtlasId].Lev;
                    break;
                default:
                    break;
            }
        }
    }
}
