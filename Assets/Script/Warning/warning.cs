/* 创建者：师小强
 * 时间：2015/7/30
 * 说明：警告框调用
 */
using UnityEngine;
using System.Collections;


public class warning : MonoBehaviour{

    public GameObject m_CameraMap;
    public GameObject m_CloseBut;
    public GameObject m_warning;

    bool Sw = false; //标记地图缩放与拖动是否是由warning框关闭的
    // Use this for initialization
	void Start () 
    {
        UIEventListener.Get(m_CloseBut).onClick = onClose;
    }

    // Update is called once per frame
    void Update(){

    }

    public warning(string WarningString)
    {
        GameObject Obj = Instantiate(Resources.Load("Prefab/Warning/Warning350")) as GameObject;//get perfab; 
        Obj.transform.parent = GameObject.Find("UI").transform;
        Obj.transform.localScale = Vector3.one;
        
        Transform Tf = Obj.transform.parent.FindChild("Warning350(Clone)");
        Tf.GetComponent<warning>().m_CameraMap = Obj.transform.parent.GetComponent<UIList>().m_CameraMap;
        Tf.FindChild("WarningString").GetComponent<UILabel>().text = WarningString;

        if (Tf.GetComponent<warning>().m_CameraMap.GetComponent<CameraDragMove>().enabled) 
        {         
            Debug.Log("拖动与缩放还没有关闭，现在由Warning自己关闭！");
            Tf.GetComponent<warning>().m_CameraMap.GetComponent<CameraDragMove>().enabled = false;
            Tf.GetComponent<warning>().m_CameraMap.GetComponent<ScalingMap>().enabled = false;
            GlobalVar.GetInstance().Sw = false;
        }
    }

    void onClose(GameObject go)
    {
        if (GlobalVar.GetInstance().Sw == false)
        {
            Debug.Log("现在由Warning自己打开拖动与缩放！");
            m_warning.GetComponent<warning>().m_CameraMap.GetComponent<CameraDragMove>().enabled = true;
            m_warning.GetComponent<warning>().m_CameraMap.GetComponent<ScalingMap>().enabled = true;
            GlobalVar.GetInstance().Sw = true;
        }
        Destroy(m_warning);
    }
}
