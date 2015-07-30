/* 创建者：师小强
 * 时间：2015/7/30
 * 说明：警告框调用
 */
using UnityEngine;
using System.Collections;


public class warning : MonoBehaviour{

    public void AddWarning(GameObject Obj ,string WarningString,string Father)
    {
        Obj = Instantiate(Resources.Load("Prefab/Warning/Warning350")) as GameObject;//get perfab; 
        Obj.transform.parent = GameObject.Find(Father).transform;
        Obj.transform.localScale = Vector3.one;
        Obj.transform.parent.FindChild("Warning350(Clone)").FindChild("WarningString").GetComponent<UILabel>().text = WarningString;
    }
}
