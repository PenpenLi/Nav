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

    GameObject Obj;

    // Use this for initialization
	void Start () 
    {
        UIEventListener.Get(m_CloseBut).onClick = onClose;
    }

    // Update is called once per frame
    void Update(){

    }

    public void AddWarning(string WarningString)
    {
        Obj = Instantiate(Resources.Load("Prefab/Warning/Warning350")) as GameObject;//get perfab; 
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
        Debug.Log(Obj.name);
        Debug.Log(Obj.transform.parent.name);
        Debug.Log(GlobalVar.GetInstance().Sw);
    }

    void onClose(GameObject go)
    {
        Debug.Log(GlobalVar.GetInstance().Sw);
        Debug.Log(m_warning.name);
        Destroy(m_warning);
        if (GlobalVar.GetInstance().Sw == false)
        {
            Debug.Log("现在由Warning自己打开拖动与缩放！");
            m_warning.GetComponent<warning>().m_CameraMap.GetComponent<CameraDragMove>().enabled = true;
            m_warning.GetComponent<warning>().m_CameraMap.GetComponent<ScalingMap>().enabled = true;
            GlobalVar.GetInstance().Sw = true;
        }
        
    }
}
