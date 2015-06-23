using UnityEngine;
using System.Collections;


public class ScalingMap : MonoBehaviour {

    public GameObject m_Map ;

    private Vector3 TFScale ; //Transform.Scale
    

    //记录上一次触摸位置判断是在左放大还是缩小手势
     private Vector3 oldPos1;
     private Vector3 oldPos2;

     ////缩放系数
     //private float distance = 10.0f;

	// Use this for initialization
	void Start () {
        TFScale = m_Map.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        Scalingmap();
	}
    
    //ScalingMap
    void Scalingmap()
    {
        if (Input.touchCount >1) //触摸点
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                //获得触点坐标
                var Pos1 = Input.GetTouch(0).position;
                var Pos2 = Input.GetTouch(1).position;

                if (isEnlarge(oldPos1, oldPos2, oldPos1, oldPos2))
                {
                    if (TFScale.x <= 1.0 )
                    {
                        TFScale.x += m_Map.transform.localScale.x + 0.05f;
                    }
                    if (TFScale.y <= 1.0)
                    {
                        TFScale.y += m_Map.transform.localScale.y + 0.05f;
                    }
                    m_Map.transform.localScale = TFScale;
                }
                else
                {

                    if (TFScale.x <= 1.0)
                    {
                        TFScale.x += m_Map.transform.localScale.x + 0.05f;
                    }
                    if (TFScale.y <= 1.0)
                    {
                        TFScale.y += m_Map.transform.localScale.y + 0.05f;
                    }
                    m_Map.transform.localScale = TFScale;
                }
                //备份上一次触摸点的位置，用于对比
                oldPos1 = Pos1;
                oldPos2 = Pos2;
 
            }
        }
    }

    bool isEnlarge(Vector3 oP1,Vector3 oP2, Vector3 nP1 , Vector3 nP2)
    {
        //函数传入上一次触摸两点的位置与本次触摸两点的位置计算出用户的手势
        var leng1 =Mathf.Sqrt((oP1.x-oP2.x)*(oP1.x-oP2.x)+(oP1.y-oP2.y)*(oP1.y-oP2.y));
        var leng2 =Mathf.Sqrt((nP1.x-nP2.x)*(nP1.x-nP2.x)+(nP1.y-nP2.y)*(nP1.y-nP2.y));
        if(leng1<leng2)
        {
            return true;//放大
        }
        else
        {
            return false;//缩小
        }         
    }
 
 
}
