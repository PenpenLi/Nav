using UnityEngine;
using System.Collections;

public class CameraDragMove : MonoBehaviour
{
    public Camera m_camera;
    private Vector3 ResetCamera;//摄像机初始坐标
    private Vector3 Origin;//移动停止坐标
    private Vector3 Diference;//摄像机当前坐标
    private Vector3 dif;
    private bool Drag = false;//是否开始记录点击点坐标的开关
    void Start()
    {
        ResetCamera = m_camera.transform.position; //获得摄像机初始位置  
        dif = new Vector3(0,0,0);
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
            //左右边界限定
            ResetCamera = Origin - Diference;
            if (ResetCamera.x < -5.5)
            {
                ResetCamera.x = -5.45f;
                m_camera.transform.position = ResetCamera;
            }
            else if (ResetCamera.x > 5.2)
            {
                ResetCamera.x = 5.15f;
                m_camera.transform.position = ResetCamera;
            }
            else if (ResetCamera.x > -5.5 || ResetCamera.x<5.2)
            {
                m_camera.transform.position = ResetCamera;
                //Debug.Log("ResetCamera =" + ResetCamera);
            }
            //上下边界限定
            if (ResetCamera.y < -3.3)
            {
                ResetCamera.y = -3.25f;
                m_camera.transform.position = ResetCamera;
            }
            else if (ResetCamera.y > 5.8)
            {
                ResetCamera.y = 5.75f;
                m_camera.transform.position = ResetCamera;
            }
            else if (ResetCamera.y > -3.3 || ResetCamera.y < 5.8)
            {
                m_camera.transform.position = ResetCamera;
                //Debug.Log("ResetCamera =" + ResetCamera);
            }     
        }
        //RESET CAMERA TO STARTING POSITION WITH RIGHT CLICK
        if (Input.GetMouseButton(1))// 鼠标右键是否按下
        {
            m_camera.transform.position = dif;
        }
    }
}
