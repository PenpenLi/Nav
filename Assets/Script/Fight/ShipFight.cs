using UnityEngine;
using System.Collections;
using System;

public class ShipFight : MonoBehaviour {
	public GameObject FightPt;
    public GameObject Shell;
    private Curve2D curve = new Curve2D();
    public float FireSpeed = 1.0f;//攻击速度
    bool bOpenFire = false;
	// Use this for initialization
	void Start () {
        Shell.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	    if(bOpenFire)
        {
            //PlayerDataManager.GetInstance().
        }
	}

    public void SetShellType(int type)
    {
        string shellname = "ani_paodan_0" + Convert.ToString(type);
        Shell.GetComponent<UISprite>().spriteName = shellname;
    }

    public void OpenFire(Vector3 targetPos, int shellType)
    {
        curve.SetCubicCurve(FightPt.transform.position, targetPos);
        bOpenFire = true;
    }
}
