using UnityEngine;
using System.Collections;

public class SestInpt : MonoBehaviour {

    public GameObject m_TestTime;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        GlobalVar.GetInstance().UpgradeTime = int.Parse(m_TestTime.GetComponent<UILabel>().text);
	}


}
