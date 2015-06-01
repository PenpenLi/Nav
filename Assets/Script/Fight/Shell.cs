using UnityEngine;
using System.Collections;

public class Shell : MonoBehaviour {

	Curve2D path = new Curve2D();
    public float m_fScale = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetShellTrack(Vector3 startPt, Vector3 endPt, int type)
	{
		path.SetCubicCurve(startPt, endPt);
        SetShellType(startPt, endPt, type);
	}

	public void AdjustArcHeight(Vector3 mid1, Vector3 mid2)
	{
		path.SetMiddlePoint(mid1, mid2);
	}

	public void SetCubicPos(float fScale)
	{
		path.SetPos(fScale);
        m_fScale = fScale;
	}

    public Vector3 AddCubicPos(float fAdd)
    {
        m_fScale += fAdd;
        return path.SetPos(m_fScale);
    }

    public float GetCubicPos()
    {
        return path.GetPos();
    }

    private void SetShellType(Vector3 startPt, Vector3 endPt, int type)
    {
        Vector3 mid1 = new Vector3();
        Vector3 mid2 = new Vector3();
        switch(type)
        {
            case 0:
                mid1.x = 80.0f;
                mid1.y = 60.0f;
                mid2.x = 300.0f;
                mid2.y = 60.0f;
                break;
            case 1:
                 mid1.x = 80.0f;
                mid1.y = 60.0f;
                mid2.x = 300.0f;
                mid2.y = 60.0f;
                break;
            case 2:
                 mid1.x = 80.0f;
                mid1.y = 60.0f;
                mid2.x = 300.0f;
                mid2.y = 60.0f;
                break;
            case 3:
                 mid1.x = 80.0f;
                mid1.y = 60.0f;
                mid2.x = 300.0f;
                mid2.y = 60.0f;
                break;
            case 4:
                 mid1.x = 80.0f;
                mid1.y = 60.0f;
                mid2.x = 300.0f;
                mid2.y = 60.0f;
                break;
        }
        AdjustArcHeight(startPt + mid1, startPt + mid2);

    }

}
