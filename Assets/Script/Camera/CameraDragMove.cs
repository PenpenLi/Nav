using UnityEngine;
using System.Collections;

public class CameraDragMove : MonoBehaviour
{
    public Camera m_camera;
    private Vector3 ResetCamera;//摄像机初始坐标
    private Vector3 Origin;//移动停止坐标
    private Vector3 Diference;//摄像机当前坐标
    private Vector3 Dif;
    private Vector3 CL;
    private bool Drag = false;//是否开始记录点击点坐标的开关

    float CameraSize ;  
    const float Scalingfactor_x = 1.8f; // X 轴边界变化系数
    const float Scalingfactor_y = 1f;  // Y 轴边界变化系数
    const float CS = 1.5f; // The initial size of the camera 

    void Start()
    {
        ResetCamera = m_camera.transform.position; //获得摄像机初始位置  
        Dif = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LateUpdate()
    {
        if (Input.GetMouseButton(0)) // 鼠标左键是否按下
        {
            Diference = (m_camera.ScreenToWorldPoint(Input.mousePosition)) - ResetCamera;//获得相机移动距离
            if (Drag == false)
            {
                Drag = true;
                Origin = m_camera.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            Drag = false;
        }
        if (Drag == true)
        {
            ResetCamera = Origin - Diference;
            float Csize_x = 0;

            // X 轴边界
            if (ResetCamera.x > 4.2 + Csize_x)
            {
                ResetCamera.x = 4.15f + Csize_x;
                m_camera.transform.position = ResetCamera;
            }
            else if (ResetCamera.x < -(3.0 + Csize_x))
            {
                ResetCamera.x = -(2.95f + Csize_x);
                CL = m_camera.transform.localPosition;
                CL.x= m_camera.orthographicSize * 576;
                m_camera.transform.position = ResetCamera;
            }
            else if (ResetCamera.x > -(3.0 + Csize_x) || ResetCamera.x < 4.2 + Csize_x)
            {
                m_camera.transform.position = ResetCamera;

            }

            // Y 轴边界
            float Csize_y = 0;
            if (ResetCamera.y > 5.2 + Csize_y)
            {
                ResetCamera.y = 5.15f + Csize_y;
                m_camera.transform.position = ResetCamera;
            }
            else if (ResetCamera.y < -(1.9 + Csize_y))
            {
                ResetCamera.y = -(1.85f + Csize_y);
                m_camera.transform.position = ResetCamera;
            }
            else if (ResetCamera.y > -(1.9 + Csize_y) || ResetCamera.y < 5.2 + Csize_y)
            {
                m_camera.transform.position = ResetCamera;

            }
        }

        //RESET CAMERA TO STARTING POSITION WITH RIGHT CLICK
        if (Input.GetMouseButton(1))// 鼠标右键是否按下
        {
            m_camera.transform.position = Dif;
        }
    }
}
