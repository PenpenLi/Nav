using UnityEngine;
using System.Collections;

public class CameraDragMove : MonoBehaviour
{
    public Camera m_camera;
    private Vector3 ResetCamera;//摄像机初始坐标
    private Vector3 Origin;//移动停止坐标
    private Vector3 Diference;//摄像机当前坐标
    private Vector3 Dif;
    private Vector3 LS;
    private bool Drag = false;//是否开始记录点击点坐标的开关

    float CameraSize ;  
    const float Scalingfactor_x = 1.8f;
    const float Scalingfactor_y = 1f;
    const float CS = 1.5f;

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
            CameraSize = m_camera.orthographicSize;
            Diference = (m_camera.ScreenToWorldPoint(Input.mousePosition)) - ResetCamera;//获得相机移动距离
            if (Drag == false)
            {
                Drag = true;
                Origin = m_camera.ScreenToWorldPoint(Input.mousePosition);
                //Debug.Log("Origin = " + Origin);
            }
        }
        else
        {
            Drag = false;
        }
        if (Drag == true)
        {
            ResetCamera = Origin - Diference;
            float Csize_x = 0;// (CS - CameraSize) * Scalingfactor_x;
            Debug.Log(Csize_x);
            if (ResetCamera.x > 5.1 + Csize_x)
            {
                ResetCamera.x = 5.05f + Csize_x;
                m_camera.transform.position = ResetCamera;
            }
            else if (ResetCamera.x < -(3.9 + Csize_x))
            {
                ResetCamera.x = -(3.85f + Csize_x);
                m_camera.transform.position = ResetCamera;
            }
            else if (ResetCamera.x > -(3.9 + Csize_x) || ResetCamera.x < 5.1 + Csize_x)
            {
                m_camera.transform.position = ResetCamera;
                Debug.Log("ResetCamera X=" + ResetCamera);
            }

            float Csize_y = 0;// (CS - CameraSize) * Scalingfactor_y;
            if (ResetCamera.y > 5.7 + Csize_y)
            {
                ResetCamera.y = 5.65f + Csize_y;
                m_camera.transform.position = ResetCamera;
            }
            else if (ResetCamera.y < -(2.4 + Csize_y))
            {
                ResetCamera.y = -(2.35f + Csize_y);
                m_camera.transform.position = ResetCamera;
            }
            else if (ResetCamera.y > -(2.4 + Csize_y) || ResetCamera.y < 5.7 + Csize_y)
            {
                m_camera.transform.position = ResetCamera;
                Debug.Log("ResetCamera Y=" + ResetCamera);
            }
        }
        //RESET CAMERA TO STARTING POSITION WITH RIGHT CLICK
        if (Input.GetMouseButton(1))// 鼠标右键是否按下
        {
            m_camera.transform.position = Dif;
        }
    }
}
