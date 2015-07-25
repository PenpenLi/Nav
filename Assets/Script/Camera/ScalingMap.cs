using UnityEngine;
using System.Collections;


public class ScalingMap : MonoBehaviour {

    //public Camera m_Camera;
    public GameObject m_CameraSize;
    public GameObject m_Leng1;
    public GameObject m_Leng2;
    public GameObject m_Scaling;
    public GameObject m_camera;
    private Vector3 TFScale ; //Transform.Scale
    
    //记录上一次触摸位置
     private Vector3 oldPos1;
     private Vector3 oldPos2;

     const float MaxCS = 2.0f;
     const float MinCS = 1.0f;

     float CameraSize;


     ////缩放系数
     private float distance ;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        Scalingmap();        
	}
    
    //ScalingMap
    void Scalingmap()
    {
        distance = float.Parse( m_Scaling.transform.FindChild("GetSize").GetComponent<UILabel>().text);
        CameraSize = m_camera.transform.GetComponent<Camera>().orthographicSize;
        if (Input.touchCount > 1) //触摸点
        {
            m_camera.GetComponent<CameraDragMove>().enabled = false;
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                //获得触点坐标
                Vector3 NewPos1 = Input.GetTouch(0).position;
                Vector3 NewPos2 = Input.GetTouch(1).position;

                if (isEnlarge(oldPos1, oldPos2, NewPos1, NewPos2))//放大
                {
                    if (CameraSize >= MinCS)
                    {
                        CameraSize -= distance;
                        m_camera.transform.GetComponent<Camera>().orthographicSize = CameraSize;
                    }
                }
                else//缩小
                {
                    if (CameraSize <= MaxCS)
                    {
                        CameraSize += distance;
                        m_camera.transform.GetComponent<Camera>().orthographicSize = CameraSize;
                    }
                }
                //备份上一次触摸点的位置，用于对比
                oldPos1 = NewPos1;
                oldPos2 = NewPos2;

                m_Leng1.transform.FindChild("GetSize").GetComponent<UILabel>().text = oldPos1.ToString();
                m_Leng2.transform.FindChild("GetSize").GetComponent<UILabel>().text = NewPos1.ToString();

            }
        }
        // 防止在缩放过程中有一个触摸点先松开，会读取但触摸点移动地图
        else if (Input.touchCount == 0)
        {
            m_camera.GetComponent<CameraDragMove>().enabled = true;
        }
        m_CameraSize.transform.FindChild("GetSize").GetComponent<UILabel>().text = CameraSize.ToString();
        
    }

    bool isEnlarge(Vector3 oP1,Vector3 oP2, Vector3 nP1 , Vector3 nP2)
    {
        //函数传入上一次触摸两点的位置与本次触摸两点的位置计算出用户的手势
        float leng1 =Mathf.Sqrt((oP1.x-oP2.x)*(oP1.x-oP2.x)+(oP1.y-oP2.y)*(oP1.y-oP2.y));
        float leng2 =Mathf.Sqrt((nP1.x-nP2.x)*(nP1.x-nP2.x)+(nP1.y-nP2.y)*(nP1.y-nP2.y));
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
