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
    void Start()
    {
        ResetCamera = m_camera.transform.position; //获得摄像机初始位置  
        Dif = new Vector3(0, 0, 0);
        Debug.Log(m_camera.transform.localScale);
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
            cameraX();
            cameraY();
            //if (m_camera.transform.lossyScale == new Vector3(1.0f, 1.0f, 1.0f))
            //{
            //    cameraX();
            //    cameraY();
            //}



        }
        //RESET CAMERA TO STARTING POSITION WITH RIGHT CLICK
        if (Input.GetMouseButton(1))// 鼠标右键是否按下
        {
            m_camera.transform.position = Dif;
        }
    }
    void cameraX()
    {
        if (ResetCamera.x < -1.7)
        {
            ResetCamera.x = -1.65f;
            m_camera.transform.position = ResetCamera;
        }
        else if (ResetCamera.x > 1.9)
        {
            ResetCamera.x = 1.85f;
            m_camera.transform.position = ResetCamera;
        }
        else if (ResetCamera.x > -1.7 || ResetCamera.x < 1.9)
        {
            m_camera.transform.position = ResetCamera;
            Debug.Log("ResetCamera X=" + ResetCamera);
        }
    }
    void cameraY()
    {
        if (ResetCamera.y < -2.4)
        {
            ResetCamera.y = -2.35f;
            m_camera.transform.position = ResetCamera;
        }
        else if (ResetCamera.y > 1.2)
        {
            ResetCamera.y = 1.15f;
            m_camera.transform.position = ResetCamera;
        }
        else if (ResetCamera.y > -2.4 || ResetCamera.y < 1.2)
        {
            m_camera.transform.position = ResetCamera;
            Debug.Log("ResetCamera Y=" + ResetCamera);
        }
    }
}
