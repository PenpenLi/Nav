using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ceshi : MonoBehaviour {

    public List<UILabel> LL = new List<UILabel>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        LL[0].text = "升级地标索引：" + GlobalVar.GetInstance().UpObjname;
        LL[1].text = "升级队列：" + GlobalVar.GetInstance().UpgradeQueues;
        LL[2].text = "升级的ID：" + GlobalVar.GetInstance().UpgradeID;
        LL[3].text = "升级耗时：" + GlobalVar.GetInstance().UpgradeTime;
        //LL[4].text = 
        //LL[5].text =
        //LL[6].text =
        LL[7].text = "建造地标索引：" + GlobalVar.GetInstance().BuildObjname;
        LL[8].text = "建造队列：" + GlobalVar.GetInstance().BuildQueues;
        LL[9].text = "建造的ID：" + GlobalVar.GetInstance().BuildID;
        LL[10].text = "建造耗时：" + GlobalVar.GetInstance().BuildTime;
        //LL[11].text =
        //LL[12].text =
        LL[13].text =  GlobalVar.GetInstance().ceshi;
	}
}
