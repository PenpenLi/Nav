using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NetManager;

public class BuilPosition
{
    public Vector3 BuildPos; //position of building
}
public class BuildingManager : MonoBehaviour
{
    // User GameObject
    public List<GameObject> m_BuildingList = new List<GameObject>();
    public List<UIAtlas> m_AtlasList = new List<UIAtlas>();  //图集list
    private List<BuilPosition> m_BP = new List<BuilPosition>();//get building perfab 
    
    public GameObject m_BMHouseMenu;
    public GameObject m_BMCamera;

    // User Parameters
    private UIAtlas H_Hhousesa;          //get house icon atlas
    private string H_Housesn;          //get house icon sprite name

    //-------------------------------------------------------------------

    private List<BuildingInfo> m_Binfolist = new List<BuildingInfo>();
    private List<BuildingInfo> m_BI;

    // Use this for initialization
    void Start()
    {
        setBCloneList();
        for (int i = 0; i < m_Binfolist.Count; i++)
        {
            m_BuildingList.Add(NetAddBuild(i));
        }
       
        //m_Binfolist = PlayerDataManager.GetInstance().m_bi;
    }

    // Update is called once per frame
    void Update()
    {

    }

     GameObject NetAddBuild(int BInd) //添加建筑
     {
         getHouseA(BInd);
         GameObject m_building = Instantiate(Resources.Load("Prefab/Building/Building")) as GameObject;//get perfab
         m_building.transform.parent = GameObject.Find("BuildingManager").transform;//set building parent
         m_building.name = BInd.ToString();
         m_building.transform.localScale = Vector3.one;

         if (m_Binfolist[BInd].BuildingID == -1)
         {
             m_building.transform.GetComponent<Building>().m_House.SetActive(false);
             m_building.transform.GetComponent<Building>().m_Sale.SetActive(true);
         }
         else
         {
             m_building.transform.GetComponent<Building>().m_House.SetActive(true);
             m_building.transform.GetComponent<Building>().m_Sale.SetActive(false);

             m_building.transform.FindChild("House").GetComponent<UISprite>().atlas = H_Hhousesa;
             m_building.transform.FindChild("House").GetComponent<UISprite>().spriteName = H_Housesn;
         }
         m_building.transform.localPosition = m_BP[BInd].BuildPos;
         m_building.GetComponent<Building>().m_HouseMenu = m_BMHouseMenu;
         m_building.GetComponent<Building>().m_CameraMap = m_BMCamera;
         
         return m_building;
     }
    void setBCloneList()//建筑数据
    {
        m_BP.Add(new BuilPosition() {  BuildPos = new Vector3(752.9f, -22.5f, 0) });
        m_BP.Add(new BuilPosition() {  BuildPos = new Vector3(752.9f, 126f, 0) });
        m_BP.Add(new BuilPosition() {  BuildPos = new Vector3(1036f, 126f, 0) });
        m_BP.Add(new BuilPosition() {  BuildPos = new Vector3(903f, 42f, 0) });
        m_BP.Add(new BuilPosition() {  BuildPos = new Vector3(903f, 210f, 0) });
        m_BP.Add(new BuilPosition() {  BuildPos = new Vector3(1220f, 220f, 0) });
        m_BP.Add(new BuilPosition() {  BuildPos = new Vector3(1070f, 300f, 0) });
        m_BP.Add(new BuilPosition() {  BuildPos = new Vector3(920f, 380f, 0) });

        m_Binfolist.Add(new BuildingInfo() { PosID = 0, BuildingID = 2, Lev = 2, UpTimes = -1, ProductID = -1, ItemNu = 0 });
        m_Binfolist.Add(new BuildingInfo() { PosID = 1, BuildingID = -1, Lev = 1, UpTimes = -1, ProductID = -1, ItemNu = 0 });
        m_Binfolist.Add(new BuildingInfo() { PosID = 2, BuildingID = 5, Lev = 1, UpTimes = -1, ProductID = -1, ItemNu = 0 });
        m_Binfolist.Add(new BuildingInfo() { PosID = 3, BuildingID = 1, Lev = 2, UpTimes = -1, ProductID = -1, ItemNu = 0 });
        m_Binfolist.Add(new BuildingInfo() { PosID = 4, BuildingID = 3, Lev = 2, UpTimes = -1, ProductID = -1, ItemNu = 0 });
        m_Binfolist.Add(new BuildingInfo() { PosID = 5, BuildingID = -1, Lev = 3, UpTimes = -1, ProductID = -1, ItemNu = 0 });
        m_Binfolist.Add(new BuildingInfo() { PosID = 6, BuildingID = 4, Lev = 2, UpTimes = -1, ProductID = -1, ItemNu = 0 });
        m_Binfolist.Add(new BuildingInfo() { PosID = 7, BuildingID = -1, Lev = 2, UpTimes = -1, ProductID = -1, ItemNu = 0 });

    }

    void getHouseA(int AtlasId) //建筑资源名称
    {
        for (int i = 0; i < m_Binfolist.Count; i++)
        {
            switch (AtlasId)
            {
                case 0:
                    H_Hhousesa = m_AtlasList[AtlasId];
                    H_Housesn = "ani_house_a_" + m_Binfolist[AtlasId].Lev;
                    break;
                case 1:
                    H_Hhousesa = m_AtlasList[AtlasId];
                    H_Housesn = "ani_house_b_" + m_Binfolist[AtlasId].Lev;
                    break;
                case 2:
                    H_Hhousesa = m_AtlasList[AtlasId];
                    H_Housesn = "ani_house_c_" + m_Binfolist[AtlasId].Lev;
                    break;
                case 3:
                    H_Hhousesa = m_AtlasList[AtlasId];
                    H_Housesn = "ani_house_d_" + m_Binfolist[AtlasId].Lev;
                    break;
                case 4:
                    H_Hhousesa = m_AtlasList[AtlasId];
                    H_Housesn = "ani_house_e_" + m_Binfolist[AtlasId].Lev;
                    break;
                default:
                    break;
            }
        }
    }

   
}
