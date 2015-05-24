using UnityEngine;
using System.Collections;

public class Shell : MonoBehaviour {

	Curve2D path = new Curve2D();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetShellTrack(Vector3 startPt, Vector3 endPt)
	{
		path.SetCubicCurve(startPt, endPt);
	}

	public void adjustArcHeight(Vector3 mid1, Vector3 mid2)
	{
		path.SetMiddlePoint(mid1, mid2);
	}

	public void getCubicPos(float fScale)
	{
		path.GetPos(fScale);
	}


}
