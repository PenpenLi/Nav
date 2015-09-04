using UnityEngine;
using System.Collections;

public class SaleBut : MonoBehaviour {

    public GameObject m_BObj;
    public GameObject m_HouseMenu;
    public GameObject m_CameraMap;
    public GameObject m_Sale;

	// Use this for initialization
	void Start () {

        m_HouseMenu = m_BObj.GetComponent<Building>().m_HouseMenu;
        m_CameraMap = m_BObj.GetComponent<Building>().m_CameraMap;

        UIEventListener.Get(m_Sale).onClick = onSale;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void onSale(GameObject go)
    {
        //string Str = GlobalVar.GetInstance().Bobjname;

        //if (GlobalVar.GetInstance().Bobjname == null)
        //{
        //    m_HouseMenu.SetActive(true);
        //    m_CameraMap.GetComponent<CameraDragMove>().enabled = false;
        //    m_CameraMap.GetComponent<ScalingMap>().enabled = false;
        //    GlobalVar.GetInstance().Bobjname = m_BObj.name;
        //}
        //else
        //{
        //    Transform Tf = m_BObj.transform.parent.FindChild(Str);
        //    //关闭上次打开的
        //    Tf.GetComponent<Building>().m_HName.SetActive(false);
        //    Tf.GetComponent<Building>().m_HLev.SetActive(false);
        //    Tf.GetComponent<Building>().m_UpInfo.SetActive(false);
        //    Tf.GetComponent<Building>().m_Upgrade.SetActive(false);
        //    Tf.GetComponent<Building>().m_Items.SetActive(false);

        //    //打开自己
        //    m_HouseMenu.SetActive(true);
        //    m_CameraMap.GetComponent<CameraDragMove>().enabled = false;
        //    m_CameraMap.GetComponent<ScalingMap>().enabled = false;
        //    GlobalVar.GetInstance().Bobjname = m_BObj.name;
        //}
    }
}
