using UnityEngine;
using System.Collections;

public class Curve2D {
	protected Vector3 m_startPoint = new Vector3();
	protected Vector3 m_middlePoint1 = new Vector3();
	protected Vector3 m_middlePoint2 = new Vector3();
	protected Vector3 m_endPoint = new Vector3();
    protected float m_Scale{set;get;}

	public virtual void SetCubicCurve(Vector3 startPoint,Vector3 endPoint)
	{
		m_startPoint = startPoint;
		m_endPoint = endPoint;

		float length = endPoint.x - startPoint.x;

		m_middlePoint1 = new Vector3 (startPoint.x+0.3f*length,startPoint.y+0.6f,0);
		m_middlePoint2 = new Vector3 (startPoint.x+0.7f*length,endPoint.y+0.6f,0);

	}

	public void SetLineCurve(Vector3 startPoint,Vector3 endPoint)
	{
		m_startPoint = startPoint;
		m_endPoint = endPoint;

		m_middlePoint1 = startPoint;
		m_middlePoint2 = endPoint;
		
	}

	public void SetMiddlePoint(Vector3 middlePoint1, Vector3 middlePoint2)
	{
		m_middlePoint1 = middlePoint1;
		m_middlePoint2 = middlePoint2;
	}

    public float GetPos()
    {
        return m_Scale;
    }

    public void RestPos()
    {
        m_Scale = 0.0f;
    }

	public virtual Vector3 SetPos(float scale)
	{
		//float scale =(x-m_startPoint.x)/(m_endPoint.x-m_endPoint.x);
		scale=Mathf.Clamp01 (scale);
        m_Scale = scale;
		//Debug.Log (scale.ToString ());

		Vector3 pos = m_startPoint * (1 - scale) * (1 - scale) * (1 - scale) +
						m_middlePoint1 * 3 * scale * (1 - scale) * (1 - scale) +
						m_middlePoint2 * 3 * scale * scale * (1 - scale) +
						m_endPoint * scale * scale * scale;
		return pos;
	}


    public int GetLength(Vector3 p1, Vector3 p2)
    {

        return 0;

    }
}

